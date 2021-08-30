using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Programmania.Models
{
    public class AppSettings
    {
        public string SecretCode { get; set; }

        public int JwtTokenExpirationMins { get; set; }

        public int RTExpirationDays { get; set; }
    }
}
