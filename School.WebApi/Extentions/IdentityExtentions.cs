using System.Security.Claims;
using System.Security.Principal;


namespace School.WebApi.Extentions
{
    public static class IdentityExtentions
    {
     
        public static int GetUserId(this ClaimsPrincipal claims)
        {
            if (claims != null)
            {
                var data = claims.Claims.SingleOrDefault(s => s.Type == ClaimTypes.NameIdentifier);
                if (data != null) return Convert.ToInt32(data.Value);
            }

            return default(int);
        }

        public static int GetUserId(this IPrincipal principal)
        {
            var user = (ClaimsPrincipal)principal;

            return user.GetUserId();
        }
    }
}
