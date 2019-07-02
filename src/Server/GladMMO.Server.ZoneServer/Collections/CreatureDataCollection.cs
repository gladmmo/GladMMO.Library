using System;
using System.Collections.Generic;
using System.Text;
using Glader.Essentials;

namespace GladMMO
{
	//TODO: Create a simplified interface for this too.
	/// <summary>
	/// Specialized <see cref="IEntityGuidMappable{TValue}"/> for creature templates
	/// and creature instances.
	/// It works by using the encoded entry data in the provided <see cref="NetworkEntityGuid"/>s
	/// of actual real spawned entities. Meaning that entities can share this data.
	/// </summary>
	public sealed class CreatureDataCollection : IEntityGuidMappable<CreatureTemplateModel>, IEntityGuidMappable<CreatureInstanceModel>
	{
		private Dictionary<int, CreatureTemplateModel> CreatureTemplateDictionary { get; }

		private Dictionary<int, CreatureInstanceModel> CreatureInstanceDictionary { get; }

		public CreatureDataCollection()
		{
			CreatureTemplateDictionary = new Dictionary<int, CreatureTemplateModel>();
			CreatureInstanceDictionary = new Dictionary<int, CreatureInstanceModel>();
		}

		CreatureTemplateModel IEntityGuidMappable<NetworkEntityGuid, CreatureTemplateModel>.this[NetworkEntityGuid key]
		{
			get => GetCreatureTemplate(key);
			set => Add(key, value);
		}

		CreatureInstanceModel IEntityGuidMappable<NetworkEntityGuid, CreatureInstanceModel>.this[NetworkEntityGuid key]
		{
			get => GetCreatureInstance(key);
			set => Add(key, value);
		}

		CreatureTemplateModel IReadonlyEntityGuidMappable<NetworkEntityGuid, CreatureTemplateModel>.this[NetworkEntityGuid key] => GetCreatureTemplate(key);

		CreatureInstanceModel IReadonlyEntityGuidMappable<NetworkEntityGuid, CreatureInstanceModel>.this[NetworkEntityGuid key] => GetCreatureInstance(key);

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, CreatureTemplateModel>.ContainsKey(NetworkEntityGuid key)
		{
			AssertEntityGuidIsCreate(key);

			//If there is an entry for this creature's entry id
			//AND if the template the entry references also exists.
			if (CreatureInstanceDictionary.ContainsKey(key.EntryId))
				if (CreatureTemplateDictionary.ContainsKey(CreatureInstanceDictionary[key.EntryId].TemplateId))
					return true;

			return false;
		}

		bool IReadonlyEntityGuidMappable<NetworkEntityGuid, CreatureInstanceModel>.ContainsKey(NetworkEntityGuid key)
		{
			AssertEntityGuidIsCreate(key);
			return CreatureInstanceDictionary.ContainsKey(key.EntryId);
		}

		private void AssertEntityGuidIsCreate([NotNull] NetworkEntityGuid key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			if(key.EntityType != EntityType.Creature)
				throw new InvalidOperationException($"Entity: {key} is not a creature.");
		}

		private CreatureTemplateModel GetCreatureTemplate([NotNull] NetworkEntityGuid key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			CreatureInstanceModel instanceModel = GetCreatureInstance(key);

			return CreatureTemplateDictionary[instanceModel.TemplateId];
		}

		private CreatureInstanceModel GetCreatureInstance([NotNull] NetworkEntityGuid key)
		{
			if (key == null) throw new ArgumentNullException(nameof(key));

			return CreatureInstanceDictionary[key.EntryId];
		}

		public void Add(NetworkEntityGuid key, CreatureTemplateModel value)
		{
			throw new NotImplementedException();
		}

		public void Add(NetworkEntityGuid key, CreatureInstanceModel value)
		{
			throw new NotImplementedException();
		}

		public bool RemoveEntityEntry(NetworkEntityGuid entityGuid)
		{
			//Do nothing, we should not remove anything.
			return false;
		}
	}
}
