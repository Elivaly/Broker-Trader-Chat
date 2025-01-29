namespace SocketClientApp.Models
{
    public class AuthorizeModel
    {
        private readonly ILogger<AuthorizeModel> _logger;

        public AuthorizeModel(ILogger<AuthorizeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {

        }
    }
}
