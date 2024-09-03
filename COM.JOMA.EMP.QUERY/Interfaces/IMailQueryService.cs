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
        Task<MailRecuperarContrasenaQueryDto> ConsultarMailRecuperarContrasena(long IdCompania, string Usuario, string Otp);
        Task<ConfigServidorCorreoQueryDto> ConsultarConfigServidorCorreoXIdCompaniaXRucCompania(long? IdCompania, string? RucCompania);
    }
}
