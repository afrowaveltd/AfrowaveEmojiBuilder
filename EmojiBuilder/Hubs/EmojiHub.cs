using Microsoft.AspNetCore.SignalR;

namespace EmojiBuilder.Hubs
{
	public class EmojiHub : Hub
	{
		// Notifikace klientovi
		public async Task SendProgress(int percent)
		{
			await Clients.All.SendAsync("ReceiveProgress", percent);
		}

		// Volitelně: další metody např. pro změny emoji v reálném čase
	}
}