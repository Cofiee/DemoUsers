using DemoUsers.Server.Users.Data;
using DemoUsers.Server.Users.Dtos;
using DemoUsers.Server.Users.Features.Create;
using FakeItEasy;
using FluentAssertions;
using Microsoft.Extensions.Logging;

namespace TestProject
{
    public class CreateUserCommandHandlerTests
    {
        CreateUserCommandHandler _handler;

        ILogger<CreateUserCommandHandler> _logger;
        IUsersRepository _usersRepository;

        [SetUp]
        public void Setup()
        {
            _logger = A.Fake<ILogger<CreateUserCommandHandler>>();
            _usersRepository = A.Fake<IUsersRepository>();
            _handler = new CreateUserCommandHandler(_logger, _usersRepository);
        }

        [TestCase(1)]
        [TestCase(0)]
        public async Task CreateUser_ReturnsId(int expectedId)
        {
            var user = new User();
            var command = new CreateUserCommand(user);
            A.CallTo(() => _usersRepository.CreateUserAsync(user, A<CancellationToken>._)).Returns(Task.FromResult(expectedId));
            
            // Act
            var result = await _handler.Handle(command, CancellationToken.None);
            result.Should().Be(expectedId);
        }

        [Test]
        public async Task CreateUser_RepositoryThrowsException_RethrowsException()
        {
            var userId = 0;
            var user = new User();
            var command = new CreateUserCommand(user);
            A.CallTo(() => _usersRepository.CreateUserAsync(user, A<CancellationToken>._)).Throws(new Exception("Test exception"));

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}
