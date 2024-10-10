using AutoMapper;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses.User;
using CashFlow.Exception.ExceptionBase;

namespace CashFlow.Application.UseCases.User.Register;

internal class RegisterUserUseCase : IRegisterUserUseCase
{
    private readonly IMapper _mapper;

    public RegisterUserUseCase(IMapper mapper)
    {
        _mapper = mapper;
    }

    public async Task<ResponseRegisterUser> Execute(RequestRegisterUser request)
    {
        Validate(request);

        var user = _mapper.Map<Domain.Entities.User>(request);


        return new ResponseRegisterUser
        {
            Name = user.Name,
        };
    }
    private void Validate(RequestRegisterUser request)
    {
        var validator = new RegisterUserValidator();

        var result = validator.Validate(request);

        if (!result.IsValid)
        {

            var errorMessages = result.Errors
                .Select(f => f.ErrorMessage)
                .ToList();

            throw new ErrorOnValidationException(errorMessages);
        }

    }


}
