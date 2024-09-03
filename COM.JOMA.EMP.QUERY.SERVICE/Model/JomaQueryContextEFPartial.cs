using COM.JOMA.EMP.QUERY.Dtos;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.Model
{
    public partial class JomaQueryContextEF
    {
        public DbSet<LoginQueryDto> loginQueryDto { get; set; }
        public DbSet<MenuQueryDto> ventanaLoginQueryDto { get; set; }
        public DbSet<MailRecuperarContrasenaQueryDto> mailRecuperarContrasenaQueryDto { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MenuQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MailRecuperarContrasenaQueryDto>().HasNoKey().ToView(null);
        }
    }
}
