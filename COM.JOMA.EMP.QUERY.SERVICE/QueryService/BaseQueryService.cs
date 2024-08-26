using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.QUERY.SERVICE.QueryService
{
    public class BaseQueryService
    {
        protected readonly IServiceScopeFactory serviceProvider;
        public BaseQueryService(IServiceScopeFactory serviceProvider)
        {
            this.serviceProvider = serviceProvider;
        }
    }
}
