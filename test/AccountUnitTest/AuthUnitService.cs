using AccountApi.Application.Services.Implementations;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Domain.Enums;
using AccountApi.Dto_s;
using AccountApi.Infrastructure.Helpers.JWT;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Helpers.ResponseModels.GenericResultModels;
using Shared.Helpers.Security.Hashing;

namespace AccountUnitTest
{
    public class AuthUnitService
    {
        private readonly Mock<IUserService> _userServiceMock;
        private readonly Mock<ITokenHelper> _tokenHelperMock;
        private readonly Mock<ILogger<AuthManager>> _logmock;
        private readonly AuthManager _authManagerMock;

        public AuthUnitService()
        {
            _userServiceMock = new Mock<IUserService>();
            _tokenHelperMock = new Mock<ITokenHelper>();
            _logmock = new Mock<ILogger<AuthManager>>();
            _authManagerMock = new AuthManager(_userServiceMock.Object,_tokenHelperMock.Object, _logmock.Object);
        }

        [Fact]
        public async Task Register_ShouldReturnSuccess_WhenUserIsRegistered()
        {
            var registerDto = new UserForRegisterDto
            {
                Email = "asd@asd.com",
                Password = "123456",
                UserName = "TestUser",
                Role    = UserRoles.Admin,
                UserTaxId = "12345678910",
            };
            _userServiceMock.Setup(u=>u.Add(It.IsAny<User>(),registerDto.Role)).Verifiable();

            var result = await _authManagerMock.Register(registerDto);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.UserName.Should().Be("TestUser");
            _userServiceMock.Verify(u=> u.Add(It.IsAny<User>(),registerDto.Role),Times.Once());
        }
        [Fact]
        public async Task Login_shouldReturnSuccess_WhenUserIsLogin()
        {
            var loginDto = new UserForLoginDto
            {
                Password = "password",
                UserName = "TestUser",
                UserTaxID = "12345678910"
            };

            byte[] passwordHash, passwordSalt;

            HashingHelper.CreatePasswordHash(loginDto.Password,out passwordHash,out passwordSalt);

            var fakeUser = new User
            {
                UserTaxID = loginDto.UserTaxID,
                UserName = loginDto.UserName,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt,
                Status = true

            };

            _userServiceMock.Setup(u => u.GetByUserTaxId(loginDto.UserTaxID)).ReturnsAsync(new SuccessDataResult<User>(fakeUser));

            var result = await _authManagerMock.Login(loginDto);

            result.Should().NotBeNull();
            result.Success.Should().BeTrue();
            result.Data.Should().NotBeNull();
            result.Data.UserName.Should().Be("TestUser");


            _userServiceMock.Verify(u=> u.GetByUserTaxId(loginDto.UserTaxID), Times.Once());
        }
    }
}