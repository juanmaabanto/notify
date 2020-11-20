using System;
using System.Linq;
using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories;
using Expertec.Lcc.Services.Notify.API.IntegrationEvents.Events;
using Expertec.Sigeco.CrossCutting.EventBus.Abstractions;

namespace Expertec.Lcc.Services.Notify.API.IntegrationEvents.EventHandling
{
    public class UsuarioEliminadoIntegrationEventHandler : IIntegrationEventHandler<UsuarioEliminadoIntegrationEvent>
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioEliminadoIntegrationEventHandler(IUsuarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(UsuarioEliminadoIntegrationEvent @event)
        {
            var usuario = (await _repository.FindByAsync(u => u.UsuarioId == @event.UsuarioId)).FirstOrDefault();

            if(usuario == null)
            {
                Console.Write($"IntegrationEvents: No se encontro el usuario {@event.UsuarioId}");
            }
            await _repository.EliminarAlertasAsync(usuario.UsuarioId);
            _repository.Remove(usuario);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}