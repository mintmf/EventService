using EventService.Features.EventFeature;
using EventService.Features.EventFeature.CreateEvent;
using EventService.ObjectStorage;
using FluentValidation;
using NSubstitute;
using SC.Internship.Common.ScResult;

namespace EventService.UnitTests;

public class CreateEventTest
{
    private readonly IEventRepository _mockRepository;
    private readonly CreateEventCommandHandler _handler;
    public CreateEventTest()
    {
        _mockRepository = Substitute.For<IEventRepository>();
        _handler = new CreateEventCommandHandler(_mockRepository);
    }

    [Fact]
    public async Task EndTimeLessThanStartTime()
    {
        // Arrange
        var mockEvent = new Event 
        { 
            EndTime = DateTimeOffset.Now, 
            StartTime = DateTimeOffset.Now, 
            SpaceId = Guid.Empty
        };

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        async Task<ScResult<Event>> Action() => await _handler.Handle(command, new CancellationToken());

        // Assert
        var ex = await Assert.ThrowsAsync<ValidationException>((Func<Task<ScResult<Event>>>)Action);
        Assert.Equal("¬рем€ начала меропри€ти€ должно быть раньше времени окончани€", ex.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task SpaceIsNull()
    {
        // Arrange
        var mockEvent = new Event
        {
            EndTime = DateTimeOffset.Now,
            StartTime = DateTimeOffset.MinValue,
            SpaceId = null
        };

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        async Task<ScResult<Event>> Action() => await _handler.Handle(command, new CancellationToken());

        // Assert
        var ex = await Assert.ThrowsAsync<ValidationException>((Func<Task<ScResult<Event>>>)Action);
        Assert.Equal("ѕространство не может быть null", ex.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var mockEvent = new Event
        {
            EndTime = DateTimeOffset.MaxValue,
            StartTime = DateTimeOffset.MinValue,
            SpaceId = Guid.NewGuid(),
            PreviewImageId = Guid.NewGuid(),
            Description = "Description",
            EventId = Guid.NewGuid(),
            Name = "Name",
            PlacesAvailable = true,
            Tickets = null
        };

        _mockRepository
            .AddEventAsync(Arg.Any<Event>())
            .Returns(mockEvent);

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        var result = await _handler.Handle(command, new CancellationToken());

        // Assert
        Assert.Equal(mockEvent.EndTime, result.Result?.EndTime);
        Assert.Equal(mockEvent.StartTime, result.Result?.StartTime);
        Assert.Equal(mockEvent.SpaceId, result.Result?.SpaceId);
        Assert.Equal(mockEvent.PreviewImageId, result.Result?.PreviewImageId);
        Assert.Equal(mockEvent.Description, result.Result?.Description);
        Assert.Equal(mockEvent.EventId, result.Result?.EventId);
        Assert.Equal(mockEvent.Name, result.Result?.Name);
        Assert.Equal(mockEvent.PlacesAvailable, result.Result?.PlacesAvailable);
        Assert.Equal(mockEvent.Tickets, result.Result?.Tickets);
    }
}