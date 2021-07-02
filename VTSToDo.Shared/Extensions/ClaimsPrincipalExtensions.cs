using System;
using System.Security.Claims;

namespace VTSToDo.Shared.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var userIdString = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if (!string.IsNullOrWhiteSpace(userIdString))
            {
                return int.Parse(userIdString);
            }
            return -1;
        }
    }
}
