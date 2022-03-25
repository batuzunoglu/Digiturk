using Core.Exceptions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Application.Exceptions
{
	public class FoundException : BaseException
	{
		public FoundException(string message, HttpStatusCode code = HttpStatusCode.BadRequest) : base(message, code)
		{
		}
	}
}
