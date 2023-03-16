using FluentValidation;
using EventService.Features.EventFeature;
using EventService.Features.EventFeature.CreateEvent;
using EventService.Services;
using Microsoft.OpenApi.Models;
using EventRepository = EventService.ObjectStorage.EventRepository;
using IEventRepository = EventService.ObjectStorage.IEventRepository;
using EventService.Models.Configs;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

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

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.RequireHttpsMetadata = false;
        options.TokenValidationParameters = new TokenValidationParameters
        {
            // укзывает, будет ли валидироваться издатель при валидации токена
            ValidateIssuer = false,
            // строка, представляющая издателя
            ValidIssuer = "",

            // будет ли валидироваться потребитель токена
            ValidateAudience = false,
            // установка потребителя токена
            ValidAudience = "",
            // будет ли валидироваться время существования
            ValidateLifetime = false,

            // установка ключа безопасности
            //IssuerSigningKey = new SymmetricSecurityKey(null),
            // валидация ключа безопасности
            ValidateIssuerSigningKey = false
        };
        options.Authority = "";
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title="Event API", Version = "V1" } );
    c.EnableAnnotations();
    c.IncludeXmlComments($@"{System.AppDomain.CurrentDomain.BaseDirectory}\EventService.xml");
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please insert JWT with Bearer into field",
        BearerFormat = "JWT",
        Scheme = "bearer",
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


builder.Services.Configure<IdentityServerConfig>(builder.Configuration.GetSection("IdentityServerConfig")); 

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Program).Assembly));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(allowAllPolicy);

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
