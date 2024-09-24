using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.PacienteDto;
using COM.JOMA.EMP.QUERY.Dtos;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IPacienteAppServices
    {
        JOMAResponse RegistrarPacienteAsync(SavePacienteReqDto pacienteReqtDto);
        JOMAResponse ActualizarPaciente(EditPacienteReqDto pacienteReqtDto);
        Task<List<PacientesQueryDto>> GetPacientesXRucEmpresa(string RucEmpresa);
    }
}
