using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.QUERY.Dtos;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IPacienteAppServices
    {
        Task<JOMAResponse> RegistrarPaciente(SavePacienteReqDto pacienteReqtDto);
        JOMAResponse ActualizarPaciente(EditPacienteReqDto pacienteReqtDto);
        Task<List<PacientesQueryDto>> GetPacientesXRucEmpresa(string RucEmpresa);
    }
}
