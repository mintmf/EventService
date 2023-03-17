using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature.CheckIfUserHasATicket;
using EventService.Features.TicketFeature.GiveUserATicket;
using EventService.ObjectStorage;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature
{
    /// <summary>
    /// Контроллер билетов
    /// </summary>
    [ApiController]
    public class TicketController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepository;
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="ticketRepository"></param>
        /// <param name="mediator"></param>
        public TicketController(ITicketRepository ticketRepository, IMediator mediator)
        {
            _ticketRepository = ticketRepository;
            _mediator = mediator;
        }

        /// <summary>
        /// Создание бесплатных билетов
        /// </summary>
        /// <returns>Список билетов</returns>
        /// <response code="200">Успех</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPost]
        [Route("tickets")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<List<Ticket>>> AddFreeTickets([FromBody] AddFreeTicketsParameters parameters)
        {
            var result = await _mediator.Send(new AddFreeTicketsCommand { Parameters = parameters });

            return result;
        }

        /// <summary>
        /// Выдача пользователю билета
        /// </summary>
        /// <param name="parameters">ID пользователя</param>
        /// <param name="ticketId">ID билета</param>
        /// <returns>Билет</returns>
        /// <response code="200">Успех</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpPost]
        [Route("tickets/{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<Ticket>> GiveUserATicket([FromRoute] Guid ticketId, [FromBody] GiveUserATicketParameters parameters)
        {
            var result = await _mediator.Send(new GiveUserATicketCommand { Parameters = parameters });

            return result;
        }

        /// <summary>
        /// Проверить, есть ли у пользователя билет на данное мероприятие
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="eventId">ID мероприятия</param>
        /// <returns>true или false</returns>
        /// <response code="200">Успех</response>
        /// <response code="500">Внутренняя ошибка</response>
        [HttpGet]
        [Route("tickets/check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<ScResult<bool>> CheckIfUserHasATicket([FromRoute] Guid userId, [FromRoute] Guid eventId)
        {
            var result = await _mediator.Send(new CheckIfUserHasATicketCommand { UserId = userId, EventId = eventId });

            return result;
        }
    }
}
