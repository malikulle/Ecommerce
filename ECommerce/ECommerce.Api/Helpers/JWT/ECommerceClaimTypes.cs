using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ECommerce.Api.JWT
{
    /// <summary>
    /// Calims
    /// </summary>
    public class ECommerceClaimTypes
    {
        public static string UserId { get; set; } = ClaimTypes.NameIdentifier;
        public static string Name { get; set; } = ClaimTypes.Name;
        public static string Surname { get; set; } = ClaimTypes.Surname;
        public static string Email { get; set; } = ClaimTypes.Email;
        public static string Role { get; set; } = ClaimTypes.Role;
    }
}
