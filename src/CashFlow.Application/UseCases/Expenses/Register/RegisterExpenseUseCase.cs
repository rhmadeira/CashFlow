using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

namespace CashFlow.Application.UseCases.Expenses.Register;
public class RegisterExpenseUseCase
{
    public ResponseRegisteredExpense Execute(RequestRegisterExpense request)
    {
        Validate(request);
        return new ResponseRegisteredExpense();
    }
    private void Validate(RequestRegisterExpense request)
    {
        var titleIsEmpty = string.IsNullOrWhiteSpace(request.Title);
        var dateIsValid = DateTime.Compare(request.Date, DateTime.UtcNow);
        var paymentTypeIsValid = Enum.IsDefined(typeof(PaymentType), request.PaymentType);

        if (titleIsEmpty)
        {
            throw new ArgumentException("The title is required");
        }
        if (request.Amount <= 0)
        {
            throw new ArgumentException("The amount must be greater than zero ");
        }
        if (dateIsValid > 0)
        {
            throw new ArgumentException("invalid Date");
        }
        if (!paymentTypeIsValid)
        {
            throw new ArgumentException("Payment type is not valid");
        }
    }
}
