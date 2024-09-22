using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IMailQueryService
    {
        Task<MailBienvenidaQueryDto> ConsultarMailBienvenida(long IdEmpresa);
        Task<MailRecuperarContrasenaQueryDto> ConsultarMailRecuperarContrasena(long IdEmpresa);
        Task<ConfigServidorCorreoQueryDto> ConsultarConfigServidorCorreoXIdCompaniaXRucCompania(long IdEmpresa, string? RucCompania);
    }
}
