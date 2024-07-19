
namespace WebApi.Domain.Model
{
    public class AuthenticationResponse
    {
        public string Username { get; set; }
        public string JwtToken { get; set; }
        public int ExpiresIN { get; set; }
    }
}
