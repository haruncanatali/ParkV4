using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ParkV4.Application.Common.Models
{
    public class TokenSetting
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int TokenValidityTime { get; set; }
        public int RefreshTokenValidityTime { get; set; }
    }
}