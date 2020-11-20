using Expertec.Sigeco.CrossCutting.EventBus.Events;

namespace Expertec.Lcc.Services.Notify.API.IntegrationEvents.Events
{
    public class UsuarioEliminadoIntegrationEvent : IntegrationEvent
    {
        public string UsuarioId { get; }

        public UsuarioEliminadoIntegrationEvent(string usuarioId)
        {
            UsuarioId = usuarioId;
        }

    }
}