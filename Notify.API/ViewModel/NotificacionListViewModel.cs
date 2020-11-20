using System;

namespace Expertec.Lcc.Services.Notify.API.ViewModel
{
    public class NotificacionListViewModel
    {
        public int NotificacionId { get; set; }
        public int AlertaId { get; set; }
        public string UsuarioId { get; set; }
        public string EmisorId { get; set; }
        public string Titulo { get; set; }
        public string DTipoAlerta { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public bool Notificado { get; set; }
        public string Enlace { get; set; }
        public bool EnlaceExterno { get; set; }
        public DateTime FechaNotificado { get; set; }
        public string IconoCls { get; set; }
        public string Emisor { get; set; }
        public bool Permanente { get; set; }
        public bool Popup { get; set; }
        public string ImagenUrl { get; set; }
        
    }
}