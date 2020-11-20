using System.Threading;
using System.Threading.Tasks;
using Expertec.Sigeco.CrossCutting.SeedWork.Domain;
using Expertec.Lcc.Services.Notify.API.Infrastructure.EntityTypeConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

namespace Expertec.Lcc.Services.Notify.API.Infrastructure
{
    public class NotifyContext : DbContext, IUnitOfWork
    {
        #region Constantes

        public const string DEFAULT_SCHEMA = "notifica";

        #endregion

        #region Constructor

        public NotifyContext(DbContextOptions<NotifyContext> options) : base(options) { }

        #endregion

        #region Override

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new AlertaEntityTypeConfiguration());
            builder.ApplyConfiguration(new AlertaUsuarioEntityTypeConfiguration());
            builder.ApplyConfiguration(new NotificacionEntityTypeConfiguration());
            builder.ApplyConfiguration(new TipoAlertaEntityTypeConfiguration());
            builder.ApplyConfiguration(new UsuarioEntityTypeConfiguration());
        }

        #endregion

        #region Metodos

        public async Task<int> SaveEntitiesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        public async Task<IDbContextTransaction> BeginTransactionAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await base.Database.BeginTransactionAsync(cancellationToken);
        }

        #endregion


    }
}