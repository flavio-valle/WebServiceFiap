using Microsoft.EntityFrameworkCore;
using WebServiceFiap.Model;

namespace WebServiceFiap.Repository.Context
{
    public class ContextBase : DbContext
    {
        public ContextBase(DbContextOptions<ContextBase> options) : base(options)
        {
        }

        public DbSet<MonitoramentoAgua> TB_MONITORAMENTO_AGUA { get; set; }
        public DbSet<MonitoramentoAr> TB_MONITORAMENTO_AR { get; set; }
        public DbSet<Usuario> TB_USUARIO { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasSequence<long>("SEQ_MONITORAMENTO_AGUA", "RM554222")
                        .StartsAt(1)
                        .IncrementsBy(1);

            modelBuilder.Entity<MonitoramentoAgua>(entity =>
            {
                entity.Property(e => e.ID_MONITORAMENTO_AGUA)
                      .UseHiLo("SEQ_MONITORAMENTO_AGUA", "RM554222")
                      .ValueGeneratedOnAdd();
            });

            // Caso queira configurar a sequence para Ar:
            modelBuilder.HasSequence<long>("SEQ_MONITORAMENTO_AR", "RM554222")
                        .StartsAt(1)
                        .IncrementsBy(1);

            modelBuilder.Entity<MonitoramentoAr>(entity =>
            {
                entity.Property(e => e.ID_MONITORAMENTO_AR)
                      .UseHiLo("SEQ_MONITORAMENTO_AR", "RM554222")
                      .ValueGeneratedOnAdd();
            });
        }
    }
}
