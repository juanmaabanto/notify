using Expertec.Sigeco.CrossCutting.SeedWork.Domain;
using Expertec.Lcc.Services.Notify.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories
{
    public interface INotificacionRepository : IRepository<Notificacion>
    {
            Task<IEnumerable<AlertaUsuario>> ListarUsuariosPorAlertaAsync(int alertaId);
            Task<Usuario> ObtenerUsuarioAsync(string usuarioId);
    }
}