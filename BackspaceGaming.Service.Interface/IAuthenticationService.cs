using BackspaceGaming.Entity.Model;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;

namespace BackspaceGaming.Service.Interface
{
    public interface IAuthenticationService: IServiceBase<Authentication>
    {
        Task<dynamic> GetJWTToken(AuthenticationBodyModel authenticationBody);
    }
}