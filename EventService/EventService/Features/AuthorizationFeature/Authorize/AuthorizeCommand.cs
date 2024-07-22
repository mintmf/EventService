using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.AuthorizationFeature.Authorize;

/// <summary>
/// Команда авторизации
/// </summary>
public class AuthorizeCommand : IRequest<ScResult>
{
}