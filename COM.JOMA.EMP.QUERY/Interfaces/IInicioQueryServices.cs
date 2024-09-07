using COM.JOMA.EMP.QUERY.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IInicioQueryServices
    {
        Task<List<LoginQueryDto>> Login(string Usuario, string Clave, string Compania);
        Task<List<MenuQueryDto>> GetOpcionesMenuPorIdUsuario(long IdUsuario);
        Task<List<ValidacionUsuarioQueryDto>> ValidarUsuarioRecuperacion(string Usuario, string Cedula);
    }
}
