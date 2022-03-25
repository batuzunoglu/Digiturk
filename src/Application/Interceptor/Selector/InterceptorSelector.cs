using Application.Interceptor.Base;
using Castle.DynamicProxy;
using System;
using System.Linq;
using System.Reflection;

namespace Application.Interceptor.Selector
{
	public class InterceptorSelector : IInterceptorSelector
	{
		public IInterceptor[] SelectInterceptors(Type type, MethodInfo method, IInterceptor[] interceptors)
		{
			var classAttributes = type.GetCustomAttributes<InterceptorBaseAttribute>(true).ToList();

			var methodAttributes = type.GetMethod(method.Name).GetCustomAttributes<InterceptorBaseAttribute>(true).ToList();

			classAttributes.AddRange(methodAttributes);

			return classAttributes.ToArray();
		}
	}
}
