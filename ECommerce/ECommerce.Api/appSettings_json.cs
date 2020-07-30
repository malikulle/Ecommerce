using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api
{
    public class appSettings_json
    {
        public string JWTKey_Issuer { get; set; }
        public string JWTKey_Audience { get; set; }
        public string JWTKey_SigningKey { get; set; }
        public string FrontUrl { get; set; }
    }
}
