using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	/// <summary>
	/// Type that helps with managing the project longterm by
	/// offering methods and properties that can be used to determine if a missing critical feature is not
	/// available in the version it should be in.
	/// </summary>
	public static class ProjectVersionStage
	{
		public static void AssertInternalTesting()
		{
			//TODO: When the project leaves internal testing this should throw on this assert.
			return;
		}

		public static void AssertAlpha()
		{
			//TODO: When the project leaves alpha we should throw on this assert.
			return;
		}

		public static void AssertBeta()
		{
			//TODO: When the project leaves beta we should throw on this assert.
			return;
		}
	}
}
