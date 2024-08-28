using COM.JOMA.EMP.DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IPacienteQueryServices
    {
        public bool RegistrarPaciente(Paciente paciente);
        public bool ActualizarPaciente(Paciente paciente);
    }
}
