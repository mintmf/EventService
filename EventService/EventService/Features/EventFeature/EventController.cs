using EventService.Features.EventFeature.CreateEvent;
using EventService.Features.EventFeature.DeleteEvent;
using EventService.Features.EventFeature.GetEventList;
using EventService.Features.EventFeature.UpdateEvent;
using Microsoft.AspNetCore.Mvc;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;

namespace EventService.Features.EventFeature
{
    /// <summary>
    /// Контроллер мероприятий
    /// </summary>
    public class EventController : Controller
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
        [Route("events")]
        [SwaggerOperation(Description = "Этот метод возвращает список всех мероприятий", Summary = "Получение списка всех мероприятий")]
        [ProducesResponseType(typeof(List<Event>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetEventList()
        {
            var result = await _mediatr.Send(new GetEventListCommand());

            return Ok(result);
        }

        /// <summary>
        /// Создание мероприятия
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("events")]
        [SwaggerOperation(Description = "Этот метод создает новое мероприятие", Summary = "Создание мероприятия")]
        [ProducesResponseType(typeof(Event), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(string), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateEvent([FromBody] Event sourceEvent)
        {
            try
            {
                var result = await _mediatr.Send(new CreateEventCommand { Event = sourceEvent });

                if (result.ValidationResult.IsValid)
                {
                    return StatusCode(StatusCodes.Status201Created, result.Event);
                }

                var errorMessage = String.Join("; ", result.ValidationResult.Errors.Select(x => x.ErrorMessage));

                return BadRequest(errorMessage);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        /// <summary>
        /// Изменение мероприятия
        /// </summary>
        /// <param name="eventId"></param>
        /// <param name="sourceEvent"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("events/{eventId}")]
        [SwaggerOperation(Description = "Этот метод изменяет мероприятие", Summary = "Изменение мероприятия")]
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
        /// <param name="eventId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("events/{eventId}")]
        [SwaggerOperation(Description = "Этот метод удаляет мероприятие", Summary = "Удаление мероприятия")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(typeof(string), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteEvent([FromRoute] Guid eventId)
        {
            await _mediatr.Send(new DeleteEventCommand(eventId));

            return StatusCode(StatusCodes.Status204NoContent);
        }
    }
}
