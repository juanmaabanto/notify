using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Expertec.Lcc.Services.Notify.API.Models;
using Expertec.Sigeco.CrossCutting.SeedWork.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure.Repositories
{
    public class UsuarioRepository : Repository<Usuario>, IUsuarioRepository
    {
        #region Variables

        private readonly NotifyContext _ctx;
            
        #endregion

        #region Constructor

        public UsuarioRepository(NotifyContext context): base(context)
        {
            _ctx = context ?? throw new ArgumentNullException(nameof(context));
        }

        #endregion

        #region Metodos

        public async Task EliminarAlertasAsync(string usuarioId)
        {
            var alertas = await  _ctx.Set<AlertaUsuario>().Where(p => p.UsuarioId == usuarioId).ToListAsync();

            _ctx.Set<AlertaUsuario>().RemoveRange(alertas);
        }

        #endregion

    }
}