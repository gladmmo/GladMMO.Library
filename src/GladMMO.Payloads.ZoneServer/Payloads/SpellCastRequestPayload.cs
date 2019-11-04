using System;
using System.Collections.Generic;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	/// <summary>
	/// Payload for the client to request an interaction with a GameObject.
	/// </summary>
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.RequestSpellCast)]
	public sealed class SpellCastRequestPayload : GameClientPacketPayload
	{
		[ProtoMember(1)]
		public int SpellId { get; private set; }

		public SpellCastRequestPayload(int spellId)
		{
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			SpellId = spellId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected SpellCastRequestPayload()
		{

		}
	}
}
