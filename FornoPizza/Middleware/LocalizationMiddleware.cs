using System.Globalization;

namespace FornoPizza.Middleware;

public class LocalizationMiddleware
{
    public const string CookieName = "Forno.Culture";

    private readonly RequestDelegate _next;

    public LocalizationMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var culture = ResolveCulture(context);
        CultureInfo.CurrentCulture = culture;
        CultureInfo.CurrentUICulture = culture;
        await _next(context);
    }

    private static CultureInfo ResolveCulture(HttpContext context)
    {
        if (context.Request.Cookies.TryGetValue(CookieName, out var cookieCulture))
        {
            return cookieCulture switch
            {
                "en" => new CultureInfo("en"),
                "ru" => new CultureInfo("ru"),
                _ => new CultureInfo("ru")
            };
        }

        var acceptLanguage = context.Request.Headers.AcceptLanguage.ToString();
        if (acceptLanguage.StartsWith("en", StringComparison.OrdinalIgnoreCase))
        {
            return new CultureInfo("en");
        }

        return new CultureInfo("ru");
    }
}
