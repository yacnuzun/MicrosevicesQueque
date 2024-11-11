using AccountApi.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace AccountApi.Helpers.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        JwtSecurityToken ValidateTokenGetClaims(string jwtToken);

    }
}
