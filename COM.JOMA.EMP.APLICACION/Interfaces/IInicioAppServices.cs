using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response.Inicio;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IInicioAppServices
    {
        Task<LoginAppResultDto> LoginCompania(LoginReqAppDto login);
        Task<List<MenuAppDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario);
    }
}
