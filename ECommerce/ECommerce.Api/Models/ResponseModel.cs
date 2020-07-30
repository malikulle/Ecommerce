using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerce.Api.Models
{
    /// <summary>
    /// API Response model
    /// </summary>
    public class ResponseModel
    {
        /// <summary>
        /// ex .  200 ,400
        /// </summary>
        public int Status { get; set; }
        /// <summary>
        /// ex . true,false
        /// </summary>
        public bool Result { get; set; }
        /// <summary>
        /// string
        /// </summary>
        public string Message { get; set; }

    }
}
