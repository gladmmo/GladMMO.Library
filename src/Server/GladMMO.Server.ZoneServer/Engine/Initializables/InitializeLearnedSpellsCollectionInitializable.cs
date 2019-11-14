using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Glader.Essentials;

namespace GladMMO
{
	[ServerSceneTypeCreate(ServerSceneType.Default)]
	public sealed class InitializeLearnedSpellsCollectionInitializable : IGameInitializable
	{
		private ILearnedSpellsCollection LearnedSpellsCollection { get; }

		private ISpellEntryDataServiceClient SpellEntryServiceClient { get; }

		public InitializeLearnedSpellsCollectionInitializable([NotNull] ILearnedSpellsCollection learnedSpellsCollection,
			[NotNull] ISpellEntryDataServiceClient spellEntryServiceClient)
		{
			LearnedSpellsCollection = learnedSpellsCollection ?? throw new ArgumentNullException(nameof(learnedSpellsCollection));
			SpellEntryServiceClient = spellEntryServiceClient ?? throw new ArgumentNullException(nameof(spellEntryServiceClient));
		}

		public async Task OnGameInitialized()
		{
			var responseModel = await SpellEntryServiceClient.GetLevelLearnedSpellsAsync();

			foreach(var learnedSpellData in responseModel.LevelLearnedSpells)
				LearnedSpellsCollection.Add(learnedSpellData);
		}
	}
}
