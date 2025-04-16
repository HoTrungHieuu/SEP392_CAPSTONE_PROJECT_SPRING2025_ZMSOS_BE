using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.WebSockets;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;

namespace Service.Other
{
    public class WebSocketHandler
    {
        private readonly WebSocketConnectionManager _manager;

        public WebSocketHandler(WebSocketConnectionManager manager)
        {
            _manager = manager;
        }

        public async Task HandleAsync(HttpContext context)
        {
            if (!context.WebSockets.IsWebSocketRequest)
            {
                context.Response.StatusCode = 400;
                return;
            }

            using var webSocket = await context.WebSockets.AcceptWebSocketAsync();
            var connectionId = Guid.NewGuid().ToString();

            _manager.AddSocket(connectionId, webSocket);

            // Giữ kết nối đến khi client đóng
            var buffer = new byte[1024 * 4];
            while (webSocket.State == WebSocketState.Open)
            {
                var result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
                if (result.MessageType == WebSocketMessageType.Close)
                {
                    break;
                }
            }
        }

        public async Task SendMessageAsync(string message)
        {
            var json = JsonSerializer.Serialize(new { status = "yes", message });
            var bytes = Encoding.UTF8.GetBytes(json);

            foreach (var socket in _manager.GetAll().Values)
            {
                if (socket.State == WebSocketState.Open)
                {
                    await socket.SendAsync(
                        new ArraySegment<byte>(bytes),
                        WebSocketMessageType.Text,
                        true,
                        CancellationToken.None);
                }
            }
        }
    }
}
