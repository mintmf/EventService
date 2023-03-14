using System.Reflection;
using FluentValidation;
using EventService.Features.EventFeature;
using EventService.Features.EventFeature.CreateEvent;
using EventService.Services;
using EventRepository = EventService.ObjectStorage.EventRepository;
using IEventRepository = EventService.ObjectStorage.IEventRepository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c => { c.EnableAnnotations(); });
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IValidator<Event>, CreateEventValidator>();


builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
