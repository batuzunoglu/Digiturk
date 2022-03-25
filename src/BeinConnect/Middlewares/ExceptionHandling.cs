using System;
using System.IO;
using System.Net;
using Application.Exceptions;
using Digiturk.Utils;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace Digiturk.Middlewares
{
	public static class ExceptionHandling
	{
		public static void ConfigureExceptionHandler(this IApplicationBuilder app)
		{
			app.UseExceptionHandler(appError =>
			{
				appError.Run(async context =>
							{
								context.Response.ContentType = "application/json";

								var exception = context.Features.Get<IExceptionHandlerFeature>()?.Error;
								if (exception != null)
								{
									switch (exception)
									{
										case UnauthorizedAccessException unauthorizedAccessException:
											{
												var data = new ResponseErrorModel(unauthorizedAccessException.Source,
																	unauthorizedAccessException.Message);
												context.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
												await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
												break;
											}
										case FoundException foundException:
											{

												var data = new ResponseErrorModel(foundException.Source,
																 foundException.Message);
												context.Response.StatusCode = (int)foundException.Code;
												await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
												break;
											}
										case AuthenticationException authenticationException:
											{
												var data = new ResponseErrorModel(authenticationException.Source,
																	authenticationException.Message);
												context.Response.StatusCode = (int)authenticationException.Code;
												await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
												break;
											}
										case ArgumentNullException nullException:
											{
												var data = new ResponseErrorModel(nullException.Source, nullException.Message);
												context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
												await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
												break;
											}
										default:
											{
												var data = new ResponseErrorModel(exception.Source, exception.Message);
												context.Response.StatusCode = (int)500;
												await context.Response.WriteAsync(JsonConvert.SerializeObject(data));
												break;
											}
									}
								}
							});
			});
		}
	}
}