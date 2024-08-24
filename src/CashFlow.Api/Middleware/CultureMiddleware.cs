using System.Globalization;

namespace CashFlow.Api.Middleware;
public class CultureMiddleware
{
 private readonly RequestDelegate _next;

    public CultureMiddleware(RequestDelegate next)
    {
        _next = next;
        
    }
    public async Task Invoke(HttpContext context)
    {
        var suportedLanguages =CultureInfo.GetCultures(CultureTypes.AllCultures).ToList();

        var requestedCulture = context.Request.Headers.AcceptLanguage.FirstOrDefault();

        var cultureDefault = new CultureInfo("en");

        if (!string.IsNullOrEmpty(requestedCulture) && suportedLanguages.Exists(l => l.Name.Equals(requestedCulture)))
        {
            cultureDefault = new CultureInfo(requestedCulture);
        }

        CultureInfo.CurrentCulture = cultureDefault;
        CultureInfo.CurrentUICulture = cultureDefault;

        await _next(context);

    }
}