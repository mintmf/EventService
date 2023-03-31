using FluentValidation;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Filters;

/// <summary>
/// Фильтр исключений
/// </summary>
public class CommonExceptionFilter : IExceptionFilter
{
    /// <summary>
    /// Обработка исключения
    /// </summary>
    /// <param name="context"></param>
    public void OnException(ExceptionContext context)
    {
        switch (context.Exception)
        {
            case ValidationException validationException:
            {
                var result = new ScResult
                {
                    Error = new ScError
                    {
                        Message = "Валидация не успешная",
                        ModelState = validationException.Errors
                            .GroupBy(x => x.PropertyName)
                            .ToDictionary(
                                x => x.Key,
                                x => x.Select(e => e.ErrorMessage).ToList())
                    }
                };

                context.Result = new JsonResult(result)
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                return;
            }
            case ScException:
                context.Result = new JsonResult(new ScResult(new ScError { Message = context.Exception.Message }))
                {
                    StatusCode = StatusCodes.Status400BadRequest
                };

                return;
            default:
                context.Result = new ContentResult
                {
                    Content = context.Exception.ToString(),
                    StatusCode = StatusCodes.Status500InternalServerError
                };
                break;
        }
    }
}