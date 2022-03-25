using Autofac;
using Autofac.Extensions.DependencyInjection;
using System;

namespace Core.IoC
{
	public static class IoCContainer
	{
		private static ILifetimeScope AutofacContainer;

		public static void Init(IServiceProvider provider)
		{
			AutofacContainer = provider.GetAutofacRoot();
		}

		public static TInstance Resolve<TInstance>()
		{
			return AutofacContainer.Resolve<TInstance>();
		}
	}
}
