using FluentAssertions;
using Moq;
using Person.Application.Services;
using Person.Domain.Aggregates.UserAggregate;
using Person.Domain.Aggregates.UserAggregate.Interface;
using System;
using System.Threading.Tasks;
using Xunit;

public class LoginServiceTest
{
    [Fact]
    public async Task Login_WithValidCredentials_ReturnsToken()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var loginService = new LoginService(mockUserRepository.Object);

        var id = Guid.NewGuid().ToString();
        var username = "testuser";
        var password = "password";
        var email = "email@email.com";
        var user = new User(id, username, email, password);

        mockUserRepository.Setup(repo => repo.GetByEmail(email)).ReturnsAsync(user);

        var token = await loginService.Login(email, password);

        token.Should().NotBeNull();
    }

    [Fact]
    public async Task Login_WithInvalidCredentials_ReturnsNull()
    {
        var mockUserRepository = new Mock<IUserRepository>();
        var loginService = new LoginService(mockUserRepository.Object);

        var username = "testuser";
        var password = "invalid_password";

        mockUserRepository.Setup(repo => repo.GetByEmail(username)).ReturnsAsync((User)null);

        var token = await loginService.Login(username, password);

        token.Should().BeNull();
    }
}
