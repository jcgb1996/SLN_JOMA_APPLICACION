using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response.Inicio;
using COM.JOMA.EMP.APLICACION.Dto.Response.Mail;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IInicioAppServices
    {
        Task<LoginAppResultDto> LoginCompania(LoginReqAppDto login);
        Task<List<MenuAppDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario);
        Task<(EnvioMailEnLineaAppResultDto, string)> RecuperarContrasena(RecuperacionReqAppDto recuperacionReqAppDto);
        Task<bool> ValidarOtp(string Usuario, string Cedula, string Otp);
        Task<bool> EliminarOtpPorUsuario(string Usuario, string Cedula);
        Task<(bool, string)> ActualizarContrasenaXUsuario(string Usuario, string Cedula, string NuevaContrasena);
    }
}
