using System.Security.Claims;
using static FootballApp.Common.GeneralConstants;

namespace FootballApp.Infrastructure.Extensions
{
    public static class ClaimsExtensions
    {
        public static bool IsAdmin(this ClaimsPrincipal user)
        {
            return user.IsInRole(AdminRoleName);
        }
    }
}
