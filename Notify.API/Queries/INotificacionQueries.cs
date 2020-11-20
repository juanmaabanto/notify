using System.Collections.Generic;
using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.ViewModel;

namespace Expertec.Lcc.Services.Notify.API.Queries
{
    public interface INotificacionQueries
    {
        Task<int> CuentaSinNotificarAsync();

        Task<(int total, IEnumerable<NotificacionListViewModel> data)> ListarAsync(bool soloNuevos, bool soloPopup, string sort, int pageSize, int start);

    }
}