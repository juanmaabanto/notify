namespace Expertec.Lcc.Services.Notify.API.Models
{
    public class Alerta
    {
        #region Propiedades

        public int AlertaId { get; set; }
        public int TipoAlertaId { get; set; }
        public string Nombre { get; set; }
        public string ImagenUrl { get; set; }
        public bool Popup { get; set; }
        public bool Permanente { get; set; }
        public bool Activo { get; set; }
        public TipoAlerta TipoAlerta { get; private set; }

        #endregion
    }
}