using System;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
	//TODO: extract
	[AdditionalRegisterationAs(typeof(IFactoryCreatable<CameraInputData, EmptyFactoryContext>))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class TestCameraDataFactory : IFactoryCreatable<CameraInputData, EmptyFactoryContext>, IGameTickable
	{
		private ILocalPlayerDetails Details { get; }

		private IReadonlyEntityGuidMappable<GameObject> GameobjectMappable { get; }

		private IReadonlyEntityGuidMappable<Camera> CameraObjectMappable { get; }

		public TestCameraDataFactory([NotNull] ILocalPlayerDetails details, [NotNull] IReadonlyEntityGuidMappable<GameObject> gameobjectMappable, [NotNull] IReadonlyEntityGuidMappable<Camera> cameraObjectMappable)
		{
			Details = details ?? throw new ArgumentNullException(nameof(details));
			GameobjectMappable = gameobjectMappable ?? throw new ArgumentNullException(nameof(gameobjectMappable));
			CameraObjectMappable = cameraObjectMappable ?? throw new ArgumentNullException(nameof(cameraObjectMappable));
		}

		public CameraInputData Create(EmptyFactoryContext context)
		{
			return new CameraInputData()
			{
				RootRotationalObject = GameobjectMappable.RetrieveEntity(Details.LocalPlayerGuid),
				CameraGameObject = CameraObjectMappable.RetrieveEntity(Details.LocalPlayerGuid).gameObject,
				CurrentRotation = CameraObjectMappable.RetrieveEntity(Details.LocalPlayerGuid).transform.eulerAngles
			};
		}

		public void Tick()
		{

		}
	}

	//TODO: Extract
	public sealed class CameraInputData
	{
		public float LookSpeed = 3;
		public Vector2 MaxLookAngle = new Vector2(60.0f, 60.0f);
		public GameObject CameraGameObject;
		public GameObject RootRotationalObject;
		public Vector3 CurrentRotation;
	}

	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CameraInputBroadcastingTickable : OnLocalPlayerSpawnedEventListener, IGameTickable
	{
		private ILog Logger { get; }

		private bool isLocalPlayerSpawned { get; set; } = false;

		private Lazy<CameraInputData> Data { get; }

		public CameraInputBroadcastingTickable(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			[NotNull] ILog logger,
			[NotNull] IFactoryCreatable<CameraInputData, EmptyFactoryContext> cameraInputDataFactory) 
			: base(subscriptionService)
		{
			if (cameraInputDataFactory == null) throw new ArgumentNullException(nameof(cameraInputDataFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));

			Data = new Lazy<CameraInputData>(() => cameraInputDataFactory.Create(EmptyFactoryContext.Instance));
		}

		/// <inheritdoc />
		protected override void OnLocalPlayerSpawned(LocalPlayerSpawnedEventArgs args)
		{
			if(Logger.IsInfoEnabled)
				Logger.Info($"Movement input enabled.");

			//Local player is spawned, we should actually handle input now.
			isLocalPlayerSpawned = true;
		}

		public void Tick()
		{
			if (!isLocalPlayerSpawned)
				return;

			CameraInputData _cameraInputData = Data.Value;

			float rotationalMovement = Input.GetAxis("Mouse X") * _cameraInputData.LookSpeed;

			//TODO: Kinda slow
			_cameraInputData.CurrentRotation = new Vector3(Mathf.Clamp(-Input.GetAxis("Mouse Y") * _cameraInputData.LookSpeed + _cameraInputData.CurrentRotation.x, -_cameraInputData.MaxLookAngle.x, _cameraInputData.MaxLookAngle.x), 0, 0);

			_cameraInputData.CameraGameObject.transform.localEulerAngles = _cameraInputData.CurrentRotation;
			_cameraInputData.RootRotationalObject.transform.Rotate(Vector3.up, rotationalMovement);
		}
	}
}
