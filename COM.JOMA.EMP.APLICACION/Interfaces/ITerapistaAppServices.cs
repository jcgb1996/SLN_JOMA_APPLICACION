using COM.JOMA.EMP.APLICACION.Dto;
using COM.JOMA.EMP.APLICACION.Dto.Request.Administracion.TerapistaDto;
using COM.JOMA.EMP.QUERY.Dtos;

namespace COM.JOMA.EMP.APLICACION.Interfaces
{
    public interface ITerapistaAppServices
    {
        public Task<JOMAResponse> RegistrarTerapista(SaveTerapistaReqDto TerapistaReqtDto);
        public Task<JOMAResponse> EditarTerapista(EditTerapistaReqDto TerapistaReqtDto);
        public Task<TerapistaQueryDto> GetTerapistasPorId(long IdTerapista, string RucCompania);
        public Task<List<TerapistasGridQueryDto>> GetTerapistasXRucEmpresa(string RucEmpresa);
        public Task<(bool, bool)> ValidaTerapistaXCedulaXRucEmpresaXCorreo(string Cedula, string RucEmpresa, string Correo);
    }
}
