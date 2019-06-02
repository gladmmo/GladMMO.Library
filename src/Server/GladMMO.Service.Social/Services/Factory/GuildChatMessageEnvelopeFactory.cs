using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class GuildChatMessageEnvelopeFactory : BaseTargetlessChannelMessageEnvelopeFactory<GuildChatMessageRequestModel, GuildChatMessageEventModel>
	{
		/// <inheritdoc />
		public override GuildChatMessageEventModel Create(GenericChatMessageContext<GuildChatMessageRequestModel> context)
		{
			return new GuildChatMessageEventModel(BuildForwardableTargetlessChannelChatMessage(context.HubContext, context.IncomingMessage));
		}
	}
}
