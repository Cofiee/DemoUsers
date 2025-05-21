using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using DemoUsers.Server.Users.Features.DeleteUser;
using DemoUsers.Server.Users.Features.EditUser;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using NUnit.Framework.Internal;

namespace TestProject
{
    public class DeleteUserCommandHandlerTests
    {
        DeleteUserCommandHandler _handler;

        ILogger<DeleteUserCommandHandler> _logger;
        IUsersRepository _usersRepository;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger<DeleteUserCommandHandler>>();
            _usersRepository = A.Fake<IUsersRepository>();
            _handler = new DeleteUserCommandHandler(_logger, _usersRepository);
        }

        [Test]
        public async Task DeleteUser_ReturnsTrue()
        {
            var userId = 1;
            var user = new User { Id = userId };
            var command = new DeleteUserCommand(userId);
            A.CallTo(() => _usersRepository.GetUserAsync(userId, A<CancellationToken>._)).Returns(Task.FromResult(user));
            A.CallTo(() => _usersRepository.DeleteUserAsync(userId, A<CancellationToken>._)).Returns(Task.FromResult(true));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeTrue();
        }

        [Test]
        public async Task DeleteUser_RepositoryError_ReturnsFalse()
        {
            var userId = 1;
            var user = new User { Id = userId };
            var command = new DeleteUserCommand(userId);
            A.CallTo(() => _usersRepository.GetUserAsync(userId, A<CancellationToken>._)).Returns(Task.FromResult(user));
            A.CallTo(() => _usersRepository.DeleteUserAsync(userId, A<CancellationToken>._)).Returns(Task.FromResult(false));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }

        [Test]
        public async Task DeleteNonExistingUser_ReturnsFalse()
        {
            var user = new User { Id = -1 };
            var command = new DeleteUserCommand(user.Id);
            A.CallTo(() => _usersRepository.GetUserAsync(user.Id, A<CancellationToken>._)).Returns(Task.FromResult<User>(null));

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            result.Should().BeFalse();
        }

        [Test]
        public async Task DeleteUser_RepositoryThrowsException_RethrowsException()
        {
            var userId = 1;
            var user = new User { Id = userId };
            var command = new DeleteUserCommand(userId);
            A.CallTo(() => _usersRepository.GetUserAsync(userId, A<CancellationToken>._)).Returns(Task.FromResult(user));
            A.CallTo(() => _usersRepository.DeleteUserAsync(userId, A<CancellationToken>._)).Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
