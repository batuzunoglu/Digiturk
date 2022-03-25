using Digiturk.Identity.Models;

namespace Application.Jwt
{
	public interface IJwtHandler
	{
		string GenerateToken(int userId, string userName, string role);
		TokenPayload TokenParser(string token);
	}
}
