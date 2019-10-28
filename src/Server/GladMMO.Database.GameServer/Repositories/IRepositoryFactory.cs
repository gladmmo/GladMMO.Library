using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public interface IRepositoryFactory<out TRepositoryType> : IFactoryCreatable<TRepositoryType, EmptyFactoryContext>
		where TRepositoryType : IDisposable
	{

	}

	public sealed class RepositoryFactory<TRepositoryType> : IRepositoryFactory<TRepositoryType>
		where TRepositoryType : IDisposable
	{
		/// <summary>
		/// Likely provided by Autofac.
		/// </summary>
		private Func<TRepositoryType> RepositoryCreationFactory { get; }

		public RepositoryFactory([JetBrains.Annotations.NotNull] Func<TRepositoryType> repositoryCreationFactory)
		{
			RepositoryCreationFactory = repositoryCreationFactory ?? throw new ArgumentNullException(nameof(repositoryCreationFactory));
		}

		public TRepositoryType Create(EmptyFactoryContext context)
		{
			return RepositoryCreationFactory();
		}
	}
}
