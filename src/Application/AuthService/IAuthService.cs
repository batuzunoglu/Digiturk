using Application.DTO;
using System.Threading.Tasks;


namespace Application.AuthService
{
	public interface IAuthService : IBaseService
	{

		/// <summary>
		/// Method that finds user from context, generate a token and sets to redis.
		/// </summary>
		/// <param name="model">Represents LoginDto</param>
		/// <returns>Returns token for user.</returns>
		Task<string> LoginAsync(LoginDto model);

		/// <summary>
		/// Method that removes redis token key for logged id user.
		/// </summary>
		Task LogoutAsync();
	}
}
