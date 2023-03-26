using ImageService;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/images/{imageId}",
    () =>
    {
        var result = new ScResult<bool>(true);

        return result;
    }
);
app.Run();
