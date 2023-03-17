using EventService.Features.EventFeature.CreateEvent;
using EventService.Features.EventFeature.DeleteEvent;
using EventService.Features.EventFeature.GetEventList;
using EventService.Features.EventFeature.UpdateEvent;
using EventService.Filters;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using SC.Internship.Common.ScResult;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Контроллер мероприятий
    /// </summary>
    [ApiController]
    [Route("events")]
    [TypeFilter(typeof(CommonExceptionFilter))]
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
        /// <returns></returns>
        /// <response></response>
        /// <response></response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventList()
        {
            var result = await _mediatr.Send(new GetEventListCommand());

            return Ok(result);
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
        /// <response code="200">Мероприятие</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPut]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] Event sourceEvent)
        {
            var result = await _mediatr.Send(new UpdateEventCommand(eventId, sourceEvent));

            return Ok(result);
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="eventId">ID мероприятия</param>
        /// <response></response>
        /// <response></response>
        [HttpDelete]
        [Route("{eventId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
        {
            await _mediatr.Send(new DeleteEventCommand(eventId));

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
