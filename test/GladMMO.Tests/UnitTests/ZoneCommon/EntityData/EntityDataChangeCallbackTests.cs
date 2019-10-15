using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Moq;
using NUnit.Framework;

namespace GladMMO
{
	[TestFixture]
	public sealed class EntityDataChangeCallbackTests
	{
		[Test]
		public void Test_Can_Create_Ctor_Without_Exception()
		{
			Assert.DoesNotThrow(() => new EntityDataChangeCallbackManager());
		}

		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Register_Callback_Without_Throw(long guid, int fieldType)
		{
			//arrange
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			Assert.DoesNotThrow(() => callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) => { }));
		}

		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Register_Multple_Callback_Without_Throw(long guid, int fieldType)
		{
			//arrange
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			Assert.DoesNotThrow(() => callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) => { }));
			Assert.DoesNotThrow(() => callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) => { }));
		}

		//TODO: Fix an renable tests.
		/*
		[Test]
		public void Test_Can_ServiceCallbacks_For_Empty_CallbackRegister()
		{
			//arrange
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//assert
			Assert.DoesNotThrow(() => callbackManager.InvokeChangeEvents(NetworkEntityGuid.Empty, 3, 5));
		}

		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Registered_Callback_Called(long guid, int fieldType)
		{
			//arrange
			Mock<IEnumerable> testCallback = new Mock<IEnumerable>(MockBehavior.Loose);
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//act
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback.Object.GetEnumerator();
			});

			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);

			//assert
			testCallback.Verify(enumerable => enumerable.GetEnumerator(), Times.Once);
		}

		//Mostly check the next call will still invoke the same callbacks
		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Registered_Callback_Called_Be_Called_Twice(long guid, int fieldType)
		{
			//arrange
			Mock<IEnumerable> testCallback = new Mock<IEnumerable>(MockBehavior.Loose);
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//act
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback.Object.GetEnumerator();
			});

			//Call twice
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);

			//assert
			testCallback.Verify(enumerable => enumerable.GetEnumerator(), Times.Exactly(2));
		}

		//Mostly check the next call will still invoke the same callbacks
		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Registered_Callback_Called_Not_Called_Twice_If_Unregistered(long guid, int fieldType)
		{
			//arrange
			Mock<IEnumerable> testCallback = new Mock<IEnumerable>(MockBehavior.Loose);
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//act
			IEntityDataEventUnregisterable unregisterable = callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback.Object.GetEnumerator();
			});

			//Call twice but unregister after the first call.
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);
			unregisterable.Unregister();
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);

			//assert
			testCallback.Verify(enumerable => enumerable.GetEnumerator(), Times.Once);
		}

		//Mostly check the next call will still invoke the same callbacks
		[Test]
		[TestCase(5, 1)]
		[TestCase(5, 2)]
		[TestCase(555, 3)]
		public void Test_Can_Registered_Multiple_Callbacks_Called_Be_Called_Twice(long guid, int fieldType)
		{
			//arrange
			Mock<IEnumerable> testCallback = new Mock<IEnumerable>(MockBehavior.Loose);
			Mock<IEnumerable> testCallback2 = new Mock<IEnumerable>(MockBehavior.Loose);
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//act
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback.Object.GetEnumerator();
			});

			//Of the same type
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)guid), fieldType, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback2.Object.GetEnumerator();
			});

			//Call twice
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)guid), fieldType, 5);

			//assert
			testCallback.Verify(enumerable => enumerable.GetEnumerator(), Times.Exactly(2));
			testCallback2.Verify(enumerable => enumerable.GetEnumerator(), Times.Exactly(2));
		}

		[Test]
		public void Test_Can_Registered_Multiple_Seperate_Callbacks_Called_Be_Called_Only()
		{
			//arrange
			Mock<IEnumerable> testCallback = new Mock<IEnumerable>(MockBehavior.Loose);
			Mock<IEnumerable> testCallback2 = new Mock<IEnumerable>(MockBehavior.Loose);
			EntityDataChangeCallbackManager callbackManager = new EntityDataChangeCallbackManager();

			//act
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)5), 2, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback.Object.GetEnumerator();
			});

			//Of the same type
			callbackManager.RegisterCallback<float>(new NetworkEntityGuid((ulong)6), 2, (eg, args) =>
			{
				//Call so we can check for test purposes
				testCallback2.Object.GetEnumerator();
			});

			//Call twice
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)5), 2, 5);
			callbackManager.InvokeChangeEvents(new NetworkEntityGuid((ulong)6), 2, 5);

			//assert
			testCallback.Verify(enumerable => enumerable.GetEnumerator(), Times.Exactly(1));
			testCallback2.Verify(enumerable => enumerable.GetEnumerator(), Times.Exactly(1));
		}*/
	}
}
