using MediatR;

namespace EventService.Features.EventFeature.GetEventList;

/// <summary>
/// Команда получения списка мероприятий
/// </summary>
public class GetEventListCommand : IRequest<List<Event>>
{
}