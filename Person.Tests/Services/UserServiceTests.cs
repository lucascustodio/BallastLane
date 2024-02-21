using FluentAssertions;
using Moq;
using Person.Application.Commands;
using Person.Application.Services;
using Person.Application.Validators;
using Person.Domain.Aggregates.UserAggregate;
using Person.Domain.Aggregates.UserAggregate.Interface;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Person.Application.Tests.Services
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Create_ValidCommand_ReturnsSuccessResponseWithUser()
        {
            var createUserCommand = new CreateUserCommand { Name = "Test User", Email = "test@example.com", Password = "password" };
            var user = new User(createUserCommand.Name, createUserCommand.Email, createUserCommand.Password);
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.AddAsync(It.IsAny<User>())).ReturnsAsync(user);
            var createUserValidatorMock = new Mock<ICreateUserCommandValidator>();
            createUserValidatorMock.Setup(v => v.Validate(createUserCommand)).Returns(new FluentValidation.Results.ValidationResult());

            var userService = new UserService(userRepositoryMock.Object, createUserValidatorMock.Object, null);

            var response = await userService.Create(createUserCommand);

            response.IsSuccess.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().BeOfType<User>();
            response.Error.Should().BeNull();
        }

        [Fact]
        public async Task Create_InvalidCommand_ReturnsErrorResponse()
        {
            var createUserCommand = new CreateUserCommand();
            var userRepositoryMock = new Mock<IUserRepository>();
            var createUserValidatorMock = new Mock<ICreateUserCommandValidator>();
            createUserValidatorMock.Setup(v => v.Validate(createUserCommand)).Returns(new FluentValidation.Results.ValidationResult { Errors = { new FluentValidation.Results.ValidationFailure("Property", "Error message") } });

            var userService = new UserService(userRepositoryMock.Object, createUserValidatorMock.Object, null);

            var response = await userService.Create(createUserCommand);

            response.IsSuccess.Should().BeFalse();
            response.Data.Should().BeNull();
            response.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Delete_ExistingId_ReturnsSuccessResponse()
        {
            var existingUserId = "1";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetByIdAsync(existingUserId)).ReturnsAsync(new User("Test", "test@example.com", "password"));

            var userService = new UserService(userRepositoryMock.Object, null, null);

            var response = await userService.Delete(existingUserId);

            response.IsSuccess.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Error.Should().BeNull();
        }

        [Fact]
        public async Task GetAll_ReturnsAllUsers()
        {
            var users = new List<User>
            {
                new User("User1", "user1@example.com", "password1"),
                new User("User2", "user2@example.com", "password2")
            };
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(users);

            var userService = new UserService(userRepositoryMock.Object, null, null);

            var response = await userService.GetAll();

            response.IsSuccess.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().BeEquivalentTo(users);
            response.Error.Should().BeNull();
        }

        [Fact]
        public async Task GetById_ExistingId_ReturnsUser()
        {
            var userId = "1";
            var user = new User("Test User", "test@example.com", "password");
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetByIdAsync(userId)).ReturnsAsync(user);

            var userService = new UserService(userRepositoryMock.Object, null, null);

            var response = await userService.GetById(userId);

            response.IsSuccess.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Should().Be(user);
            response.Error.Should().BeNull();
        }

        [Fact]
        public async Task GetById_NonExistingId_ReturnsError()
        {
            var nonExistingUserId = "999";
            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetByIdAsync(nonExistingUserId)).ReturnsAsync((User)null);

            var userService = new UserService(userRepositoryMock.Object, null, null);

            var response = await userService.GetById(nonExistingUserId);

            response.IsSuccess.Should().BeFalse();
            response.Data.Should().BeNull();
            response.Error.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task Update_ExistingUser_ReturnsUpdatedUser()
        {
            var updateUserCommand = new UpdateUserCommand { Id = "1", Name = "Updated User", Email = "updated@example.com" };
            var existingUser = new User("Test User", "test@example.com", "password");

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetByIdAsync(updateUserCommand.Id)).ReturnsAsync(existingUser);
            userRepositoryMock.Setup(r => r.Update(existingUser.Id, existingUser)).ReturnsAsync(existingUser);

            var updateUserValidatorMock = new Mock<IUpdateUserCommandValidator>();
            updateUserValidatorMock.Setup(v => v.Validate(updateUserCommand)).Returns(new FluentValidation.Results.ValidationResult());

            var userService = new UserService(userRepositoryMock.Object, null, updateUserValidatorMock.Object);

            var response = await userService.Update(updateUserCommand);

            response.IsSuccess.Should().BeTrue();
            response.Data.Should().NotBeNull();
            response.Data.Name.Should().Be(updateUserCommand.Name);
            response.Data.Email.Should().Be(updateUserCommand.Email);
            response.Error.Should().BeNull();
        }

        [Fact]
        public async Task Update_NonExistingUser_ReturnsError()
        {
            var updateUserCommand = new UpdateUserCommand { Id = "999", Name = "Updated User", Email = "updated@example.com" };

            var userRepositoryMock = new Mock<IUserRepository>();
            userRepositoryMock.Setup(r => r.GetByIdAsync(updateUserCommand.Id)).ReturnsAsync((User)null);

            var updateUserValidatorMock = new Mock<IUpdateUserCommandValidator>();
            updateUserValidatorMock.Setup(v => v.Validate(updateUserCommand)).Returns(new FluentValidation.Results.ValidationResult());

            var userService = new UserService(userRepositoryMock.Object, null, updateUserValidatorMock.Object);

            var response = await userService.Update(updateUserCommand);

            response.IsSuccess.Should().BeFalse();
            response.Data.Should().BeNull();
            response.Error.Should().NotBeNullOrEmpty();
        }
    }
}
