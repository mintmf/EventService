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

        public EventController(IMediator mediator)
        {
            _mediatr = mediator;
        }
        
        /// <summary>
        /// Получение списка всех мероприятий
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        /*[SwaggerOperation(Description = "Этот метод возвращает список всех мероприятий", Summary = "Получение списка всех мероприятий")]
        [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]*/
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
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateEvent([FromRoute] Guid eventId, [FromBody] Event sourceEvent)
        {
            var result = await _mediatr.Send(new UpdateEventCommand(eventId, sourceEvent));

            return Ok(result);
        }

        /// <summary>
        /// Удаление мероприятия
        /// </summary>
        /// <param name="eventId">ID мероприятия</param>
        [HttpDelete]
        [Route("{eventId}")]
        /*[SwaggerOperation(Description = "Этот метод удаляет мероприятие", Summary = "Удаление мероприятия")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]*/
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
        {
            await _mediatr.Send(new DeleteEventCommand(eventId));

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
