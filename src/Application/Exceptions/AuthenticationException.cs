using Core.Exceptions;
using System.Net;

namespace Application.Exceptions
{
	public class AuthenticationException : BaseException
	{
		public AuthenticationException(string message, HttpStatusCode code = HttpStatusCode.Unauthorized) : base(message, code)
		{
		}
	}
}
