using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GladMMO
{
	public sealed class ZoneChatMessageEnvelopeFactory : BaseTargetlessChannelMessageEnvelopeFactory<ZoneChatMessageRequestModel, ZoneChatMessageEventModel>
	{
		/// <inheritdoc />
		public override ZoneChatMessageEventModel Create(GenericChatMessageContext<ZoneChatMessageRequestModel> context)
		{
			return new ZoneChatMessageEventModel(BuildForwardableTargetlessChannelChatMessage(context.HubContext, context.IncomingMessage));
		}
	}
}
