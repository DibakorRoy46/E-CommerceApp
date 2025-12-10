using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Notification.API.Filters;

public class ApiExceptionFilter : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(new
        {
            Message = context.Exception.Message,
            StackTrace = context.Exception.StackTrace
        })
        {
            StatusCode = 500
        };
    }
}