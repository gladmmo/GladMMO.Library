using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using FreecraftCore;
using Newtonsoft.Json;

namespace GladMMO
{
	[JsonObject]
	public sealed class CharacterCreationRequest
	{
		/// <summary>
		/// Should be an ID specified in <see cref="ChrRacesEntry{TStringType}"/>
		/// </summary>
		[JsonProperty]
		public CharacterRace RequestedRace { get; private set; }

		/// <summary>
		/// Should be an ID specified in <see cref="ChrClassesEntry{TStringType}"/>
		/// </summary>
		[JsonProperty]
		public CharacterClass RequestedClass { get; private set; }

		/// <summary>
		/// Non-normalized requested character name.
		/// </summary>
		[JsonProperty]
		public string RequestedName { get; private set; }

		public CharacterCreationRequest(CharacterRace requestedRace, CharacterClass requestedClass)
		{
			if (!Enum.IsDefined(typeof(CharacterRace), requestedRace)) throw new InvalidEnumArgumentException(nameof(requestedRace), (int) requestedRace, typeof(CharacterRace));
			if (!Enum.IsDefined(typeof(CharacterClass), requestedClass)) throw new InvalidEnumArgumentException(nameof(requestedClass), (int) requestedClass, typeof(CharacterClass));
			RequestedRace = requestedRace;
			RequestedClass = requestedClass;
		}

		/// <summary>
		/// Serializer ctor.
		/// </summary>
		[JsonConstructor]
		private CharacterCreationRequest()
		{
			
		}

		public bool isValidCombination(IEnumerable<CharBaseInfoEntry> baseInfoEntries)
		{
			//Check if we're a known combo base on CharBaseInfo DBC data.
			return baseInfoEntries.Any(e => e.ClassId == (int) RequestedClass && e.RaceId == (int) RequestedRace);
		}
	}
}
