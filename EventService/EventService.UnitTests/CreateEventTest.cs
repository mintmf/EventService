using EventService.Features.EventFeature;
using EventService.Features.EventFeature.CreateEvent;
using EventService.ObjectStorage;
using EventService.Services;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using SC.Internship.Common.Exceptions;

namespace EventService.UnitTests;

public class CreateEventTest
{
    IEventRepository mockRepository;
    CreateEventCommandHandler handler;
    public CreateEventTest()
    {
        var serviceCollection = new ServiceCollection();
        serviceCollection.AddTransient<IImageService, ImageService>();
        serviceCollection.AddTransient<ISpaceService, SpaceService>();
        serviceCollection.AddTransient<IValidator<Event>, EventValidator>();

        var serviceProvider = serviceCollection.BuildServiceProvider();
        var validator = serviceProvider.GetService<IValidator<Event>>() ?? throw new ArgumentNullException();

        mockRepository = Substitute.For<IEventRepository>();
        handler = new CreateEventCommandHandler(mockRepository, validator);
    }

    [Fact]
    public async Task EndTimeLessThanStartTime()
    {
        // Arrange
        var mockEvent = new Event 
        { 
            EndTime = DateTime.Now, 
            StartTime = DateTime.Now, 
            SpaceId = Guid.Empty
        };

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        var action = async () => await handler.Handle(command, new CancellationToken());

        // Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(action);
        Assert.Equal("¬рем€ начала меропри€ти€ должно быть раньше времени окончани€", ex.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task SpaceIsNull()
    {
        // Arrange
        var mockEvent = new Event
        {
            EndTime = DateTime.Now,
            StartTime = DateTime.MinValue,
            SpaceId = null
        };

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        var action = async () => await handler.Handle(command, new CancellationToken());

        // Assert
        var ex = await Assert.ThrowsAsync<ValidationException>(action);
        Assert.Equal("ѕространство не может быть null", ex.Errors.First().ErrorMessage);
    }

    [Fact]
    public async Task Success()
    {
        // Arrange
        var mockEvent = new Event
        {
            EndTime = DateTime.MaxValue,
            StartTime = DateTime.MinValue,
            SpaceId = Guid.NewGuid(),
            PreviewImageId = Guid.NewGuid(),
            Description = "Description",
            EventId = Guid.NewGuid(),
            Name = "Name",
            PlacesAvailable = true,
            Tickets = null
        };

        mockRepository
            .AddEventAsync(Arg.Any<Event>())
            .Returns(mockEvent);

        var command = new CreateEventCommand { Event = mockEvent };

        // Act
        var result = await handler.Handle(command, new CancellationToken());

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