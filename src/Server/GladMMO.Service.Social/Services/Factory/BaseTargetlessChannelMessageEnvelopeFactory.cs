using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace GladMMO
{
	public abstract class BaseTargetlessChannelMessageEnvelopeFactory<TIncomingMessageType, TOutgoingMessageType> : IFactoryCreatable<TOutgoingMessageType, GenericChatMessageContext<TIncomingMessageType>> 
		where TIncomingMessageType : class
	{
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		protected EntityAssociatedData<TargetlessChannelChatMessageRequestModel> BuildForwardableTargetlessChannelChatMessage(IHubConnectionMessageContext context, TargetlessChannelChatMessageRequestModel message) => BuildForwardableAssociatedData(context, message);

		protected EntityAssociatedData<T> BuildForwardableAssociatedData<T>([JetBrains.Annotations.NotNull] IHubConnectionMessageContext context, [JetBrains.Annotations.NotNull] T envolpeContents)
		{
			if(context == null) throw new ArgumentNullException(nameof(context));
			if(envolpeContents == null) throw new ArgumentNullException(nameof(envolpeContents));

			//TODO: We should cache somehow the identifier's int value, parsing it each time I think can be costly.
			NetworkEntityGuid guid = new NetworkEntityGuidBuilder()
				.WithId(int.Parse(context.HubConntext.UserIdentifier))
				.WithType(EntityType.Player)
				.Build();

			return new EntityAssociatedData<T>(guid, envolpeContents);
		}

		/// <inheritdoc />
		public abstract TOutgoingMessageType Create(GenericChatMessageContext<TIncomingMessageType> context);
	}
}
