using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Autofac.Features.AttributeFilters;

namespace GladMMO
{
	public interface IGossipWindowCollection : IEnumerable<IUIWindow>
	{
		void CloseAll();
	}

	public class DefaultGossipWindowCollection : IGossipWindowCollection
	{
		private IUIWindow[] Windows { get; }

		public DefaultGossipWindowCollection([KeyFilter(UnityUIRegisterationKey.GossipWindow)] [NotNull] IUIGossipWindow gossipWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestWindow)] [NotNull] IUIQuestWindow questWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestRequirementsWindow)] [NotNull] IUIQuestRequirementWindow questRequirementsWindow,
			[KeyFilter(UnityUIRegisterationKey.QuestCompleteWindow)] [NotNull] IUIQuestCompleteWindow questCompleteWindow)
		{
			Windows = new IUIWindow[]
			{
				gossipWindow,
				questWindow,
				questCompleteWindow,
				questRequirementsWindow
			};
		}

		public IEnumerator<IUIWindow> GetEnumerator()
		{
			return ((IEnumerable<IUIWindow>) Windows).GetEnumerator();
		}

		public void CloseAll()
		{
			foreach(var window in Windows)
				window.RootElement.SetElementActive(false);
		}

		IEnumerator IEnumerable.GetEnumerator()
		{
			return GetEnumerator();
		}
	}
}
