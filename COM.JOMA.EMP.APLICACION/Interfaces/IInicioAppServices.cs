using COM.JOMA.EMP.APLICACION.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IInicioAppServices
    {
        Task<LoginAppResultDto> LoginCompania(LoginReqAppDto login);
        Task<List<MenuAppDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario, byte SitioWeb);
    }
}
