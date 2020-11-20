using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expertec.Sigeco.CrossCutting.Common.Util;
using Expertec.Sigeco.CrossCutting.LoggingEvent;
using Expertec.Lcc.Services.Notify.API.Hubs;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Exceptions;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories;
using Expertec.Lcc.Services.Notify.API.ViewModel;
using Microsoft.AspNetCore.SignalR;

namespace Expertec.Lcc.Services.Notify.API.Services
{
    public class NotificacionService : INotificacionService
    {
        private readonly IHubContext<NotificacionHub, INotificacionClient> _hubContext;
        private readonly INotificacionRepository _repository;
        private readonly IIdentityService _identityService;
        private readonly ILogger _log;

        public NotificacionService(IHubContext<NotificacionHub, INotificacionClient> hubContext, INotificacionRepository repository, IIdentityService identityService, ILogger log)
        {
            _hubContext = hubContext ?? throw new ArgumentNullException(nameof(hubContext));
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
            _identityService = identityService ?? throw new ArgumentNullException(nameof(identityService));
            _log = log ?? throw new ArgumentNullException(nameof(log));
        }

        public async Task ActualizaNoNotificadosAsync(IEnumerable<NotificacionListViewModel> items)
        {
            string usuario = _identityService.Usuario;
            try
            {
                foreach (var item in items.Where(i => !i.Notificado))
                {
                    var current = await _repository.GetByIdAsync(item.NotificacionId);

                    if (current != null && current.AlertaId != 1)
                    {
                        current.Notificado = true;
                        current.FechaNotificado = DateTime.Now;
                        _repository.Modify(current);
                    }
                }
                //await _hubContext.Clients.All.RecibirNotificacion("danger", "Actualizando no notificados");
                await _repository.UnitOfWork.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                var result = await _log.ErrorAsync(ex.Message, ex.Source, ex.StackTrace, usuario);

                throw new NotifyDomainException(Constants.MsgGetError, result.EventLogId);
            }
        }

        public async Task AnularAsync(int notificacionId)
        {
            string usuario = _identityService.Usuario;

            try
            {
                var current = await _repository.GetByIdAsync(notificacionId);

                if (current != null)
                {
                    current.Anulado = true;
                    _repository.Modify(current);

                    await _repository.UnitOfWork.SaveChangesAsync();
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