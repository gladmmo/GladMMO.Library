using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
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
