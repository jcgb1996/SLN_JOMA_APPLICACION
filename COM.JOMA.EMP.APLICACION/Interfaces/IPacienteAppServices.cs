using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IPacienteAppServices
    {
        public JOMAResponse RegistrarPaciente(PacienteReqtDto pacienteReqtDto);
        public JOMAResponse ActualizarPaciente(EditarPacienteReqDto pacienteReqtDto);
    }
}
