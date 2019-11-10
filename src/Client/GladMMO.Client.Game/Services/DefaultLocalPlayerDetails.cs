using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class DefaultLocalPlayerDetails : ILocalPlayerDetails, IReadonlyLocalPlayerDetails
	{
		private Lazy<NetworkEntityGuid> _localPlayerGuid;

		/// <inheritdoc />
		public NetworkEntityGuid LocalPlayerGuid
		{
			get => _localPlayerGuid.Value;
			set => throw new NotSupportedException();
		}

		//TODO: Come up with a better way of storing entity data, without downcasting.
		/// <inheritdoc />
		public IEntityDataFieldContainer EntityData => FieldDataMap[LocalPlayerGuid];

		/// <summary>
		/// Entity data map used to access the entity data through <see cref="EntityData"/>
		/// </summary>
		private IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> FieldDataMap { get; }

		private ILocalCharacterDataRepository CharacterDataRepo { get; }

		/// <inheritdoc />
		public DefaultLocalPlayerDetails(IReadonlyEntityGuidMappable<IChangeTrackableEntityDataCollection> fieldDataMap, [NotNull] ILocalCharacterDataRepository characterDataRepo)
		{
			FieldDataMap = fieldDataMap ?? throw new ArgumentNullException(nameof(fieldDataMap));
			CharacterDataRepo = characterDataRepo ?? throw new ArgumentNullException(nameof(characterDataRepo));

			_localPlayerGuid = new Lazy<NetworkEntityGuid>(() =>
			{
				return new NetworkEntityGuidBuilder()
					.WithId(characterDataRepo.CharacterId)
					.WithType(EntityType.Player)
					.Build();
			});
		}
	}
}
