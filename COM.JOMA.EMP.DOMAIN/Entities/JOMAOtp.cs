using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace COM.JOMA.EMP.DOMAIN.Entities
{
    public class JOMAOtp
    {
        public string Otp { get; set; }
        public DateTime Expiry { get; set; }
        public int Attempts { get; set; }
    }
}
