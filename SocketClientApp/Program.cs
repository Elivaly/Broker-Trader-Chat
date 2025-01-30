using System.Net.WebSockets;
using System.Text;
using Microsoft.AspNetCore.Builder;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();

var app = builder.Build();
var ws = new ClientWebSocket();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseWebSockets();
app.UseAuthorization();
app.MapRazorPages();

Console.WriteLine("Connect to server...");
await ws.ConnectAsync(new Uri("ws://192.168.5.32:5149/ws"), CancellationToken.None);
Console.WriteLine("Connected");

var receive = Task.Run(async () =>
{
    var buffer = new byte[4096];
    while (true) 
    {
        var result = await ws.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);

        if (result.MessageType == WebSocketMessageType.Close)
        {
            break;
        }

        var mesasge = Encoding.UTF8.GetString(buffer, 0, result.Count);
        Console.WriteLine("Получено сообщение " + mesasge);
    }
});

await receive;

app.Run(builder.Configuration["ApplicationHost:Address"]);
Console.ReadKey();
