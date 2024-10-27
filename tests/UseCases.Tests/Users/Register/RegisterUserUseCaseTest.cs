﻿
using CashFlow.Application.UseCases.User.Register;
using CashFlow.Exception;
using CashFlow.Exception.ExceptionBase;
using CommonTestUtilities.Cryptography;
using CommonTestUtilities.Mapper;
using CommonTestUtilities.Repositories;
using CommonTestUtilities.Requests;
using CommonTestUtilities.Token;
using FluentAssertions;

namespace UseCases.Tests.Users.Register;
public class RegisterUserUseCaseTest
{

    [Fact]
    public async Task Sucess()
    {
        var request = RequestRegisterUserBuilder.Build();
        var useCase = CreateUseCase();

        var result = await useCase.Execute(request);

        result.Should().NotBeNull();
        result.Name.Should().Be(request.Name);
        result.Token.Should().NotBeNullOrEmpty();

    }

    [Fact]
    public async Task Error_Name_Empty()
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Name = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.NAME_REQUIRED) );
    }

    [Fact]
    public async Task Error_Email_Empty()
    {
        var request = RequestRegisterUserBuilder.Build();
        request.Email = string.Empty;

        var useCase = CreateUseCase();

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_REQUIRED));
    }
    [Fact]
    public async Task Error_Email_Already_Exist()
    {
        var request = RequestRegisterUserBuilder.Build();

        var useCase = CreateUseCase(request.Email);

        var act = async () => await useCase.Execute(request);

        var result = await act.Should().ThrowAsync<ErrorOnValidationException>();

        result.Where(ex => ex.GetErrors().Count == 1 && ex.GetErrors().Contains(ResourceErrorMessages.EMAIL_ALREADY_REGISTERED));
    }

    private RegisterUserUseCase CreateUseCase(string? email = null)
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var writeOnly = UserWriteOnlyRepositoryBuilder.Build();
        var readOnly = new UserReadOnlyRepositoryBuilder();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var jwtToken = JwtTokensGeneratorBuilder.Build();

        if(string.IsNullOrWhiteSpace(email) == false)
        {
            readOnly.ExistActiveUserWithEmail();
        }
        
        return new RegisterUserUseCase(
            mapper,
            passwordEncripter,
            readOnly.Build(),
            writeOnly,
            unitOfWork,
            jwtToken);
    }
}
