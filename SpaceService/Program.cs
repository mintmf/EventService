//using SC.Internship.Common.ScResult;

using SpaceService;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

//app.MapGet("/", () => "Hello World!");
app.MapGet("/", () => "This is a GET");
//app.MapGet("/users/{userId}/books/{bookId}", (int userId, int bookId) => $"The user id is {userId} and book id is {bookId}");
app.MapGet("/spaces/{spaceId}",
    () =>
    {
        var result = new ScResult<bool> (true);
        //var result = true;

        return result;
    }
);  

app.Run();
