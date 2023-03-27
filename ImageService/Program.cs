using IdentityModel.AspNetCore.OAuth2Introspection;
using ImageService;

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

app.MapGet("/", () => "Hello World!");
app.MapGet("/images/{imageId}",
    () =>
    {
        var result = new ScResult<bool>(true);

        return result;
    }
).RequireAuthorization();
app.Run();
