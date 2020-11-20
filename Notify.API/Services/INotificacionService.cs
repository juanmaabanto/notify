using System.Collections.Generic;
using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.ViewModel;

namespace Expertec.Lcc.Services.Notify.API.Services
{
    public interface INotificacionService
    {
        //Task<Notificacion> AgregarAsync(NotificacionRegisterDTO item, TipoNotificacionDTO tipo, string usuario);

        Task ActualizaNoNotificadosAsync(IEnumerable<NotificacionListViewModel> items);

        Task AnularAsync(int notificacionId);
    }
}