using FluentValidation;
using EventService.Features.EventFeature;
using EventService.Services;
using Microsoft.OpenApi.Models;
using EventRepository = EventService.ObjectStorage.EventRepository;
using IEventRepository = EventService.ObjectStorage.IEventRepository;
using EventService.ObjectStorage;
using IdentityModel.AspNetCore.OAuth2Introspection;
using MongoDB.Driver;
using EventService.Infrastracture;
using Microsoft.AspNetCore.HttpLogging;
using Polly;
using EventService.Services.BackgroundServices;
using MediatR;
using EventService.Features;
using MongoDB.Bson.Serialization;

BsonClassMap.RegisterClassMap<Event>(cm =>
{
    cm.AutoMap();
    cm.SetIgnoreExtraElements(true);
});

var builder = WebApplication.CreateBuilder(args);

const string allowAllPolicy = "_allowAllPolicy";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: allowAllPolicy,
        policy =>
        {
            policy.AllowAnyHeader();
            policy.AllowAnyMethod();
            policy.AllowAnyOrigin();
        });
});

// Add services to the container.

builder.Services.AddControllers();

var identityServerConfig = builder.Configuration.GetSection("IdentityServer").Get<IdentityServerConfig>();

builder.Services.AddAuthentication(OAuth2IntrospectionDefaults.AuthenticationScheme)
    .AddOAuth2Introspection(options =>
    {
        options.Authority = identityServerConfig?.Authority;
        options.ClientId = identityServerConfig?.ClientId;
        options.ClientSecret = identityServerConfig?.ClientSecret;
        options.IntrospectionEndpoint = identityServerConfig?.IntrospectionEndpoint;
    });
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title="Event API", Version = "V1" } );
    c.EnableAnnotations();
    c.IncludeXmlComments(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "EventService.xml"));

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        BearerFormat = "JWT",
        Scheme = "Bearer",
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] { }
        }
    });
});

builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.MediaTypeOptions.AddText("application/javascript");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;
});

builder.Services.AddHttpClient<IPaymentService, PaymentService>()
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
builder.Services.AddHttpClient<IImageService, ImageService>()
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));
builder.Services.AddHttpClient<ISpaceService, SpaceService>()
    .AddTransientHttpErrorPolicy(policy => policy.WaitAndRetryAsync(3, _ => TimeSpan.FromSeconds(2)));

builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IValidator<Event>, EventValidator>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IEventsMongoClient, EventsMongoClient>();
builder.Services.AddScoped<IPaymentService, PaymentService>();
builder.Services.AddScoped<IRabbitMqService, RabbitMqService>();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehaviour<,>));

builder.Services.Configure<IdentityServerConfig>(builder.Configuration.GetSection("IdentityServer"));
builder.Services.Configure<EventsMongoConfig>(builder.Configuration.GetSection("MongoParameters"));
builder.Services.Configure<ImageServiceConfig>(builder.Configuration.GetSection("ImageService"));
builder.Services.Configure<SpaceServiceConfig>(builder.Configuration.GetSection("SpaceService"));
builder.Services.Configure<PaymentServiceConfig>(builder.Configuration.GetSection("PaymentService"));
builder.Services.Configure<RabbitMqConfig>(builder.Configuration.GetSection("RabbitMqParameters"));

builder.Services.AddHostedService<RabbitMqListener>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI(c => 
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Event Service");
    c.RoutePrefix = String.Empty;
});

//app.UseHttpsRedirection();

app.UseCors(allowAllPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
