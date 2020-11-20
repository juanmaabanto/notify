using System;
using Expertec.Sigeco.CrossCutting.SeedWork.Domain;

namespace Expertec.Lcc.Services.Notify.API.Models
{
    public class Notificacion : IAggregateRoot
    {
        public int NotificacionId { get; set; }
        public int AlertaId { get; set; }
        public string UsuarioId { get; set; }
        public string EmisorId { get; set; }
        public string Contenido { get; set; }
        public DateTime Fecha { get; set; }
        public bool Notificado { get; set; }
        public string Enlace { get; set; }
        public bool EnlaceExterno { get; set; }
        public DateTime? FechaNotificado { get; set; }
        public bool Anulado { get; set; }
        public Alerta Alerta { get; private set; }
        public Usuario Usuario { get; private set; }
        public Usuario Emisor { get; private set; }
    }
}