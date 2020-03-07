using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	public abstract class AuthorizationReadyController : BaseGuardiansController
	{
		/// <summary>
		/// The reader for the claims.
		/// </summary>
		protected IClaimsPrincipalReader ClaimsReader { get; }

		/// <inheritdoc />
		protected AuthorizationReadyController([FromServices] IClaimsPrincipalReader claimsReader, [FromServices] ILogger<AuthorizationReadyController> logger)
			: base(logger)
		{
			if(claimsReader == null) throw new ArgumentNullException(nameof(claimsReader));

			ClaimsReader = claimsReader;
		}
	}
}
