using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Linq;
using System.Text;

namespace MyAPI.CrossCutting.ExtensionMethods
{
    public static class ClaimsPrincipalExtensions
    {
        public static long? GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return GetId(claimsPrincipal, "user_id");
        }

        public static long? GetOrganizationId(this ClaimsPrincipal claimsPrincipal)
        {
            return GetId(claimsPrincipal, "organizationId");
        }

        private static long? GetId(ClaimsPrincipal claimsPrincipal, string claimType)
        {
            int id;

            var idType = claimsPrincipal.Claims.FirstOrDefault(c => c.Type == claimType);

            if (idType == null)
                return null;

            if (!int.TryParse(idType.Value, out id))
                return null;

            return id;
        }
    }
}
