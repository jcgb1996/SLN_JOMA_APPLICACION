using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Attributes
{
    [AttributeUsage(AttributeTargets.Field)]
    internal class JomaDetComponenteAttribute : DescriptionAttribute
    {
        public JomaDetComponenteAttribute(string Codigo, string? Nombre)
        {
            this.Codigo = Codigo;
            this.Nombre = Nombre;
        }

        public string Codigo { get; set; }
        public string? Nombre { get; set; }
    }
}
