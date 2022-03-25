using Castle.DynamicProxy;
using System;

namespace Application.Interceptor.Base
{
	public class InterceptorBaseAttribute : Attribute, IInterceptor
	{
		public virtual void OnEntry(IInvocation invocation) { }

		public void Intercept(IInvocation invocation)
		{
			OnEntry(invocation);
			try
			{
				OnProceed(invocation);
			}
			catch (Exception ex)
			{
				if (ex.InnerException != null)
				{
					ex = ex.InnerException;
				}
				OnError(invocation, ex);
				throw ex;
			}
			finally
			{
				OnFinally(invocation);
			}
		}
		public virtual void OnProceed(IInvocation invocation)
		{
			invocation.Proceed();
		}
		public virtual void OnError(IInvocation invocation, Exception ex) { }
		public virtual void OnFinally(IInvocation invocation) { }
	}
}
