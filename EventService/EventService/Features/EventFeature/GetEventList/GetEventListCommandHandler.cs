using EventService.ObjectStorage;
using MediatR;
using JetBrains.Annotations;

namespace EventService.Features.EventFeature.GetEventList;

/// <summary>
/// Класс обработчика команды получения списка всех мероприятий
/// </summary>
[UsedImplicitly]
public class GetEventListCommandHandler : IRequestHandler<GetEventListCommand, List<Event>>
{
    private readonly IEventRepository _eventRepository;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="eventRepository">Репозиторий мероприятий</param>
    public GetEventListCommandHandler(IEventRepository eventRepository)
    {
        _eventRepository = eventRepository ?? throw new ArgumentNullException(nameof(eventRepository));
    }

    /// <summary>
    /// Обработчик команды получения списка всех мероприятий
    /// </summary>
    /// <param name="command">Команда получения списка всех мероприятий</param>
    /// <param name="cancellationToken">Токен отмены</param>
    /// <returns>Список всех мероприятий</returns>
    public async Task<List<Event>> Handle(GetEventListCommand command, CancellationToken cancellationToken)
    {
        return await _eventRepository.GetEventListAsync();
    }
}