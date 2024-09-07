using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface IConsultasAppServices
    {
        public void GetPacientes(long IdCompania);
        public Task<List<TerapistaQueryDto>> GetTerapistasPorIdCompania(long IdCompania, string RucCompania);
        public Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista, string RucCompania);
        public Task<EmpresaQueryDtos> GetCompaniaXidXRuc(long IdCompania, string Ruc);
    }
}
