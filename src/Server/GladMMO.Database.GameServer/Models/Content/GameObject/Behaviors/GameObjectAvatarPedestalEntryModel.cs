using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using GladMMO.Database;

namespace GladMMO
{
	[Table("gameobject_avatarpedestal")]
	public class GameObjectAvatarPedestalEntryModel : IInstanceableWorldObjectModel, IModelEntryUpdateable<GameObjectAvatarPedestalEntryModel>
	{
		public int ObjectInstanceId => TargetGameObjectId;

		//TODO: This are UNSAFE!
		public Vector3<float> SpawnPosition => TargetGameObject.SpawnPosition;

		public float InitialOrientation => TargetGameObject.InitialOrientation;

		/// <summary>
		/// Defines the <see cref="GameObjectEntryModel"/> instance that
		/// this behavior is attached to. It is the primary
		/// and foreign key to the instance it's attached to.
		/// </summary>
		[Key]
		[Column(Order = 1)]
		[Required]
		[ForeignKey(nameof(TargetGameObject))]
		public int TargetGameObjectId { get; set; }

		//Navigation property
		/// <summary>
		/// The GameObject instance the pedestal behavior is attached to.
		/// </summary>
		public virtual GameObjectEntryModel TargetGameObject { get; private set; }

		/// <summary>
		/// The ID of the avatar this pedestal is for.
		/// </summary>
		[Required]
		[Column(Order = 2)]
		[ForeignKey(nameof(AvatarModel))]
		public long AvatarModelId { get; set; }

		//Navigation property
		/// <summary>
		/// The local spawn point entry that people using this teleporter
		/// will spawn at.
		/// </summary>
		public virtual AvatarEntryModel AvatarModel { get; private set; }

		public GameObjectAvatarPedestalEntryModel(int targetGameObjectId, int avatarModelId)
		{
			if (targetGameObjectId <= 0) throw new ArgumentOutOfRangeException(nameof(targetGameObjectId));
			if (avatarModelId <= 0) throw new ArgumentOutOfRangeException(nameof(avatarModelId));

			TargetGameObjectId = targetGameObjectId;
			AvatarModelId = avatarModelId;
		}

		/// <summary>
		/// EF ctor.
		/// </summary>
		protected GameObjectAvatarPedestalEntryModel()
		{
			
		}

		public void Update([JetBrains.Annotations.NotNull] GameObjectAvatarPedestalEntryModel model)
		{
			if (model == null) throw new ArgumentNullException(nameof(model));

			AvatarModelId = model.AvatarModelId;
		}
	}
}
