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

		//Since zoneservers are not expected to call this directly we don't need to reply with
		//something like "No, you're done." or "you're unregistered now" because
		//the expectation is this request will be proxied through a message queue.
		[AuthorizeJwt(GuardianApplicationRole.ZoneServer)]
		[HttpPatch("checkin")]
		public async Task<IActionResult> RegisteredZoneCheckin([FromBody] ZoneServerCheckinRequestModel zoneCheckinRequest)
		{
			//This is ok, it means the zone was unregistered before the checkin message was recieved.
			//It doesn't alone indicate a failure.
			if(!await ZoneRepository.ContainsAsync(ClaimsReader.GetAccountIdInt(User)))
			{
				if(Logger.IsEnabled(LogLevel.Warning))
					Logger.LogWarning($"UserId: {ClaimsReader.GetPlayerAccountId(User)} attempted to checkin ZoneId: {ClaimsReader.GetAccountId(User)} but the zone was not registered.");

				return Ok();
			}

			ZoneInstanceEntryModel model = await ZoneRepository.RetrieveAsync(ClaimsReader.GetAccountIdInt(User));

			//We don't REMOVE if expired. This should only update checkin time
			//something else should be responsible for removal if expired at some point.
			model.UpdateCheckinTime();
			await ZoneRepository.UpdateAsync(model.ZoneId, model);

			//We don't really have anything to reply to, this shouldn't be called externally.
			//It's basically handling a proxied message queue message.
			return Ok();
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
