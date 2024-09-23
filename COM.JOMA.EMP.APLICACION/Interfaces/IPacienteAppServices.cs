using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.QUERY.Dtos;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IPacienteAppServices
    {
        JOMAResponse RegistrarPaciente(PacienteReqtDto pacienteReqtDto);
        JOMAResponse ActualizarPaciente(EditarPacienteReqDto pacienteReqtDto);
        Task<List<PacientesQueryDto>> GetPacientesXRucEmpresa(string RucEmpresa);
    }
}
