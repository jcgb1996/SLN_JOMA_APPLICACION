using Microsoft.EntityFrameworkCore;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContextEF : DbContext
    {
        public readonly string? connectionString;

        public JomaQueryContextEF(string connectionString)
        {
            this.connectionString = connectionString;
        }
        public JomaQueryContextEF()
           : base()
        {
        }


        public JomaQueryContextEF(DbContextOptions<JomaQueryContextEF> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.UseSqlServer("");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
