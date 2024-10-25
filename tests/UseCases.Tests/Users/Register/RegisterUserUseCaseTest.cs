﻿
using CashFlow.Application.UseCases.User.Register;
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

    private RegisterUserUseCase CreateUseCase()
    {
        var mapper = MapperBuilder.Build();
        var unitOfWork = UnitOfWorkBuilder.Build();
        var userWriteOnlyRepositoryBuilder = UserWriteOnlyRepositoryBuilder.Build();
        var useReadOnlyRepositoryBuilder = new UserReadOnlyRepositoryBuilder().Build();
        var passwordEncripter = PasswordEncripterBuilder.Build();
        var jwtToken = JwtTokensGeneratorBuilder.Build();

        return new RegisterUserUseCase(
            mapper,
            passwordEncripter,
            useReadOnlyRepositoryBuilder,
            userWriteOnlyRepositoryBuilder,
            unitOfWork,
            jwtToken);
    }
}
