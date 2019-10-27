using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public static class GenericSocialEventArgs
	{
		/// <summary>
		/// Creates a new generic social event args model.
		/// </summary>
		/// <param name="data">The social model data.</param>
		/// <returns></returns>
		public static GenericSocialEventArgs<TSocialModel> Create<TSocialModel>(TSocialModel data) 
			where TSocialModel : BaseSocialModel
		{
			return new GenericSocialEventArgs<TSocialModel>(data);
		}
	}

	/// <summary>
	/// Generic social event args.
	/// </summary>
	/// <typeparam name="TSocialModel">The social data model Type.</typeparam>
	public sealed class GenericSocialEventArgs<TSocialModel> : EventArgs
		where TSocialModel : BaseSocialModel
	{
		public TSocialModel Data { get; }

		public GenericSocialEventArgs([NotNull] TSocialModel data)
		{
			Data = data ?? throw new ArgumentNullException(nameof(data));
		}
	}
}
