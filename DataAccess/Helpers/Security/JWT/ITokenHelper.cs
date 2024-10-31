using Shared.Entities;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Shared.Helpers.Security.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        JwtSecurityToken ValidateTokenGetClaims(string jwtToken);

    }
}
