using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.Models;
using Expertec.Sigeco.CrossCutting.SeedWork.Domain;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories
{
    public interface IUsuarioRepository : IRepository<Usuario>
    {
        Task EliminarAlertasAsync(string usuarioId);
    }
}