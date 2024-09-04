using COM.JOMA.EMP.DOMAIN.Entities;
using COM.JOMA.EMP.QUERY.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class SucursalQueryService : BaseQueryService, ISucursalQueryService
    {
        public SucursalQueryService(IServiceScopeFactory serviceProvider) : base(serviceProvider)
        {
        }

        public bool RegistrarSucursal(Sucursal sucursal)
        {
            throw new NotImplementedException();
        }
    }
}
