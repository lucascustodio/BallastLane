using FluentAssertions;
using Moq;
using Person.Application.Commands;
using Person.Application.Services;
using Person.Application.Validators;
using Person.Domain.Aggregates.PersonAggregate.Interface;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

public class PersonServiceTests
{
    [Fact]
    public async Task Create_WithValidCommand_ReturnsPerson()
    {
        var command = new CreatePersonCommand { Name = "John Doe", CPF = "534.386.093-12", BirthDate = System.DateTime.Now };
        var validatorMock = new Mock<ICreatePersonCommandValidator>();
        validatorMock.Setup(v => v.Validate(command)).Returns(new FluentValidation.Results.ValidationResult());

        var personRepositoryMock = new Mock<IPersonRepository>();
        var personService = new PersonService(personRepositoryMock.Object, validatorMock.Object, null);

        var response = await personService.Create(command);

        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Error.Should().BeNull();
    }

    [Fact]
    public async Task Create_WithInvalidCommand_ReturnsError()
    {
        var command = new CreatePersonCommand(); 
        var validationResult = new FluentValidation.Results.ValidationResult();
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("Name", "Name is required."));
        validationResult.Errors.Add(new FluentValidation.Results.ValidationFailure("CPF", "CPF is required."));

        var validatorMock = new Mock<ICreatePersonCommandValidator>();
        validatorMock.Setup(v => v.Validate(command)).Returns(validationResult);

        var personRepositoryMock = new Mock<IPersonRepository>();
        var personService = new PersonService(personRepositoryMock.Object, validatorMock.Object, null);

        var response = await personService.Create(command);

        response.IsSuccess.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task Delete_WithExistingId_ReturnsSuccess()
    {
        var existingPersonId = "1";
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(r => r.GetByIdAsync(existingPersonId)).ReturnsAsync(new Person.Domain.Aggregates.PersonAggregate.Person());
        var personService = new PersonService(personRepositoryMock.Object, null, null);

        var response = await personService.Delete(existingPersonId);

        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Error.Should().BeNull();
    }

    [Fact]
    public async Task Delete_WithNonExistingId_ReturnsError()
    {
        var nonExistingPersonId = "999";
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(r => r.GetByIdAsync(nonExistingPersonId)).ReturnsAsync((Person.Domain.Aggregates.PersonAggregate.Person)null);
        var personService = new PersonService(personRepositoryMock.Object, null, null);

        var response = await personService.Delete(nonExistingPersonId);

        response.IsSuccess.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Error.Should().NotBeNullOrEmpty();
    }

    [Fact]
    public async Task GetAll_ReturnsListOfPersons()
    {
        var persons = new List<Person.Domain.Aggregates.PersonAggregate.Person> { new Person.Domain.Aggregates.PersonAggregate.Person(), new Person.Domain.Aggregates.PersonAggregate.Person(), new Person.Domain.Aggregates.PersonAggregate.Person() };
        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(r => r.GetAll()).ReturnsAsync(persons);
        var personService = new PersonService(personRepositoryMock.Object, null, null);

        var response = await personService.GetAll();

        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data.Should().HaveCount(persons.Count);
        response.Error.Should().BeNull();
    }

    [Fact]
    public async Task Update_WithValidCommand_ReturnsUpdatedPerson()
    {
        var existingPersonId = "1";
        var updatedPersonCommand = new UpdatePersonCommand { Id = existingPersonId, Name = "Updated Name", BirthDate = System.DateTime.Now, CPF = "583.114.284-17" };
        var existingPerson = new Person.Domain.Aggregates.PersonAggregate.Person(existingPersonId, "Old Name", "711.342.668-98", System.DateTime.Now);

        var personRepositoryMock = new Mock<IPersonRepository>();
        personRepositoryMock.Setup(r => r.GetByIdAsync(existingPersonId)).ReturnsAsync(existingPerson);
        personRepositoryMock.Setup(r => r.Update(existingPersonId, It.IsAny<Person.Domain.Aggregates.PersonAggregate.Person>())).ReturnsAsync(existingPerson);

        var validatorMock = new Mock<IUpdatePersonCommandValidator>();
        validatorMock.Setup(v => v.Validate(updatedPersonCommand)).Returns(new FluentValidation.Results.ValidationResult());

        var personService = new PersonService(personRepositoryMock.Object, null, validatorMock.Object);

        var response = await personService.Update(updatedPersonCommand);

        response.IsSuccess.Should().BeTrue();
        response.Data.Should().NotBeNull();
        response.Data.Id.Should().Be(existingPersonId);
        response.Data.Name.Should().Be(updatedPersonCommand.Name);
        response.Error.Should().BeNull();
    }

    [Fact]
    public async Task Update_WithInvalidCommand_ReturnsError()
    {
        var invalidPersonId = "999";
        var invalidCommand = new UpdatePersonCommand { Id = invalidPersonId }; 

        var personRepositoryMock = new Mock<IPersonRepository>();
        var validatorMock = new Mock<IUpdatePersonCommandValidator>();
        validatorMock.Setup(v => v.Validate(invalidCommand)).Returns(new FluentValidation.Results.ValidationResult());

        var personService = new PersonService(personRepositoryMock.Object, null, validatorMock.Object);

        var response = await personService.Update(invalidCommand);

        response.IsSuccess.Should().BeFalse();
        response.Data.Should().BeNull();
        response.Error.Should().NotBeNullOrEmpty();
    }
}
