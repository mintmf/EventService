using EventService.ObjectStorage;
using JetBrains.Annotations;
using MediatR;
using SC.Internship.Common.Exceptions;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature.CheckIfUserHasATicket;

/// <summary>
/// Класс обработчика команды проверки на то, есть ли у пользователя билет
/// </summary>
[UsedImplicitly]
public class CheckIfUserHasATicketCommandHandler : IRequestHandler<CheckIfUserHasATicketCommand, ScResult<bool>>
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository"></param>
    public CheckIfUserHasATicketCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository;
    }

    /// <summary>
    /// Обработчик команды проверки на то, есть ли у пользователя билет
    /// </summary>
    /// <param name="request"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public async Task<ScResult<bool>> Handle(CheckIfUserHasATicketCommand request, CancellationToken cancellationToken)
    {
        var foundEvent = await  _eventRepository.GetEventAsync(request.EventId);

        if (foundEvent == null)
        {
            throw new ScException("Такого мероприятия не существует");
        }
        var ticketOwner = foundEvent.Tickets?.Find(t => t.Owner == request.UserId);

        return new ScResult<bool>
        {
            Result = ticketOwner != null
        };
    }
}