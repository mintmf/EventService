using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature.GiveUserATicket;
using Microsoft.AspNetCore.Mvc;

namespace EventService.Features.TicketFeature
{
    public class TicketController : Controller
    {
        /// <summary>
        /// Создание бесплатных билетов
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Route("tickets")]
        public async Task<IActionResult> AddFreeTickets([FromBody] AddFreeTicketsParameters parameters)
        {
            return Ok();
        }

        /// <summary>
        /// Выдача пользователю билета
        /// </summary>
        /// <param name="parameters"></param>
        /// <param name="ticketId"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("tickets/{ticketId}")]
        public async Task<IActionResult> GiveUserATicket([FromRoute] Guid ticketId, [FromBody] GiveUserATicketParameters parameters)
        {
            return Ok();
        }

        /// <summary>
        /// Проверить, есть ли у пользователя билет на данное мероприятие
        /// </summary>
        /// <param name="userId">ID пользователя</param>
        /// <param name="eventId">ID мероприятия</param>
        /// <returns></returns>
        [HttpGet]
        [Route("tickets/check")]
        public async Task<IActionResult> CheckIfUserHasATicket([FromRoute] Guid userId, [FromRoute] Guid eventId)
        {
            return Ok();
        }
    }
}
