using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using NUnit.Framework;

namespace GladMMO
{
	/// <summary>
	/// Runs all the crud repo interface default tests against the NPC Template Repository.
	/// </summary>
	[TestFixture]
	public class NpcTemplateRepCrudTest : GenericCrubRepositoryDefaultTests<NpcDatabaseContext, DatabaseBackedNpcTemplateRepository, int, NPCTemplateModel>
	{
		public override IEnumerable<int> TestCaseKeys => new int[] { 1, 2, 3, 5, 6, 7, 8, 9, 22, 6666, short.MaxValue, int.MaxValue };

		/// <inheritdoc />
		public override NPCTemplateModel BuildRandomModel(bool generateKey)
		{
			Random random = new Random();

			byte[] bytes = new byte[10];
			random.NextBytes(bytes);
			return new NPCTemplateModel(random.Next(), Encoding.ASCII.GetString(bytes));
		}

		/// <inheritdoc />
		public override int ProduceKeyFromModel(NPCTemplateModel model)
		{
			return model.TemplateId;
		}
	}
}