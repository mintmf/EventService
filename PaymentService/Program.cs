using IdentityModel.AspNetCore.OAuth2Introspection;
using PaymentService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
