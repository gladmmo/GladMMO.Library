using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	[AdditionalRegisterationAs(typeof(IMovementInputChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class MovementInputBroadcastingTickable : OnLocalPlayerSpawnedEventListener, IGameTickable, IMovementInputChangedEventSubscribable
	{
		public const int HEARTBEAT_MILLISECOND_TIME = 500;

		/// <inheritdoc />
		public event EventHandler<MovementInputChangedEventArgs> OnMovementInputDataChanged;

		private float LastHoritzontalInput { get; set; }

		private float LastVerticalInput { get; set; }

		private bool isLocalPlayerSpawned { get; set; } = false;

		private ILog Logger { get; }

		private float HeartBeatCounter { get; set; } = 0.0f;

		public bool isBroadcastingHeartbeat { get; set; } = false;

		private IMovementInputController InputController { get; }

		/// <inheritdoc />
		public MovementInputBroadcastingTickable(ILocalPlayerSpawnedEventSubscribable subscriptionService,
			[NotNull] ILog logger,
			[NotNull] IMovementInputController inputController)
			: base(subscriptionService)
		{
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			InputController = inputController ?? throw new ArgumentNullException(nameof(inputController));
		}

		/// <inheritdoc />
		public void Tick()
		{
			if(!isLocalPlayerSpawned)
				return;

			bool changed = false;

			float horizontal = Math.Sign(InputController.CurrentHorizontal);

			if(Math.Abs(LastHoritzontalInput - horizontal) > 0.005f)
			{
				changed = true;
				LastHoritzontalInput = horizontal;
			}

			float vertical = Math.Sign(InputController.CurrentVertical);

			if(Math.Abs(LastVerticalInput - vertical) > 0.005f)
			{
				changed = true;
				LastVerticalInput = vertical;
			}

			//Add the elaspsed milliseconds.
			if(isBroadcastingHeartbeat)
				HeartBeatCounter += Time.deltaTime * 1000f;

			if((int)HeartBeatCounter > HEARTBEAT_MILLISECOND_TIME)
				Debug.Log("Sending heartbeat movement.");

			//If the input has changed we should dispatch to anyone interested.
			if (changed || (int)HeartBeatCounter > HEARTBEAT_MILLISECOND_TIME)
			{
				MovementInputChangedEventArgs args = new MovementInputChangedEventArgs(vertical, horizontal);
				OnMovementInputDataChanged?.Invoke(this, args);

				//Always reset heartbeat on send.
				HeartBeatCounter = 0.0f;
				isBroadcastingHeartbeat = args.isMoving; //if moving we should be broadcasting heartbeat data
			}
		}

		/// <inheritdoc />
		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Movement input enabled.");

			//Local player is spawned, we should actually handle input now.
			isLocalPlayerSpawned = true;
		}
	}
}