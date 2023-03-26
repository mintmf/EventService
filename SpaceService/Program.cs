using IdentityModel.AspNetCore.OAuth2Introspection;
using SpaceService;

var builder = WebApplication.CreateBuilder(args);

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

app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/", () => "This is a GET");
app.MapGet("/spaces/{spaceId}",
    () =>
    {
        return new ScResult<bool>(true);
    }
).RequireAuthorization();

app.Run();
