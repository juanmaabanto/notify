using Expertec.Sigeco.CrossCutting.SeedWork.Domain;

namespace Expertec.Lcc.Services.Notify.API.Models
{
    public class Usuario : IAggregateRoot
    {
        public string UsuarioId { get; set; }
        public string NombreUsuario { get; set; }
        public bool Activo { get; set; }
    }
}