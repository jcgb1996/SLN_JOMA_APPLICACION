using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface ITerapistaQueryServices
    {
        Task<long> RegistrarTerapista(Terapista terapista);
        Task<bool> EditarTerapista(Terapista terapista);
        Task<List<TerapistasGridQueryDto>> GetTerapistasXRucEmpresa(string RucEmpresa);
        Task<ValidaTerapistaQueryDto> ValidaTerapistaXCedulaXCorreo(string Cedula, string RucEmpresa, string Correo);//lo copie tal cual, creo
        Task<TerapistaQueryDto> GetTerapistasXIdXIdEmpresa(long IdTerapista, long IdEmpresa);//porque valida los usuarios y la sucursal no es un usuario
    }
}
