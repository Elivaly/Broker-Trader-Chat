using System.Text;
using SocketApp;
using System.Net.WebSockets;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddSignalR();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseWebSockets();
app.Map("/SocketApp", async context =>
{
    if (context.WebSockets.IsWebSocketRequest) 
    {
        using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
        var rn = new Random();

        while (true)
        {
            var today = DateTime.Now;
            byte[] data = Encoding.ASCII.GetBytes($"{today}");
            await webSocket.SendAsync(data, WebSocketMessageType.Text, true, CancellationToken.None);
            await Task.Delay(500);

            long rnd = rn.NextInt64();

            if (rnd == 7)
            {
                await webSocket.CloseAsync(WebSocketCloseStatus.NormalClosure, "random close", CancellationToken.None);

                return;
            }
        }
    }
    else 
    {
        context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
    }
}
);
app.UseRouting();

app.UseAuthorization();
app.MapHub<SocketHub>("/Trader-Makler-Chat");
app.MapRazorPages();

app.Run();
