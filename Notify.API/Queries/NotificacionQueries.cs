using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Dapper;
using Expertec.Sigeco.CrossCutting.Common.Util;
using Expertec.Sigeco.CrossCutting.LoggingEvent;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Exceptions;
using Expertec.Lcc.Services.Notify.API.Services;
using Expertec.Lcc.Services.Notify.API.ViewModel;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;

namespace Expertec.Lcc.Services.Notify.API.Queries
{
    public class NotificacionQueries : INotificacionQueries
    {
        private readonly IIdentityService _identityService;
        private readonly IOptions<NotifySettings> _settings;
        private readonly ILogger _log;

        public NotificacionQueries(IIdentityService identityService, IOptions<NotifySettings> settings, ILogger log)
        {
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _settings = settings ?? throw new ArgumentNullException(nameof(settings));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task<int> CuentaSinNotificarAsync()
        {
            string usuarioId = _identityService.UsuarioId;
            string usuario = _identityService.UsuarioId;

            try
            {
                string sql = @"SELECT	COUNT(*)
	                FROM notifica.Notificacion
                    WHERE Notificado = 0 and UsuarioId = @usuarioId";

                using (var connection = new SqlConnection(_settings.Value.ConnectionString))
                {
                    connection.Open();

                    var result = await connection.QueryFirstOrDefaultAsync<int>(sql, new { usuarioId });

                    return result;
                }
            }
            catch (Exception ex)
            {
                var result = await _log.ErrorAsync(ex.Message, ex.Source, ex.StackTrace, usuario);

                throw new NotifyDomainException(Constants.MsgGetError, result.EventLogId);
            }
        }

        public async Task<(int total, IEnumerable<NotificacionListViewModel> data)> ListarAsync(bool soloNuevos, bool soloPopup, string sort, int pageSize, int start)
        {
            string usuarioId = _identityService.UsuarioId;
            string usuario = _identityService.UsuarioId;

            try
            {
                var orderby = Helper.GetOrderByFormat(sort);
                string sql = @"SELECT	COUNT(*)
                    FROM notifica.Notificacion n (NOLOCK)
					INNER JOIN notifica.Alerta a (NOLOCK) ON n.AlertaId = a.AlertaId
                    INNER JOIN notifica.TipoAlerta t (NOLOCK) ON a.TipoAlertaId = t.TipoAlertaId
					LEFT JOIN notifica.Usuario em (NOLOCK) ON n.EmisorId = em.UsuarioId
                    WHERE n.Anulado = 0 and n.UsuarioId = @usuarioId
                        and ((@soloNuevos = 1 and n.Notificado = 0 ) or @SoloNuevos = 0)
						and ((@soloPopup = 1 and a.Popup = 1) or @soloPopup = 0);

                    SELECT	n.NotificacionId, a.AlertaId, n.UsuarioId, n.EmisorId, 
                            Titulo = a.Nombre, DTipoAlerta = t.Nombre, n.Contenido, n.Fecha,
                            n.Notificado, n.Enlace, n.EnlaceExterno, n.FechaNotificado,
                            t.IconoCls, Emisor = em.NombreUsuario, a.Permanente, a.Popup,
							a.ImagenUrl
                    FROM notifica.Notificacion n (NOLOCK)
					INNER JOIN notifica.Alerta a (NOLOCK) ON n.AlertaId = a.AlertaId
                    INNER JOIN notifica.TipoAlerta t (NOLOCK) ON a.TipoAlertaId = t.TipoAlertaId
					LEFT JOIN notifica.Usuario em (NOLOCK) ON n.EmisorId = em.UsuarioId
                    WHERE n.Anulado = 0 and n.UsuarioId = @usuarioId
                        and ((@soloNuevos = 1 and n.Notificado = 0 ) or @SoloNuevos = 0)
						and ((@soloPopup = 1 and a.Popup = 1) or @soloPopup = 0)
                    ORDER BY "+ (string.IsNullOrEmpty(orderby) ? "Fecha desc" : orderby) +
                    " OFFSET @start ROWS FETCH NEXT @pageSize ROWS ONLY";
                
                using (var connection = new SqlConnection(_settings.Value.ConnectionString))
                {
                    connection.Open();

                    using(var multi = await connection.QueryMultipleAsync(sql, new {usuarioId, soloNuevos, soloPopup, pageSize, start}))
                    {
                        var total = multi.Read<int>().First();
                        var data = multi.Read<NotificacionListViewModel>().AsList();

                        return (total, data);
                    }
                }
            }
            catch (Exception ex)
            {
                var result = await _log.ErrorAsync(ex.Message, ex.Source, ex.StackTrace, usuario);

                throw new NotifyDomainException(Constants.MsgGetError, result.EventLogId);
            }
        }
    }

}