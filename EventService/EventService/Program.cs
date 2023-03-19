using FluentValidation;
using EventService.Features.EventFeature;
using EventService.Features.EventFeature.CreateEvent;
using EventService.Services;
using Microsoft.OpenApi.Models;
using EventRepository = EventService.ObjectStorage.EventRepository;
using IEventRepository = EventService.ObjectStorage.IEventRepository;
using EventService.Models.Configs;
using EventService.ObjectStorage;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text.Json;
using System.Security.Claims;
using IdentityServer4.AccessTokenValidation;
using Microsoft.VisualBasic;
using IdentityModel.AspNetCore.OAuth2Introspection;

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
        options.ClientId = "spaWeb";
        options.ClientSecret = "hardtoguess";
    });
    /*.AddIdentityServerAuthentication(options =>
    {
        options.Authority = "http://127.0.0.1:5000";
        options.ApiSecret = "web-api-secret";
        options.ApiName = "spaWeb";
    });*/
/*.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
.AddIdentityServerAuthentication(
options =>
{
    options.Authority = "http://127.0.0.1:5000";
    options.SupportedTokens = SupportedTokens.Reference;
    options.RequireHttpsMetadata = false;
});*/


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
builder.Services.AddScoped<IEventRepository, EventRepository>();
builder.Services.AddScoped<IImageService, ImageService>();
builder.Services.AddScoped<ISpaceService, SpaceService>();
builder.Services.AddScoped<IValidator<Event>, CreateEventValidator>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<ITicketRepository, TicketRepository>();


builder.Services.Configure<IdentityServerConfig>(builder.Configuration.GetSection("IdentityServerConfig")); 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseCors(allowAllPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
