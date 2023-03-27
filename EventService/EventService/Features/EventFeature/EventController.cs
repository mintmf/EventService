using EventService.Features.EventFeature.CreateEvent;
using EventService.Features.EventFeature.DeleteEvent;
using EventService.Features.EventFeature.GetEventList;
using EventService.Features.EventFeature.UpdateEvent;
using EventService.Filters;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SC.Internship.Common.ScResult;
using EventService.Features.EventFeature.CheckIfPlaceIsAvailable;
using Microsoft.AspNetCore.Authorization;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Контроллер мероприятий
    /// </summary>
    [ApiController]
    [Route("events")]
    [Authorize]
    [TypeFilter(typeof(CommonExceptionFilter))]
    //[Authorize]
    public class EventController : ControllerBase
    {
        private readonly IMediator _mediatr;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mediator"></param>
        public EventController(IMediator mediator)
        {
            _mediatr = mediator;
        }

        /// <summary>
        /// Получение списка всех мероприятий
        /// </summary>
        /// <returns>Список всех мероприятий</returns>
        /// <response code="200">Мероприятия</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<List<Event>>> GetEventList()
        {
            var result = await _mediatr.Send(new GetEventListCommand());

            return new ScResult<List<Event>>
            {
                Result = result
            };
        }

        /// <summary>
        /// Создание мероприятия
        /// </summary>
        /// <param name="sourceEvent">Мероприятие</param>
        /// <returns>Мероприятие или сообщение об ошибке</returns>
        /// <response code="200">Признак успешного выполнения операции</response>
        /// <response code="400">Сообщение об ошибке</response>
        [HttpPost]
        [Produces("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ScResult<Event>> CreateEvent([FromBody] Event sourceEvent)
        {
            return await _mediatr.Send(new CreateEventCommand { Event = sourceEvent });
        }

        /// <summary>
        /// Изменение мероприятия
        /// </summary>
        /// <param name="eventId">ID мероприятия</param>
        /// <param name="sourceEvent">Мероприятие</param>
        /// <returns>Измененное мероприятие</returns>
        /// <response code="200">Мероприятие</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPut]
        [Produces("application/json")]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<Event>> UpdateEvent([FromRoute] Guid eventId, [FromBody] Event sourceEvent)
        {
            var result = await _mediatr.Send(new UpdateEventCommand(eventId, sourceEvent));

            return new ScResult<Event> { Result = result};
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="eventId">ID мероприятия</param>
        /// <response code="200">Успешное удаление</response>
        /// <response code="500">Внутренняя ошибка сервера</response>
        [HttpDelete]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
        {
            await _mediatr.Send(new DeleteEventCommand(eventId));

            return StatusCode(StatusCodes.Status204NoContent);
        }

        /// <summary>
        /// Проверить место
        /// </summary>
        /// <param name="place"></param>
        /// <param name="eventId"></param>
        /// <response code="200">true или false</response>
        /// <response code="400">Сообщение об ошибке</response>
        /// <returns></returns>
        [HttpGet]
        [Route("{place}&{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<bool>> CheckPlace([FromRoute] int place, [FromRoute] Guid eventId)
        {
            var result = await _mediatr.Send(new CheckIfPlaceIsAvailableCommand { Place = place, EventId = eventId });

            return result;
        }
    }
}
