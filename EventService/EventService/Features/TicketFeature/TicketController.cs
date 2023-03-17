using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature.CheckIfUserHasATicket;
using EventService.Features.TicketFeature.GiveUserATicket;
using EventService.Filters;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature
{
    /// <summary>
    /// Контроллер билетов
    /// </summary>
    [ApiController]
    [Route("tickets")]
    [TypeFilter(typeof(CommonExceptionFilter))]
    public class TicketController : ControllerBase
    {
        private readonly IMediator _mediator;

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="mediator"></param>
        public TicketController(IMediator mediator)
        {
            _mediator = mediator;
        }

        /// <summary>
        /// Создание бесплатных билетов
        /// </summary>
        /// <returns>Список билетов</returns>
        /// <response code="200">Успех</response>
        /// <response code="400">Неверный запрос</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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
        /// <response code="200">Билет</response>
        /// <response code="400">Неверные входные даные</response>
        [HttpPost]
        [Route("{ticketId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ScResult<Ticket>> GiveUserATicket([FromRoute] Guid ticketId, [FromBody] GiveUserATicketParameters parameters)
        {
            var result = await _mediator.Send(new GiveUserATicketCommand { TicketId = ticketId, Parameters = parameters });

            return result;
        }

        /// <summary>
        /// Проверить, есть ли у пользователя билет на данное мероприятие
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="eventId">ID мероприятия</param>
        /// <returns>true или false</returns>
        /// <response code="200">Успех</response>
        /// <response code="400">Неверные входные данные</response>
        [HttpGet]
        [Route("check")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ScResult<bool>> CheckIfUserHasATicket([FromRoute] Guid userId, [FromRoute] Guid eventId)
        {
            var result = await _mediator.Send(new CheckIfUserHasATicketCommand { UserId = userId, EventId = eventId });

            return result;
        }
    }
}
