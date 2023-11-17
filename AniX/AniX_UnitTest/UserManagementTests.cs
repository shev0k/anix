using Xunit;
using Moq;
using AniX_Shared.Interfaces;
using AniX_Shared.DomainModels;
using System;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Anix_Shared.DomainModels;
using AniX_Utility;
using Assert = Xunit.Assert;

namespace AniX_UnitTest
{
    public class UserManagementTests
    {
        private readonly Mock<IUserManagement> _mockRepo;
        private readonly User _testUser;

        public UserManagementTests()
        {
            _mockRepo = new Mock<IUserManagement>();
            _testUser = new User
            {
                Id = 1,
                Username = "TestUser",
                Email = "test@example.com",
                RegistrationDate = DateTime.Now,
                Banned = false,
                IsAdmin = false,
                ProfileImagePath = "path/to/image"
            };
        }

        [Fact]
        public async Task CreateAsync_Success()
        {
            _mockRepo.Setup(x => x.CreateAsync(It.IsAny<User>(), null, null))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockRepo.Object.CreateAsync(_testUser);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task CreateAsync_Failure()
        {
            _mockRepo.Setup(x => x.CreateAsync(It.IsAny<User>(), null, null))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockRepo.Object.CreateAsync(_testUser);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task UpdateAsync_Success()
        {
            _mockRepo.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<bool>()))
                .ReturnsAsync(true);

            var result = await _mockRepo.Object.UpdateAsync(_testUser, true);

            Assert.True(result);
        }

        [Fact]
        public async Task UpdateAsync_Failure()
        {
            _mockRepo.Setup(x => x.UpdateAsync(It.IsAny<User>(), It.IsAny<bool>()))
                .ReturnsAsync(false);

            var result = await _mockRepo.Object.UpdateAsync(_testUser, false);

            Assert.False(result);
        }

        [Fact]
        public async Task DeleteAsync_Success()
        {
            _mockRepo.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = true });

            var result = await _mockRepo.Object.DeleteAsync(1);

            Assert.True(result.Success);
        }

        [Fact]
        public async Task DeleteAsync_Failure()
        {
            _mockRepo.Setup(x => x.DeleteAsync(It.IsAny<int>()))
                .ReturnsAsync(new OperationResult { Success = false });

            var result = await _mockRepo.Object.DeleteAsync(999);

            Assert.False(result.Success);
        }

        [Fact]
        public async Task GetUserFromIdAsync_Success()
        {
            _mockRepo.Setup(x => x.GetUserFromIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_testUser);

            var result = await _mockRepo.Object.GetUserFromIdAsync(1);

            Assert.NotNull(result);
            Assert.Equal("TestUser", result.Username);
        }

        [Fact]
        public async Task GetUserFromIdAsync_Failure()
        {
            _mockRepo.Setup(x => x.GetUserFromIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            var result = await _mockRepo.Object.GetUserFromIdAsync(999);

            Assert.Null(result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_Success()
        {
            _mockRepo.Setup(x => x.AuthenticateUserAsync("TestUser", "password"))
                .ReturnsAsync(_testUser);

            var result = await _mockRepo.Object.AuthenticateUserAsync("TestUser", "password");

            Assert.NotNull(result);
        }

        [Fact]
        public async Task AuthenticateUserAsync_Failure()
        {
            _mockRepo.Setup(x => x.AuthenticateUserAsync("WrongUser", "password"))
                .ReturnsAsync((User)null);

            var result = await _mockRepo.Object.AuthenticateUserAsync("WrongUser", "password");

            Assert.Null(result);
        }

        [Fact]
        public async Task FetchFilteredAndSearchedUsersAsync_ReturnsUsers()
        {
            _mockRepo.Setup(x => x.FetchFilteredAndSearchedUsersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<User> { _testUser });

            var result = await _mockRepo.Object.FetchFilteredAndSearchedUsersAsync("filter", "searchTerm");

            Assert.Single(result);
        }

        [Fact]
        public async Task FetchFilteredAndSearchedUsersAsync_ReturnsEmpty()
        {
            _mockRepo.Setup(x => x.FetchFilteredAndSearchedUsersAsync(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(new List<User>());

            var result = await _mockRepo.Object.FetchFilteredAndSearchedUsersAsync("filter", "searchTerm");

            Assert.Empty(result);
        }

        [Fact]
        public async Task DoesUsernameExistAsync_True()
        {
            _mockRepo.Setup(x => x.DoesUsernameExistAsync("TestUser"))
                .ReturnsAsync(true);

            var exists = await _mockRepo.Object.DoesUsernameExistAsync("TestUser");

            Assert.True(exists);
        }

        [Fact]
        public async Task DoesUsernameExistAsync_False()
        {
            _mockRepo.Setup(x => x.DoesUsernameExistAsync("newUser"))
                .ReturnsAsync(false);

            var exists = await _mockRepo.Object.DoesUsernameExistAsync("newUser");

            Assert.False(exists);
        }

        [Fact]
        public async Task DoesEmailExistAsync_True()
        {
            _mockRepo.Setup(x => x.DoesEmailExistAsync("test@example.com"))
                .ReturnsAsync(true);

            var exists = await _mockRepo.Object.DoesEmailExistAsync("test@example.com");

            Assert.True(exists);
        }

        [Fact]
        public async Task DoesEmailExistAsync_False()
        {
            _mockRepo.Setup(x => x.DoesEmailExistAsync("newemail@example.com"))
                .ReturnsAsync(false);

            var exists = await _mockRepo.Object.DoesEmailExistAsync("newemail@example.com");

            Assert.False(exists);
        }

    }
}