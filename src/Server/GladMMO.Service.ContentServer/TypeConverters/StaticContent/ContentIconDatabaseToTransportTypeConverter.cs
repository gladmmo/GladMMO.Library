using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class ContentIconDatabaseToTransportTypeConverter : ITypeConverterProvider<ContentIconEntryModel, ContentIconInstanceModel>
	{
		public ContentIconInstanceModel Convert([NotNull] ContentIconEntryModel fromObject)
		{
			if (fromObject == null) throw new ArgumentNullException(nameof(fromObject));

			return new ContentIconInstanceModel(fromObject.IconId, fromObject.IconPathName);
		}
	}
}
