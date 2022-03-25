using Application.Exceptions;
using Application.Interceptor.Base;
using Application.Interfaces;
using Castle.DynamicProxy;
using Core.Exceptions;
using Core.IoC;
using System;

namespace Application.Interceptors.Attributes
{
	public class Logging : InterceptorBaseAttribute
	{
		private readonly ILogService _logService;
		public Logging()
		{
			_logService = IoCContainer.Resolve<ILogService>();
		}

		public override void OnEntry(IInvocation invocation)
		{
			var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
			_logService.LogInformation(name, "On Before");
		}
		public override void OnError(IInvocation invocation, Exception ex)
		{
			var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
			_logService.LogWarning($"Error : {name}", ex.Message);
		}

		public override void OnFinally(IInvocation invocation)
		{
			var name = $"{invocation.Method.DeclaringType}.{invocation.Method.Name}";
			_logService.LogInformation(name, $"On After");
		}
	}
}
