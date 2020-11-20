using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Expertec.Sigeco.CrossCutting.Common.ViewModel;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Exceptions;
using Expertec.Lcc.Services.Notify.API.Queries;
using Expertec.Lcc.Services.Notify.API.Services;
using Expertec.Lcc.Services.Notify.API.ViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Expertec.Lcc.Services.Notify.API.Controllers
{
    [Route("api/v1/[controller]")]
    public class NotificacionesController : Controller
    {
        #region Variables

        private readonly INotificacionQueries _queries;
        private readonly INotificacionService _services;

        #endregion

        #region Builders

        public NotificacionesController(INotificacionService services, INotificacionQueries queries)
        { 
            _queries = queries ?? throw new ArgumentNullException(nameof(queries));
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        #endregion

        #region Gets

        /// <summary>
        /// Devuelve lista de notificaciones.
        /// </summary>
        /// <param name="soloNuevos">Solo notificaciones nueva.</param>
        /// <param name="sort">Criterio de ordenación en formato json.</param>
        /// <param name="pageSize">Número de filas devueltas por página.</param>
        /// <param name="pageIndex">Número de página.</param>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("")]
        [ProducesResponseType(typeof(PaginadoViewModel<IEnumerable<NotificacionListViewModel>>), 200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public async Task<IActionResult> Listar(string titulo, bool soloNuevos, bool soloPopup, string sort, [FromQuery]int pageSize = 50, [FromQuery]int start = 0)
        {
            try
            {
                var result = await _queries.ListarAsync(soloNuevos, soloPopup, sort, pageSize, start);
                await _services.ActualizaNoNotificadosAsync(result.data);

                var model = new PaginadoViewModel<NotificacionListViewModel>(
                start, pageSize, result.total, result.data);

                return Ok(model);
            }
            catch (NotifyDomainException ex)
            {
                return BadRequest(new ErrorViewModel(ex.ErrorId, ex.Message));
            }
        }

        /// <summary>
        /// Devuelve cuenta de notificaciones no vistas.
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("SinNotificar")]
        [ProducesResponseType(typeof(int), 200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        public async Task<IActionResult> CuentaNoVistos()
        {
            try
            {
                var result = await _queries.CuentaSinNotificarAsync();

                return Ok(result);
            }
            catch (NotifyDomainException ex)
            {
                return BadRequest(new ErrorViewModel(ex.ErrorId, ex.Message));
            }
        }

        #endregion

        #region Deletes

        /// <summary>
        /// Elimina una notificación.
        /// </summary>
        /// <param name="notificacionId">Id de la notificación.</param>
        /// <returns></returns>
        [Authorize]
        [HttpDelete]
        [Route("{notificacionId:int}")]
        [ProducesResponseType(typeof(Boolean), 200)]
        [ProducesResponseType(typeof(ErrorViewModel), 400)]
        [ProducesResponseType(typeof(String), 404)]
        public async Task<IActionResult> Anular(int notificacionId)
        {
            try
            {
                await _services.AnularAsync(notificacionId);

                return Ok(true);
            }
            catch (NotifyDomainException ex)
            {
                return BadRequest(new ErrorViewModel(ex.ErrorId, ex.Message));
            }
        }

        #endregion

    }

}