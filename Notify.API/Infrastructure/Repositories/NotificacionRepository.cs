using System;
using Expertec.Sigeco.CrossCutting.SeedWork.Infrastructure;
using Expertec.Lcc.Services.Notify.API.Models;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories
{
    public class NotificacionRepository : Repository<Notificacion>, INotificacionRepository
    {
        #region Variables

        private readonly NotifyContext _ctx;
            
        #endregion

        #region Constructor

        public NotificacionRepository(NotifyContext context): base(context)
        {
            _ctx = context ?? throw new ArgumentNullException(nameof(context));
        }


        #endregion

        #region Metodos

        public async Task<IEnumerable<AlertaUsuario>> ListarUsuariosPorAlertaAsync(int alertaId)
        {
            return await _ctx.Set<AlertaUsuario>()
                .Include(n => n.Alerta)
                .ThenInclude(d => d.TipoAlerta)
                .Where(p => p.AlertaId == alertaId).ToListAsync();
        }

        public async Task<Usuario> ObtenerUsuarioAsync(string usuarioId)
        {
            return await _ctx.Set<Usuario>().FindAsync(usuarioId);
        }

        #endregion

    }
}