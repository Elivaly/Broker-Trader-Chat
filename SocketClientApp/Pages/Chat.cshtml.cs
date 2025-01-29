using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SocketClientApp.Models;

namespace SocketClientApp.Pages
{
    public class ChatModel : PageModel
    {

        private readonly ILogger<ChatModel> _logger;

        public ChatModel(ILogger<ChatModel> logger)
        {
            _logger = logger;
        }
        public void OnGet()
        {
        }
    }
}
