using System.Security.Claims;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using WebApi.Domain.Model;
namespace JwtAuthenticationManger
{
    public class JwtTokenHandler
    {
        public const string JWT_SECURITY_KEY = "Yh2k7QSu4l8CZg5p6X3Pna9L0Miy4D3Bvt0JVr87UcOj69Kqw5R2Nmf4FWs03Hdx";
        private const int JWT_TOKWN_VALIDITY_MINS = 20;

        private readonly List<Login> _userAccountList;
        public JwtTokenHandler()
        {
            _userAccountList = new List<Login>
            {
                new Login { Username = "admin", Password = "admin@123", userRole = "Administrator" },
                new Login { Username = "user", Password = "user@123", userRole = "User" }
            };
        }

        public AuthenticationResponse? GenerateJwtToken(AuthenticationRequest authenticationRequest) 
        {
            if (string.IsNullOrEmpty(authenticationRequest.Username)|| string.IsNullOrEmpty(authenticationRequest.Password))
             return null;

            var userAccount =_userAccountList.Where(x=>x.Username == authenticationRequest.Username && x.Password==authenticationRequest.Password).FirstOrDefault();
            if (userAccount == null) return null;
             var  tokenExpiryTimeStamp = DateTime.Now.AddMinutes(JWT_TOKWN_VALIDITY_MINS);
            var tokenKey = Encoding.ASCII.GetBytes(JWT_SECURITY_KEY);
            var claimsIdentity = new ClaimsIdentity(new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Name,authenticationRequest.Username),
                new Claim(ClaimTypes.Role,userAccount.userRole)
               //  new Claim("Role",userAccount.userRole)
            });
            
            var signingCredentials = new SigningCredentials(new SymmetricSecurityKey(tokenKey),
             SecurityAlgorithms.HmacSha256Signature);

            var securityTokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = claimsIdentity,
                Expires = tokenExpiryTimeStamp,
                SigningCredentials = signingCredentials
            };

            var jwtSecurityTokenHandler =new JwtSecurityTokenHandler();
            var securityToken = jwtSecurityTokenHandler.CreateToken(securityTokenDescriptor);
            var token = jwtSecurityTokenHandler.WriteToken(securityToken);
            return new AuthenticationResponse
            {
                Username = authenticationRequest.Username,
                ExpiresIN = (int)tokenExpiryTimeStamp.Subtract(DateTime.Now).TotalSeconds,
                JwtToken = token
            };
        }
    }
}
