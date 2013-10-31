using Microsoft.AspNet.SignalR;

namespace NoCms.Net.Hubs
{
	internal class ChatHub : Hub
	{
		 public void Send(string name, string message)
		 {
			 Clients.All.broadcastMessage(name, message);
		 }
	}
}