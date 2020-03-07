using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace GladMMO
{
	public sealed class DisposableRepository<TRepositoryType> : IDisposable
	{
		public TRepositoryType Repository { get; }

		internal IDisposable DisposableScope { get; }

		public DisposableRepository([JetBrains.Annotations.NotNull] TRepositoryType repository, IDisposable disposableScope)
		{
			Repository = repository;
			DisposableScope = disposableScope;
		}

		public void Dispose()
		{
			DisposableScope?.Dispose();
		}
	}

	public interface IRepositoryFactory<TRepositoryType> : IFactoryCreatable<DisposableRepository<TRepositoryType>, EmptyFactoryContext>
		where TRepositoryType : IDisposable
	{

	}

	public sealed class RepositoryFactory<TRepositoryType> : IRepositoryFactory<TRepositoryType>
		where TRepositoryType : IDisposable
	{
		//We must used a scope factory so that
		//we're able to resolved scope services and not have issues
		//with DbContext disposal.
		private IServiceScopeFactory ScopeFactory { get; }

		public RepositoryFactory([JetBrains.Annotations.NotNull] IServiceScopeFactory scopeFactory)
		{
			ScopeFactory = scopeFactory ?? throw new ArgumentNullException(nameof(scopeFactory));
		}

		public DisposableRepository<TRepositoryType> Create(EmptyFactoryContext context)
		{
			IServiceScope scope = ScopeFactory.CreateScope();
			return new DisposableRepository<TRepositoryType>(scope.ServiceProvider.GetService<TRepositoryType>(), scope);
		}
	}
}
