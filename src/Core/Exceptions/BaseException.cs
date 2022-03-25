using System;
using System.Net;

namespace Core.Exceptions
{
	public class BaseException : Exception
	{
		public HttpStatusCode Code { get; }

		public BaseException(string message = "", HttpStatusCode code = HttpStatusCode.BadRequest) : base(message)
		{
			Code = code;
		}
	}
}
