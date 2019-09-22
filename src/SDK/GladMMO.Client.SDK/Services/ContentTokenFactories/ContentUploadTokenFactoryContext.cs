using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace GladMMO
{
	public sealed class ContentUploadTokenFactoryContext
	{
		/// <summary>
		/// The content type to create a token for.
		/// </summary>
		public UserContentType ContentType { get; }

		public ContentUploadTokenFactoryContext(UserContentType contentType)
		{
			if(!Enum.IsDefined(typeof(UserContentType), contentType)) throw new InvalidEnumArgumentException(nameof(contentType), (int)contentType, typeof(UserContentType));

			ContentType = contentType;
		}
	}
}
