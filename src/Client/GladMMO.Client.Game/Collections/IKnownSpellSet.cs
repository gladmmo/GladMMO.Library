using System;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Collection for known spell ids.
	/// </summary>
	public interface IKnownSpellSet : IEnumerable<int>
	{
		bool Contains(int spellId);

		bool Add(int spellId);
	}
}
