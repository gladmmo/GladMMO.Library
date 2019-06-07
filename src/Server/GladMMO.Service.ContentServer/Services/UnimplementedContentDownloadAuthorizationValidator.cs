using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	/// <summary>
	/// Unimplemented/unifnished download authorization.
	/// </summary>
	public sealed class UnimplementedContentDownloadAuthorizationValidator : IContentDownloadAuthroizationValidator
	{
		/// <inheritdoc />
		public Task<bool> CanUserAccessWorldContet(int userId, long worldId)
		{
			//TODO: When this project leaves alpha, we should probably handle the download authorization validator.
			ProjectVersionStage.AssertAlpha();
			return Task.FromResult<bool>(true);
		}
	}
}
