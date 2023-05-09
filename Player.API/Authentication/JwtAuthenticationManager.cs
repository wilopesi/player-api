using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using System.Security.Claims;
using Player.Domain.Model.Authentication;

namespace Player.API.Authentication
{
    public class JwtAuthenticationManager
	{
		private readonly IConfiguration _configuration;

		private readonly string _secret;

		public JwtAuthenticationManager(IConfiguration configuration)
		{
			_configuration = configuration;
			_secret = _configuration["Jwt:Secret"];
		}

		public Token Authenticate(string secret)
		{
			if (secret == null || !_secret.Equals(secret))
				return null;

			JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
			var tokenKey = Encoding.ASCII.GetBytes(_configuration["Jwt:Token"]);

			SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor()
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.Hash, secret)
				}),
				Expires = DateTime.UtcNow.AddHours(1),
				SigningCredentials = new SigningCredentials(
					new SymmetricSecurityKey(tokenKey),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			Token tokenResponse = new Token()
			{
				token = tokenHandler.WriteToken(token),
				token_type = "bearer",
				expires_in = 3600
			};

			return tokenResponse;
		}
	}
}
