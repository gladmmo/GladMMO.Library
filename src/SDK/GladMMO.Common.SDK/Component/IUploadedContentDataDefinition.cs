using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO.SDK
{
	public interface IUploadedContentDataDefinition
	{
		long ContentId { get; }

		Guid ContentGuid { get; }
	}
}
