using System;
using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories;
using Expertec.Lcc.Services.Notify.API.IntegrationEvents.Events;
using Expertec.Lcc.Services.Notify.API.Models;
using Expertec.Sigeco.CrossCutting.EventBus.Abstractions;

namespace Expertec.Lcc.Services.Notify.API.IntegrationEvents.EventHandling
{
    public class UsuarioCreadoIntegrationEventHandler : IIntegrationEventHandler<UsuarioCreadoIntegrationEvent>
    {
        private readonly IUsuarioRepository _repository;

        public UsuarioCreadoIntegrationEventHandler(IUsuarioRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        public async Task Handle(UsuarioCreadoIntegrationEvent @event)
        {
            var usuario = new Usuario();

            usuario.UsuarioId = @event.UsuarioId;
            usuario.NombreUsuario = @event.NombreUsuario;
            usuario.Activo = @event.Activo;

            await _repository.AddAsync(usuario);
            await _repository.UnitOfWork.SaveChangesAsync();
        }
    }
}