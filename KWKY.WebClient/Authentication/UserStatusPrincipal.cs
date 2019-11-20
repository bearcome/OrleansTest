using System.Security.Claims;
using System.Security.Principal;

namespace KWKY.WebClient.Authentication
{
    public class UserStatusPrincipal : ClaimsPrincipal
    {
        public UserStatusPrincipal (IIdentity Identity, string userId, string platformKey) : base(Identity)
        {
            UserId = userId;
            PlatformKey = platformKey;
        }

        public string UserId { get; }
        public string PlatformKey { get; }

    }
}
