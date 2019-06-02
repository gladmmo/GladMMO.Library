using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public enum GroupUnitFrameIssueResult
	{
		Unknown = 0,
		Success = 1,
		FailedNotAPlayer = 2,
		FailedUnitframeUnavailable = 3,
		FailedAlreadyClaimedUnitframe = 4
	}

	public enum GroupUnitFrameReleaseResult
	{
		Unknown = 0,
		Sucess = 1,
		FailedNotAPlayer = 2,
		FailedNoUnitFrameClaimed = 3,
	}

	public interface IGroupUnitFrameIssuable
	{
		/// <summary>
		/// Attempts to claim a group unitframe for
		/// the provided Player <see cref="guid"/>.
		/// </summary>
		/// <param name="guid">The guid of the player.</param>
		/// <returns>True if a unitframe could be claimed.</returns>
		GroupUnitFrameIssueResult TryClaimUnitFrame(NetworkEntityGuid guid);

		/// <summary>
		/// Attempts to release a claimed unitframe
		/// for the provided Player <see cref="guid"/>.
		/// </summary>
		/// <param name="guid">The guid of the player.</param>
		/// <returns>True if a unitframe was claimed and successfully released for the player.</returns>
		GroupUnitFrameReleaseResult TryReleaseUnitFrame(NetworkEntityGuid guid);
	}
}
