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
        public DbSet<EmpresaQueryDtos> empresaQueryDtos { get; set; }
        public DbSet<ValidacionUsuarioQueryDto> validacionUsuarioQueryDto { get; set; }
        public DbSet<ActualizarContrasenaQueryDto> actualizarContrasenaQueryDto { get; set; }
        public DbSet<MenuQueryDto> ventanaLoginQueryDto { get; set; }
        public DbSet<MailRecuperarContrasenaQueryDto> mailRecuperarContrasenaQueryDto { get; set; }
        public DbSet<TipoTerapiaQueryDto> lstTipoTerapiaQueryDto { get; set; }
        public DbSet<SucursalQueryDto> sucursalQueryDto { get; set; }
        public DbSet<ValidaTerapistaQueryDto> validaTerapistaQueryDto { get; set; }
        public DbSet<TerapistaQueryDto> terapistaQueryDto { get; set; }
        public DbSet<MailBienvenidaQueryDto> mailBienvenidaQueryDto { get; set; }
        public DbSet<RolQueryDto> rolQueryDto { get; set; }
        public DbSet<PacientesQueryDto> pacientesQueryDto { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ActualizarContrasenaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<EmpresaQueryDtos>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValidacionUsuarioQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MenuQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MailRecuperarContrasenaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<TipoTerapiaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<ValidaTerapistaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<TerapistaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<MailBienvenidaQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<RolQueryDto>().HasNoKey().ToView(null);
            modelBuilder.Entity<PacientesQueryDto>().HasNoKey().ToView(null);
        }
    }
}
