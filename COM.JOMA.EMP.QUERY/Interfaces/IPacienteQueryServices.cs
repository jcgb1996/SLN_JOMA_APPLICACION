using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IPacienteQueryServices
    {
        Task<long> RegistrarPaciente(Paciente paciente);
        bool ActualizarPaciente(Paciente paciente);
        Task<List<PacientesQueryDto>> GetPacientesXIdEmpresa(long IdEmpresa);
        Task<ValidaPacienteQueryDto> ValidarCedulaPacienteCorreoNotificacionXIdEmpresa(string Cedula, string CorreoNotificacion, long IdEmpresa);
    }
}
