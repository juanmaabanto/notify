namespace Expertec.Lcc.Services.Notify.API.Models
{
    public class AlertaUsuario
    {
        #region Propiedades

        public int AlertaId { get; set; }
        public string UsuarioId { get; set; }
        public Alerta Alerta { get; private set; }
        public Usuario Usuario { get; private set; }

        #endregion
    }
}