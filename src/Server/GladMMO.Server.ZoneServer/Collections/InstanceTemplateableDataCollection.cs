using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//This is a hack. See: https://stackoverflow.com/a/51297066
	public abstract class EntityGuidMappableTemplateHackForSharedGeneric<TInstanceModelType, TTemplateModelType> : IEntityGuidMappable<TTemplateModelType>
		where TInstanceModelType : IInstanceObjectModel
		where TTemplateModelType : IObjectTemplateModel
	{
		protected Dictionary<int, TTemplateModelType> CreatureTemplateDictionary { get; }

		protected Dictionary<int, TInstanceModelType> CreatureInstanceDictionary { get; }

		protected EntityGuidMappableTemplateHackForSharedGeneric()
		{
			CreatureTemplateDictionary = new Dictionary<int, TTemplateModelType>();
			CreatureInstanceDictionary = new Dictionary<int, TInstanceModelType>();
		}

		public abstract void Add(NetworkEntityGuid key, TTemplateModelType value);

		TTemplateModelType IEntityGuidMappable<NetworkEntityGuid, TTemplateModelType>.this[NetworkEntityGuid key]
		{
			get => GetCreatureTemplate(key);
			set => Add(key, value);
		}

		TTemplateModelType IReadonlyEntityGuidMappable<NetworkEntityGuid, TTemplateModelType>.this[NetworkEntityGuid key] => GetCreatureTemplate(key);

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, TTemplateModelType>.ContainsKey(NetworkEntityGuid key)
		{
			AssertEntityGuidIsCreate(key);

			//If there is an entry for this creature's entry id
			//AND if the template the entry references also exists.
			if(CreatureInstanceDictionary.ContainsKey(key.EntryId))
				if(CreatureTemplateDictionary.ContainsKey(CreatureInstanceDictionary[key.EntryId].TemplateId))
					return true;

			return false;
		}

		protected TTemplateModelType GetCreatureTemplate([NotNull] NetworkEntityGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			TInstanceModelType instanceModel = GetCreatureInstance(key);

			return CreatureTemplateDictionary[instanceModel.TemplateId];
		}

		protected TInstanceModelType GetCreatureInstance([NotNull] NetworkEntityGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			return CreatureInstanceDictionary[key.EntryId];
		}

		public abstract bool RemoveEntityEntry(NetworkEntityGuid entityGuid);

		protected void AssertEntityGuidIsCreate([NotNull] NetworkEntityGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			if(key.EntityType != EntityType.Creature)
				throw new InvalidOperationException($"Entity: {key} is not a creature.");
		}
	}

	//TODO: Create a simplified interface for this too.
	/// <summary>
	/// Specialized <see cref="IEntityGuidMappable{TValue}"/> for creature templates
	/// and creature instances.
	/// It works by using the encoded entry data in the provided <see cref="NetworkEntityGuid"/>s
	/// of actual real spawned entities. Meaning that entities can share this data.
	/// </summary>
	public sealed class InstanceTemplateableDataCollection<TInstanceModelType, TTemplateModelType> : EntityGuidMappableTemplateHackForSharedGeneric<TInstanceModelType, TTemplateModelType>, IEntityGuidMappable<TInstanceModelType>
		where TInstanceModelType : IInstanceObjectModel
		where TTemplateModelType : IObjectTemplateModel
	{
		public InstanceTemplateableDataCollection()
		{
			
		}

		TInstanceModelType IEntityGuidMappable<NetworkEntityGuid, TInstanceModelType>.this[NetworkEntityGuid key]
		{
			get => GetCreatureInstance(key);
			set => Add(key, value);
		}

		TInstanceModelType IReadonlyEntityGuidMappable<NetworkEntityGuid, TInstanceModelType>.this[NetworkEntityGuid key] => GetCreatureInstance(key);

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, TInstanceModelType>.ContainsKey(NetworkEntityGuid key)
		{
			AssertEntityGuidIsCreate(key);
			return CreatureInstanceDictionary.ContainsKey(key.EntryId);
		}

		public override void Add([NotNull] NetworkEntityGuid key, [NotNull] TTemplateModelType value)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			if (value == null) throw new ArgumentNullException(nameof(value));

			CreatureTemplateDictionary.Add(value.TemplateId, value);
		}

		public void Add([NotNull] NetworkEntityGuid key, [NotNull] TInstanceModelType value)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			if (value == null) throw new ArgumentNullException(nameof(value));

			CreatureInstanceDictionary.Add(key.EntryId, value);
		}

		public override bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			//Do nothing, we should not remove anything.
			return false;
		}
	}
}
