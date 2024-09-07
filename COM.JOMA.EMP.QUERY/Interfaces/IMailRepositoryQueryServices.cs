using COM.JOMA.EMP.DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.Interfaces
{
    public interface IMailRepositoryQueryServices
    {
        bool ActualizarMail(TrazabilidadCorreo mail);
        bool InsertarTrazabilidadCorreo(TrazabilidadCorreo mail);
    }
}
