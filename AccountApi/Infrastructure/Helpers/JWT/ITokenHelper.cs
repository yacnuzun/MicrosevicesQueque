using AccountApi.Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace AccountApi.Infrastructure.Helpers.JWT
{
    public interface ITokenHelper
    {
        AccessToken CreateToken(User user, List<OperationClaim> operationClaims);
        JwtSecurityToken ValidateTokenGetClaims(string jwtToken);

    }
}
