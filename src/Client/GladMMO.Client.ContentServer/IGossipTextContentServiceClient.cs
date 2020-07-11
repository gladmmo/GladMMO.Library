﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Refit;

namespace GladMMO
{
	[Headers("User-Agent: SDK 0.0.1")]
	public interface IGossipTextContentServiceClient
	{
		[Get("/api/gossiptext/quest/{id}")]
		Task<ResponseModel<QuestTextContentModel, GameContentQueryResponseCode>> GetQuestGossipTextAsync([AliasAs("id")] int questId);

		[Get("/api/gossiptext/creature/{id}")]
		Task<string> GetCreatureGossipTextAsync([AliasAs("id")] int textId);

		[Get("/api/gossiptext/questincomplete/{id}")]
		Task<ResponseModel<string, GameContentQueryResponseCode>> GetQuestIncompleteGossipTextAsync([AliasAs("id")] int questId);

		[Get("/api/gossiptext/questcomplete/{id}")]
		Task<ResponseModel<string, GameContentQueryResponseCode>> GetQuestCompleteGossipTextAsync([AliasAs("id")] int questId);
	}
}
