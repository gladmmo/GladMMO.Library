using System; using FreecraftCore;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using ProtoBuf;

namespace GladMMO
{
	[ProtoContract]
	[GamePayload(GamePayloadOperationCode.RequestSpellCast)]
	public sealed class SpellCastResponsePayload : GameServerPacketPayload, ISucceedable
	{
		/// <summary>
		/// Indicates the result of the spell cast.
		/// </summary>
		[ProtoMember(1)]
		public SpellCastResult Result { get; private set; }

		/// <summary>
		/// The ID of the spell this is a result for attempted casting.
		/// </summary>
		[ProtoMember(2)]
		public int SpellId { get; private set; }

		/// <summary>
		/// Indicates if the spell cast is successful.
		/// </summary>
		[ProtoIgnore]
		public bool isSuccessful => Result == SpellCastResult.SPELL_FAILED_SUCCESS;

		public SpellCastResponsePayload(SpellCastResult result, int spellId)
		{
			if (!Enum.IsDefined(typeof(SpellCastResult), result)) throw new InvalidEnumArgumentException(nameof(result), (int) result, typeof(SpellCastResult));
			if (spellId <= 0) throw new ArgumentOutOfRangeException(nameof(spellId));

			Result = result;
			SpellId = spellId;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		private SpellCastResponsePayload()
		{

		}
	}
}
