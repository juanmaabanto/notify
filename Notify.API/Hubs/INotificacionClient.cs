using System;
using System.Threading.Tasks;

namespace Expertec.Lcc.Services.Notify.API.Hubs
{
    public interface INotificacionClient
    {
        Task RecibirNotificacion(int notificacionId, string dTipoAlerta, string titulo, string contenido, DateTime fecha, string enlace, bool enlaceExterno, 
            string iconoCls, string emisor);
    }
}