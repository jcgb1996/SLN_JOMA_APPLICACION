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
        public DbSet<LoginQueryDto> LoginQueryDto { get; set; }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<LoginQueryDto>().HasNoKey().ToView(null);
        }
    }
}
