using Digiturk.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Digiturk.Controllers
{
	[Route("[controller]")]
	[ApiController]
	public class BaseController : ControllerBase
	{
		protected ResponseModel<T> Success<T>(T data) where T : class, new()
		{
			return new ResponseSuccessModel<T>(data);
		}

		protected ResponseModel Success()
		{
			return new ResponseSuccessModel();
		}
	}
}
