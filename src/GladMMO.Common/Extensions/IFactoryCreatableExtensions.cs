using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class IFactoryCreatableExtensions
	{
		/// <summary>
		/// Simplified creation API that creates a type <typeparamref name="TConstructionType"/> with provided <see cref="factory"/>.
		/// Only for creation contexts of <see cref="EmptyFactoryContext"/>.
		/// </summary>
		/// <typeparam name="TConstructionType">The construction type.</typeparam>
		/// <param name="factory">The factory instance.</param>
		/// <returns>An instance of the construction type.</returns>
		public static TConstructionType Create<TConstructionType>(this IFactoryCreatable<TConstructionType, EmptyFactoryContext> factory)
		{
			if (factory == null) throw new ArgumentNullException(nameof(factory));

			return factory.Create(EmptyFactoryContext.Instance);
		}
	}
}
