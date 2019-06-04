using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public class InterestTests
	{
		[Test]
		public void Test_DequeueCommand_Handles_Entering_Entities()
		{
			//arrange
			//InterestDequeueSetCommand
			InterestCollection interestCollection = new InterestCollection();
			interestCollection.Register(new NetworkEntityGuid(1), new NetworkEntityGuid(1));
			interestCollection.Register(new NetworkEntityGuid(2), new NetworkEntityGuid(2));

			//act
			Assert.AreEqual(0, interestCollection.ContainedEntities.Count);
			InterestDequeueSetCommand dequeue = new InterestDequeueSetCommand(interestCollection, interestCollection);
			dequeue.Execute();

			//assert
			Assert.AreEqual(2, interestCollection.ContainedEntities.Count);
			Assert.True(interestCollection.EnteringDequeueable.isEmpty);
		}

		[Test]
		public void Test_DequeueCommand_Handles_Leaving_Entities()
		{
			//arrange
			//InterestDequeueSetCommand
			InterestCollection interestCollection = new InterestCollection();
			interestCollection.Add(new NetworkEntityGuid(1));
			interestCollection.Add(new NetworkEntityGuid(2));

			interestCollection.Unregister(new NetworkEntityGuid(1));
			interestCollection.Unregister(new NetworkEntityGuid(2));

			//act
			Assert.AreEqual(2, interestCollection.ContainedEntities.Count, "Has no contained entites.");
			InterestDequeueSetCommand dequeue = new InterestDequeueSetCommand(interestCollection, interestCollection);
			dequeue.Execute();

			//assert
			Assert.AreEqual(0, interestCollection.ContainedEntities.Count, "Expected the contained entities to be removed, but some remained.");
			Assert.True(interestCollection.LeavingDequeueable.isEmpty);
		}
	}
}
