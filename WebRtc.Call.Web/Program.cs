using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.OpenApi.Models;
using chores.BLL;
using WebRtc.Call.Web.Middlewares;
using WebRtc.Call.Web.Services;

var builder = WebApplication.CreateBuilder();

builder.Services.AddCors();
builder.Services.AddControllers().AddNewtonsoftJson(options => { options.SerializerSettings.Configure(); });
builder.Services.AddSwaggerGen(options =>
{
    options.EnableAnnotations();
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "WebRtc API",
        Description = "WebRtc API"
    });
});

builder.Services.AddScoped<WebSocketHandlerService>();

// builder.Services.AddDb(builder.Configuration);
// builder.Services.AddBLL();
// ---

var app = builder.Build();

// await app.Services.MigrateDb();

app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});

if (!builder.Environment.IsProduction())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "swagger/{documentName}/swagger.json";
        options.PreSerializeFilters.Add((swaggerDoc, httpReq) => swaggerDoc.Servers = new List<OpenApiServer>
        {
            new() {Url = $"{httpReq.Scheme}://{httpReq.Host.Value}/webrtc"}
        });
    });
    app.UseSwaggerUI();
}

app.UseCors(policyBuilder =>
    policyBuilder.WithOrigins(
            "http://localhost",
            "http://localhost:4200",
            "https://localhost",
            "https://localhost:4200",
            "http://birdegop.ru",
            "https://birdegop.ru")
        .AllowAnyMethod()
        .AllowAnyHeader()
        .AllowCredentials());


app.UseMiddleware<ExceptionCatcherMiddleware>();

app.UseRouting();

app.UseWebSockets();

app.Use(async (context, next) =>
{
    if (context.Request.Path == "/ws")
    {
        if (context.WebSockets.IsWebSocketRequest)
        {
            Console.WriteLine("WebSocket accept");
            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            byte[] buffer = new byte[8192];
            var receiveResult = await webSocket.ReceiveAsync(buffer, CancellationToken.None);

            Console.WriteLine($"Received: {Encoding.UTF8.GetString(buffer, 0, receiveResult.Count)}");
            
            await webSocket.SendAsync(
                Encoding.UTF8.GetBytes("Success motherfucker"),
                WebSocketMessageType.Text,
                WebSocketMessageFlags.EndOfMessage,
                CancellationToken.None
            );
            Console.WriteLine("Written message");
            await Task.Delay(5000);
            await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "Closing", CancellationToken.None);
        }
        else
        {
            context.Response.StatusCode = StatusCodes.Status400BadRequest;
        }
    }
    else
    {
        await next(context);
    }
});

app.MapControllers();

app.Run();