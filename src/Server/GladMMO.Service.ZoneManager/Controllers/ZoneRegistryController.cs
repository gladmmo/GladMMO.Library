using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace GladMMO
{
	[Route("api/[controller]")]
	public class ZoneRegistryController : AuthorizationReadyController
	{
		private IZoneServerRepository ZoneRepository { get; }

		/// <inheritdoc />
		public ZoneRegistryController([FromServices] IZoneServerRepository zoneRepository, 
			IClaimsPrincipalReader claimsReader, 
			ILogger<AuthorizationReadyController> logger) 
			: base(claimsReader, logger)
		{
			ZoneRepository = zoneRepository ?? throw new ArgumentNullException(nameof(zoneRepository));
		}

		//ZoneServerRegistrationResponse
		/// <summary>
		/// 
		/// </summary>
		/// <param name="request"></param>
		/// <returns></returns>
		[AuthorizeJwt(GuardianApplicationRole.ZoneServer)] //only zone servers obviously.
		[HttpPost("register")]
		[ProducesJson]
		public async Task<IActionResult> TryRegisterZoneServer([FromBody] ZoneServerRegistrationRequest request,
			[FromServices] [NotNull] IWorldDataServiceClient worldDataClient)
		{
			if (worldDataClient == null) throw new ArgumentNullException(nameof(worldDataClient));

			if (!ModelState.IsValid)
				return BuildFailedResponseModel(ZoneServerRegistrationResponseCode.GeneralServerError);

			//This should really ever happen in normal circumstances.
			if (await ZoneRepository.ContainsAsync(ClaimsReader.GetAccountIdInt(User)))
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"UserId: {ClaimsReader.GetPlayerAccountId(User)} attempted to register ZoneId: {ClaimsReader.GetAccountId(User)} multiple times.");

				return BuildFailedResponseModel(ZoneServerRegistrationResponseCode.ZoneAlreadyRegistered);
			}

			//Check world exists
			if(!await worldDataClient.CheckWorldExistsAsync(request.WorldId))
				return BuildFailedResponseModel(ZoneServerRegistrationResponseCode.WorldRequestedNotFound);

			//TODO: Should we check world rights ownership here?
			bool registerResult = await ZoneRepository.TryCreateAsync(new ZoneInstanceEntryModel(ClaimsReader.GetAccountIdInt(User), HttpContext.Connection.RemoteIpAddress.ToString(), request.NetworkPort, request.WorldId));

			if (!registerResult)
				return BuildFailedResponseModel(ZoneServerRegistrationResponseCode.GeneralServerError);

			return BuildSuccessfulResponseModel(new ZoneServerRegistrationResponse(ClaimsReader.GetAccountIdInt(User)));
		}
	}
}
