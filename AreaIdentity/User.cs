using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Currency.AreaIdentity
{
    public class User : IdentityUser
    {
        public int ForeignYear { get; set; }
        
    }
}
