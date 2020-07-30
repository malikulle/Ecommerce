using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.JWT
{
    public class TokenResultModel
    {
        public string token_type { get; set; }
        public string access_token { get; set; }
        public DateTime expires_in_date { get; set; }
    }
}
