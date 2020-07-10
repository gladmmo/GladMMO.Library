using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.DependencyInjection;

namespace GladMMO
{
	/// <summary>
	/// Auto-generated code. Do not change.
	/// </summary>
	public static class CharactersDatabaseDependencyInjectionExtensions
	{
		public static IServiceCollection RegisterCharactersDatabaseRepositoryTypes(this IServiceCollection collection)
		{
			collection.AddTransient<ITrinityAccountDataRepository, TrinityCoreAccountDataRepository>();
			collection.AddTransient<ITrinityAccountInstanceTimesRepository, TrinityCoreAccountInstanceTimesRepository>();
			collection.AddTransient<ITrinityAccountTutorialRepository, TrinityCoreAccountTutorialRepository>();
			collection.AddTransient<ITrinityAddonsRepository, TrinityCoreAddonsRepository>();
			collection.AddTransient<ITrinityArenaTeamRepository, TrinityCoreArenaTeamRepository>();
			collection.AddTransient<ITrinityArenaTeamMemberRepository, TrinityCoreArenaTeamMemberRepository>();
			collection.AddTransient<ITrinityAuctionbiddersRepository, TrinityCoreAuctionbiddersRepository>();
			collection.AddTransient<ITrinityAuctionhouseRepository, TrinityCoreAuctionhouseRepository>();
			collection.AddTransient<ITrinityBannedAddonsRepository, TrinityCoreBannedAddonsRepository>();
			collection.AddTransient<ITrinityBattlegroundDesertersRepository, TrinityCoreBattlegroundDesertersRepository>();
			collection.AddTransient<ITrinityBugreportRepository, TrinityCoreBugreportRepository>();
			collection.AddTransient<ITrinityCalendarEventsRepository, TrinityCoreCalendarEventsRepository>();
			collection.AddTransient<ITrinityCalendarInvitesRepository, TrinityCoreCalendarInvitesRepository>();
			collection.AddTransient<ITrinityChannelsRepository, TrinityCoreChannelsRepository>();
			collection.AddTransient<ITrinityCharacterAccountDataRepository, TrinityCoreCharacterAccountDataRepository>();
			collection.AddTransient<ITrinityCharacterAchievementRepository, TrinityCoreCharacterAchievementRepository>();
			collection.AddTransient<ITrinityCharacterAchievementProgressRepository, TrinityCoreCharacterAchievementProgressRepository>();
			collection.AddTransient<ITrinityCharacterActionRepository, TrinityCoreCharacterActionRepository>();
			collection.AddTransient<ITrinityCharacterArenaStatsRepository, TrinityCoreCharacterArenaStatsRepository>();
			collection.AddTransient<ITrinityCharacterAuraRepository, TrinityCoreCharacterAuraRepository>();
			collection.AddTransient<ITrinityCharacterBannedRepository, TrinityCoreCharacterBannedRepository>();
			collection.AddTransient<ITrinityCharacterBattlegroundDataRepository, TrinityCoreCharacterBattlegroundDataRepository>();
			collection.AddTransient<ITrinityCharacterBattlegroundRandomRepository, TrinityCoreCharacterBattlegroundRandomRepository>();
			collection.AddTransient<ITrinityCharacterDeclinednameRepository, TrinityCoreCharacterDeclinednameRepository>();
			collection.AddTransient<ITrinityCharacterEquipmentsetsRepository, TrinityCoreCharacterEquipmentsetsRepository>();
			collection.AddTransient<ITrinityCharacterFishingstepsRepository, TrinityCoreCharacterFishingstepsRepository>();
			collection.AddTransient<ITrinityCharacterGiftsRepository, TrinityCoreCharacterGiftsRepository>();
			collection.AddTransient<ITrinityCharacterGlyphsRepository, TrinityCoreCharacterGlyphsRepository>();
			collection.AddTransient<ITrinityCharacterHomebindRepository, TrinityCoreCharacterHomebindRepository>();
			collection.AddTransient<ITrinityCharacterInstanceRepository, TrinityCoreCharacterInstanceRepository>();
			collection.AddTransient<ITrinityCharacterInventoryRepository, TrinityCoreCharacterInventoryRepository>();
			collection.AddTransient<ITrinityCharacterPetRepository, TrinityCoreCharacterPetRepository>();
			collection.AddTransient<ITrinityCharacterPetDeclinednameRepository, TrinityCoreCharacterPetDeclinednameRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusRepository, TrinityCoreCharacterQueststatusRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusDailyRepository, TrinityCoreCharacterQueststatusDailyRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusMonthlyRepository, TrinityCoreCharacterQueststatusMonthlyRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusRewardedRepository, TrinityCoreCharacterQueststatusRewardedRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusSeasonalRepository, TrinityCoreCharacterQueststatusSeasonalRepository>();
			collection.AddTransient<ITrinityCharacterQueststatusWeeklyRepository, TrinityCoreCharacterQueststatusWeeklyRepository>();
			collection.AddTransient<ITrinityCharacterReputationRepository, TrinityCoreCharacterReputationRepository>();
			collection.AddTransient<ITrinityCharacterSkillsRepository, TrinityCoreCharacterSkillsRepository>();
			collection.AddTransient<ITrinityCharacterSocialRepository, TrinityCoreCharacterSocialRepository>();
			collection.AddTransient<ITrinityCharacterSpellRepository, TrinityCoreCharacterSpellRepository>();
			collection.AddTransient<ITrinityCharacterSpellCooldownRepository, TrinityCoreCharacterSpellCooldownRepository>();
			collection.AddTransient<ITrinityCharacterStatsRepository, TrinityCoreCharacterStatsRepository>();
			collection.AddTransient<ITrinityCharacterTalentRepository, TrinityCoreCharacterTalentRepository>();
			collection.AddTransient<ITrinityCharactersRepository, TrinityCoreCharactersRepository>();
			collection.AddTransient<ITrinityCorpseRepository, TrinityCoreCorpseRepository>();
			collection.AddTransient<ITrinityGameEventConditionSaveRepository, TrinityCoreGameEventConditionSaveRepository>();
			collection.AddTransient<ITrinityGameEventSaveRepository, TrinityCoreGameEventSaveRepository>();
			collection.AddTransient<ITrinityGmSubsurveyRepository, TrinityCoreGmSubsurveyRepository>();
			collection.AddTransient<ITrinityGmSurveyRepository, TrinityCoreGmSurveyRepository>();
			collection.AddTransient<ITrinityGmTicketRepository, TrinityCoreGmTicketRepository>();
			collection.AddTransient<ITrinityGroupInstanceRepository, TrinityCoreGroupInstanceRepository>();
			collection.AddTransient<ITrinityGroupMemberRepository, TrinityCoreGroupMemberRepository>();
			collection.AddTransient<ITrinityGroupsRepository, TrinityCoreGroupsRepository>();
			collection.AddTransient<ITrinityGuildRepository, TrinityCoreGuildRepository>();
			collection.AddTransient<ITrinityGuildBankEventlogRepository, TrinityCoreGuildBankEventlogRepository>();
			collection.AddTransient<ITrinityGuildBankItemRepository, TrinityCoreGuildBankItemRepository>();
			collection.AddTransient<ITrinityGuildBankRightRepository, TrinityCoreGuildBankRightRepository>();
			collection.AddTransient<ITrinityGuildBankTabRepository, TrinityCoreGuildBankTabRepository>();
			collection.AddTransient<ITrinityGuildEventlogRepository, TrinityCoreGuildEventlogRepository>();
			collection.AddTransient<ITrinityGuildMemberRepository, TrinityCoreGuildMemberRepository>();
			collection.AddTransient<ITrinityGuildMemberWithdrawRepository, TrinityCoreGuildMemberWithdrawRepository>();
			collection.AddTransient<ITrinityGuildRankRepository, TrinityCoreGuildRankRepository>();
			collection.AddTransient<ITrinityInstanceRepository, TrinityCoreInstanceRepository>();
			collection.AddTransient<ITrinityInstanceResetRepository, TrinityCoreInstanceResetRepository>();
			collection.AddTransient<ITrinityItemInstanceRepository, TrinityCoreItemInstanceRepository>();
			collection.AddTransient<ITrinityItemLootItemsRepository, TrinityCoreItemLootItemsRepository>();
			collection.AddTransient<ITrinityItemLootMoneyRepository, TrinityCoreItemLootMoneyRepository>();
			collection.AddTransient<ITrinityItemRefundInstanceRepository, TrinityCoreItemRefundInstanceRepository>();
			collection.AddTransient<ITrinityItemSoulboundTradeDataRepository, TrinityCoreItemSoulboundTradeDataRepository>();
			collection.AddTransient<ITrinityLagReportsRepository, TrinityCoreLagReportsRepository>();
			collection.AddTransient<ITrinityLfgDataRepository, TrinityCoreLfgDataRepository>();
			collection.AddTransient<ITrinityMailRepository, TrinityCoreMailRepository>();
			collection.AddTransient<ITrinityMailItemsRepository, TrinityCoreMailItemsRepository>();
			collection.AddTransient<ITrinityPetAuraRepository, TrinityCorePetAuraRepository>();
			collection.AddTransient<ITrinityPetSpellRepository, TrinityCorePetSpellRepository>();
			collection.AddTransient<ITrinityPetSpellCooldownRepository, TrinityCorePetSpellCooldownRepository>();
			collection.AddTransient<ITrinityPetitionRepository, TrinityCorePetitionRepository>();
			collection.AddTransient<ITrinityPetitionSignRepository, TrinityCorePetitionSignRepository>();
			collection.AddTransient<ITrinityPoolQuestSaveRepository, TrinityCorePoolQuestSaveRepository>();
			collection.AddTransient<ITrinityPvpstatsBattlegroundsRepository, TrinityCorePvpstatsBattlegroundsRepository>();
			collection.AddTransient<ITrinityPvpstatsPlayersRepository, TrinityCorePvpstatsPlayersRepository>();
			collection.AddTransient<ITrinityQuestTrackerRepository, TrinityCoreQuestTrackerRepository>();
			collection.AddTransient<ITrinityReservedNameRepository, TrinityCoreReservedNameRepository>();
			collection.AddTransient<ITrinityRespawnRepository, TrinityCoreRespawnRepository>();
			collection.AddTransient<ITrinityUpdatesRepository, TrinityCoreUpdatesRepository>();
			collection.AddTransient<ITrinityUpdatesIncludeRepository, TrinityCoreUpdatesIncludeRepository>();
			collection.AddTransient<ITrinityWardenActionRepository, TrinityCoreWardenActionRepository>();
			collection.AddTransient<ITrinityWorldstatesRepository, TrinityCoreWorldstatesRepository>();

			return collection;
		}
	}
}