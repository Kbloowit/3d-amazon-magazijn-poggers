using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Net.WebSockets;
using System.Threading.Tasks;
using System.Text;
using Controllers;

namespace Views
{
    public class ClientView : View
    {
        /// <summary>
        /// WebSocket that the view uses
        /// </summary>
        private WebSocket socket;

        /// <summary>
        /// Constructor of ClientView, sets socket
        /// </summary>
        /// <param name="socket"></param>
        public ClientView(WebSocket socket)
        {
            this.socket = socket;
        }

        /// <summary>
        /// recieve async task, lissens to socket
        /// </summary>
        /// <returns>message the socket sends</returns>
        public async Task StartReceiving()
        {
            var buffer = new byte[1024 * 4];

            Console.WriteLine("ClientView connection started");

            WebSocketReceiveResult result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                Console.WriteLine("Received the following information from client: " + Encoding.UTF8.GetString(buffer));

                result = await socket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }

            Console.WriteLine("ClientView has disconnected");

            await socket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
        }

        /// <summary>
        /// Send async message trough socket
        /// </summary>
        /// <param name="message">Message</param>
        private async void SendMessage(string message)
        {
            byte[] buffer = Encoding.UTF8.GetBytes(message);
            try
            {
                await socket.SendAsync(new ArraySegment<byte>(buffer, 0, message.Length), WebSocketMessageType.Text, true, CancellationToken.None);
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while sending information to client, probably a Socket disconnect");
            }
        }

        public void SendCommand(Command c)
        {
            SendMessage(c.ToJson());
        }

        /// <summary>
        /// On completed
        /// </summary>
        public override void OnCompleted()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// On error
        /// </summary>
        /// <param name="error"></param>
        public override void OnError(Exception error)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// On next
        /// </summary>
        /// <param name="value">Send command value</param>
        public override void OnNext(Command value)
        {
            SendCommand(value);
        }
    }
}