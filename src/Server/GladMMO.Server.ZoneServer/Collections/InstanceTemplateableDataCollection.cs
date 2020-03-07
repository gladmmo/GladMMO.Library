using System;
using System.Collections;
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

		public abstract void Add(ObjectGuid key, TTemplateModelType value);

		TTemplateModelType IEntityGuidMappable<ObjectGuid, TTemplateModelType>.this[ObjectGuid key]
		{
			get => GetCreatureTemplate(key);
			set => Add(key, value);
		}

		TTemplateModelType IReadonlyEntityGuidMappable<ObjectGuid, TTemplateModelType>.this[ObjectGuid key] => GetCreatureTemplate(key);

		bool IReadonlyEntityGuidMappable<ObjectGuid, TTemplateModelType>.ContainsKey(ObjectGuid key)
		{
			AssertEntityGuidIsCreate(key);

			//If there is an entry for this creature's entry id
			//AND if the template the entry references also exists.
			if(CreatureInstanceDictionary.ContainsKey(key.EntryId))
				if(CreatureTemplateDictionary.ContainsKey(CreatureInstanceDictionary[key.EntryId].TemplateId))
					return true;

			return false;
		}

		protected TTemplateModelType GetCreatureTemplate([NotNull] ObjectGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			TInstanceModelType instanceModel = GetCreatureInstance(key);

			return CreatureTemplateDictionary[instanceModel.TemplateId];
		}

		protected TInstanceModelType GetCreatureInstance([NotNull] ObjectGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			return CreatureInstanceDictionary[key.EntryId];
		}

		public abstract bool RemoveEntityEntry(ObjectGuid entityGuid);

		protected void AssertEntityGuidIsCreate([NotNull] ObjectGuid key)
		{
			if(key == null) throw new ArgumentNullException(nameof(key));

			if(key.EntityType != EntityType.Creature)
				throw new InvalidOperationException($"Entity: {key} is not a creature.");
		}

		public bool TryGetValue(ObjectGuid key, out TTemplateModelType value)
		{
			return this.CreatureTemplateDictionary.TryGetValue(key.EntryId, out value);
		}

		public IEnumerator<TTemplateModelType> GetEnumerator()
		{
			return CreatureTemplateDictionary.Values.GetEnumerator();
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}

	//TODO: Create a simplified interface for this too.
	/// <summary>
	/// Specialized <see cref="IEntityGuidMappable{TValue}"/> for creature templates
	/// and creature instances.
	/// It works by using the encoded entry data in the provided <see cref="ObjectGuid"/>s
	/// of actual real spawned entities. Meaning that entities can share this data.
	/// </summary>
	public sealed class InstanceTemplateableDataCollection<TInstanceModelType, TTemplateModelType> : EntityGuidMappableTemplateHackForSharedGeneric<TInstanceModelType, TTemplateModelType>, IEntityGuidMappable<TInstanceModelType>
		where TInstanceModelType : IInstanceObjectModel
		where TTemplateModelType : IObjectTemplateModel
	{
		public InstanceTemplateableDataCollection()
		{
			
		}

		TInstanceModelType IEntityGuidMappable<ObjectGuid, TInstanceModelType>.this[ObjectGuid key]
		{
			get => GetCreatureInstance(key);
			set => Add(key, value);
		}

		TInstanceModelType IReadonlyEntityGuidMappable<ObjectGuid, TInstanceModelType>.this[ObjectGuid key] => GetCreatureInstance(key);

		bool IReadonlyEntityGuidMappable<ObjectGuid, TInstanceModelType>.ContainsKey(ObjectGuid key)
		{
			AssertEntityGuidIsCreate(key);
			return CreatureInstanceDictionary.ContainsKey(key.EntryId);
		}

		public override void Add([NotNull] ObjectGuid key, [NotNull] TTemplateModelType value)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			if (value == null) throw new ArgumentNullException(nameof(value));

			CreatureTemplateDictionary.Add(value.TemplateId, value);
		}

		public void Add([NotNull] ObjectGuid key, [NotNull] TInstanceModelType value)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));
			if (value == null) throw new ArgumentNullException(nameof(value));

			CreatureInstanceDictionary.Add(key.EntryId, value);
		}

		public override bool RemoveEntityEntry(ObjectGuid entityGuid)
		{
			//Do nothing, we should not remove anything.
			return false;
		}

		public bool TryGetValue(ObjectGuid key, out TInstanceModelType value)
		{
			return this.CreatureInstanceDictionary.TryGetValue(key.EntryId, out value);
		}

		IEnumerator<TInstanceModelType> IEnumerable<TInstanceModelType>.GetEnumerator()
		{
			return this.CreatureInstanceDictionary.Values.GetEnumerator();
		}
	}
}
