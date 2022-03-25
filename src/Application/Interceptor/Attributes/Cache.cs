using Application.Interceptor.Base;
using Application.Interfaces;
using Castle.DynamicProxy;
using Core.IoC;
using Newtonsoft.Json;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Interceptors.Attributes
{
	public class Cache : InterceptorBaseAttribute
	{
		private TimeSpan ExpiryTime;
		private readonly ICacheService _cacheService;
		public Cache(int minute = 30, int fromHour = 0)
		{
			ExpiryTime = new TimeSpan(fromHour, minute, 0);
			_cacheService = IoCContainer.Resolve<ICacheService>();
		}

		public override void OnProceed(IInvocation invocation)
		{
			var method = invocation.MethodInvocationTarget ?? invocation.Method;
			if (method.ReturnType == typeof(void) || method.ReturnType == typeof(Task))
			{
				invocation.Proceed();
			}
			else
			{
				var cacheKey = GetKey(invocation);
				var cacheValue = _cacheService.Get(cacheKey);
				if (cacheValue != null)
				{
					Type returnType;
					if (typeof(Task).IsAssignableFrom(method.ReturnType))
					{
						returnType = method.ReturnType.GenericTypeArguments.FirstOrDefault();
					}
					else
					{
						returnType = method.ReturnType;
					}
					dynamic _result = JsonConvert.DeserializeObject(cacheValue, returnType);
					invocation.ReturnValue = (typeof(Task).IsAssignableFrom(method.ReturnType)) ? Task.FromResult(_result) : _result;
					return;
				}

				invocation.Proceed();
				if (!string.IsNullOrWhiteSpace(cacheKey))
				{
					object response;
					var type = invocation.Method.ReturnType;
					if (typeof(Task).IsAssignableFrom(type))
					{
						var resultProperty = type.GetProperty("Result");
						response = resultProperty.GetValue(invocation.ReturnValue);
					}
					else
					{
						response = invocation.ReturnValue;
					}
					if (response == null) response = string.Empty;
					_cacheService.Set(cacheKey, response, ExpiryTime);
				}
			}
		}

		private string GetKey(IInvocation invocation)
		{
			var typeName = invocation.TargetType.Name;
			var methodName = invocation.Method.Name;
			var methodArguments = invocation.Arguments.ToList();

			string key = $"{typeName}_{methodName}";
			foreach (var param in methodArguments)
			{
				key = $"{key}_{param}";
			}

			return key;
		}

	}
}
