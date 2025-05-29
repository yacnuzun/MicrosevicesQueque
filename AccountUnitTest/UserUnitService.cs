using AccountApi.Application.Services.Implementations;
using AccountApi.Application.Services.Interfaces;
using AccountApi.Domain.Entities;
using AccountApi.Infrastructure.Repositories.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Shared.Persistance.Interfaces;
using System;
using System.Linq.Expressions;

namespace AccountUnitTest
{
    public class UserUnitService
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IUserOperationClaimService> _userOperationClaimServiceMock;
        private readonly Mock<IOperationClaimService> _operationClaimServiceMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly Mock<ILogger<UserManager>> _loggerMock;
        private readonly UserManager _userManager;

        public UserUnitService()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _userOperationClaimServiceMock = new Mock<IUserOperationClaimService>();
            _operationClaimServiceMock = new Mock<IOperationClaimService>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _loggerMock = new Mock<ILogger<UserManager>>();
            _userManager = new UserManager(_userRepositoryMock.Object,_unitOfWorkMock.Object,_loggerMock.Object, _userOperationClaimServiceMock.Object, _operationClaimServiceMock.Object);
        }

        [Fact]
        public async Task GetById_ReturnsSuccessDataResult_WhenUserExists()
        {
            // Arrange
            int userId = 1;
            var user = new User { Id = userId, Status = true };
            _userRepositoryMock.Setup(repo => repo.GetAsync(u => u.Id == userId && u.Status == true))
                .ReturnsAsync(user);

            // Act
            var result = await _userManager.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.True(result.Success);
            Assert.Equal(userId, result.Data.Id);
        }

        [Fact]
        public async Task GetById_ReturnsErrorDataResult_WhenUserDoesNotExist()
        {
            // Arrange
            int userId = 1;
            _userRepositoryMock.Setup(repo => repo.GetAsync(u => u.Id == userId && u.Status == true))
                .ReturnsAsync((User)null);

            // Act
            var result = await _userManager.GetById(userId);

            // Assert
            Assert.NotNull(result);
            Assert.False(result.Success);
            Assert.Null(result.Data);
        }

        [Fact]
        public async Task GetById_ThrowsException_LogsError()
        {
            // Arrange
            int userId = 1;
            var exception = new System.Exception("Test exception");
            _userRepositoryMock.Setup(repo => repo.GetAsync(r=>r.Id == userId && r.Status == true))
                .ThrowsAsync(exception);

            // Act & Assert
            await Assert.ThrowsAsync<System.Exception>(() => _userManager.GetById(userId));
            _userRepositoryMock.Verify(s=>s.GetAsync(u=> u.Id == userId && u.Status == true), Times.Once());
            _loggerMock.Verify(
                x => x.Log(
                    LogLevel.Error,
                    It.IsAny<EventId>(),
                    It.Is<It.IsAnyType>((v, t) =>
                        v.ToString().Contains("Test exception")),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<It.IsAnyType, Exception, string>>()
                ),
                Times.Once()
            );
        }

    }
}