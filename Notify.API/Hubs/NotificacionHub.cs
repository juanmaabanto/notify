using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace Expertec.Lcc.Services.Notify.API.Hubs
{
    public class NotificacionHub : Hub<INotificacionClient>
    {
        #region Overrides

        public override async Task OnConnectedAsync()
        {
            await base.OnConnectedAsync();
        }

        #endregion

    }
}