using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using JetBrains.Annotations;
using Newtonsoft.Json;
using ProtoBuf;

namespace GladMMO
{
	//TODO: Not checked/validated with serialization tests
	/// <summary>
	/// Generic base-type for any model that is coupled/associated with an Entity.
	/// Specificially with the <see cref="ObjectGuid"/>
	/// </summary>
	/// <typeparam name="TDataType"></typeparam>
	[JsonObject]
	[ProtoContract]
	public class EntityAssociatedData<TDataType> : IEntityGuidContainer
	{
		/// <summary>
		/// The GUID of the entity.
		/// </summary>
		[JsonProperty]
		[ProtoMember(1, IsRequired = true)]
		public ObjectGuid EntityGuid { get; private set; }

		[JsonProperty]
		[ProtoMember(2)]
		public TDataType Data { get; private set; }

		/// <inheritdoc />
		public EntityAssociatedData([NotNull] ObjectGuid entityGuid, [NotNull] TDataType data)
		{
			EntityGuid = entityGuid ?? throw new ArgumentNullException(nameof(entityGuid));
			Data = data;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		protected EntityAssociatedData()
		{
			
		}
	}
}
