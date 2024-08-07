﻿using EventService.Features.TicketFeature.AddFreeTickets;
using EventService.Features.TicketFeature.CheckIfUserHasATicket;
using EventService.Features.TicketFeature.GiveUserATicket;
using EventService.Features.TicketFeature.SellTicket;
using EventService.Filters;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SC.Internship.Common.ScResult;

namespace EventService.Features.TicketFeature;

/// <summary>
/// Контроллер билетов
/// </summary>
[ApiController]
[Route("tickets")]
[Authorize]
[TypeFilter(typeof(CommonExceptionFilter))]
public class TicketController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly ILogger<TicketController> _logger;

    /// <summary>
    /// Конструктор
    /// </summary>
    /// <param name="mediator">Медиатор</param>
    /// <param name="logger">Логгер</param>
    /// <exception cref="ArgumentNullException"></exception>
    public TicketController(IMediator mediator, ILogger<TicketController> logger)
    {
        _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
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

        _logger.LogInformation($"\nВремя: {DateTimeOffset.Now}\n" +
                               "Имя метода: POST AddFreeTickets\n" +
                               $"Параметры: {parameters.NumberOfTickets}, {parameters.EventId}\n");

        return result;
    }

    /// <summary>
    /// Выдача пользователю билета
    /// </summary>
    /// <param name="parameters">ID пользователя</param>
    /// <param name="ticketId">ID билета</param>
    /// <returns>Билет</returns>
    /// <response code="200">Билет</response>
    /// <response code="400">Неверные входные данные</response>
    [HttpPost]
    [Route("{ticketId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ScResult<Ticket>> GiveUserATicket([FromRoute] Guid ticketId, [FromBody] GiveUserATicketParameters parameters)
    {
        var result = await _mediator.Send(new GiveUserATicketCommand(parameters) { TicketId = ticketId });

        return result;
    }

    /// <summary>
    /// Проверить, есть ли у пользователя билет на данное мероприятие
    /// </summary>
    /// <param name="parameters">ID пользователя</param>
    /// <returns>true или false</returns>
    /// <response code="200">Успех</response>
    /// <response code="400">Неверные входные данные</response>
    [HttpPost]
    [Route("appointment")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ScResult<bool>> CheckIfUserHasATicket([FromBody] CheckIfUserHasATicketParameters parameters)
    {
        var result = await _mediator.Send(
            new CheckIfUserHasATicketCommand 
            { 
                UserId = parameters.UserId, 
                EventId = parameters.EventId 
            });

        return result;
    }

    /// <summary>
    /// Продать билет
    /// </summary>
    /// <param name="userId"></param>
    /// <param name="ticketId"></param>
    /// <response code="200">Успех</response>
    /// <response code="400">Неверные входные данные</response>
    /// <returns></returns>
    [HttpPost]
    [Route("{ticketId:guid}/owner/{userId:guid}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ScResult> SellTicket([FromRoute] Guid ticketId, [FromRoute] Guid userId)
    {
        var result = await _mediator.Send(new SellTicketCommand 
        { 
            TicketId = ticketId, 
            UserId =  userId 
        });

        return result;
    }
}