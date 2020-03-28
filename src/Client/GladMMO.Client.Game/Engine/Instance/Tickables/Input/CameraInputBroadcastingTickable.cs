using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Common.Logging;
using Glader.Essentials;
using UnityEngine;

namespace GladMMO
{
#warning This needs some MAJOR cleanup, this entire file

	//TODO: extract
	[AdditionalRegisterationAs(typeof(IFactoryCreatable<CameraInputData, EmptyFactoryContext>))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public class TestCameraDataFactory : IFactoryCreatable<CameraInputData, EmptyFactoryContext>, IGameTickable
	{
		private ILocalPlayerDetails Details { get; }

		private IReadonlyEntityGuidMappable<EntityGameObjectDirectory> CameraObjectMappable { get; }

		public TestCameraDataFactory([NotNull] ILocalPlayerDetails details, [NotNull] IReadonlyEntityGuidMappable<EntityGameObjectDirectory> cameraObjectMappable)
		{
			Details = details ?? throw new ArgumentNullException(nameof(details));
			CameraObjectMappable = cameraObjectMappable ?? throw new ArgumentNullException(nameof(cameraObjectMappable));
		}

		public CameraInputData Create(EmptyFactoryContext context)
		{
			EntityGameObjectDirectory directory = CameraObjectMappable.RetrieveEntity(Details.LocalPlayerGuid);

			return new CameraInputData()
			{
				RootRotationalObject = directory.GetGameObject(EntityGameObjectDirectory.Type.Root),
				CameraGameObject = directory.GetGameObject(EntityGameObjectDirectory.Type.CameraRoot),
				CurrentRotation = directory.GetGameObject(EntityGameObjectDirectory.Type.Root).transform.eulerAngles
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

	[AdditionalRegisterationAs(typeof(ICameraInputChangedEventSubscribable))]
	[SceneTypeCreateGladMMO(GameSceneType.InstanceServerScene)]
	public sealed class CameraInputBroadcastingTickable : OnLocalPlayerSpawnedEventListener, IGameTickable, ICameraInputChangedEventSubscribable
	{
		private ILog Logger { get; }

		private bool isLocalPlayerSpawned { get; set; } = false;

		private Lazy<CameraInputData> Data { get; }

		public event EventHandler<CameraInputChangedEventArgs> OnCameraInputChange;

		private ICameraInputController CameraInputController { get; }

		public CameraInputBroadcastingTickable(ILocalPlayerSpawnedEventSubscribable subscriptionService, 
			[NotNull] ILog logger,
			[NotNull] IFactoryCreatable<CameraInputData, EmptyFactoryContext> cameraInputDataFactory,
			[NotNull] ICameraInputController cameraInputController) 
			: base(subscriptionService)
		{
			if (cameraInputDataFactory == null) throw new ArgumentNullException(nameof(cameraInputDataFactory));
			Logger = logger ?? throw new ArgumentNullException(nameof(logger));
			CameraInputController = cameraInputController ?? throw new ArgumentNullException(nameof(cameraInputController));

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

			//If not right-clicking then we shouldn't rotate the camera.
			if (!CameraInputController.isCameraControllerRunning)
				return;

			//float mouseX = Input.GetAxis("Mouse X");
			//float mouseY = Input.GetAxis("Mouse Y");

			float mouseX = CameraInputController.CurrentHorizontal;
			float mouseY = CameraInputController.CurrentVertical;

			//We can skip this if the inputs are 0.
			if(mouseY == mouseX && mouseY == 0.0f)
				return;

			CameraInputData _cameraInputData = Data.Value;

			float rotationalMovement = mouseX * CameraInputController.LookSpeed;

			//TODO: Kinda slow
			_cameraInputData.CurrentRotation = new Vector3(Mathf.Clamp(-mouseY * CameraInputController.LookSpeed + _cameraInputData.CurrentRotation.x, -_cameraInputData.MaxLookAngle.x, _cameraInputData.MaxLookAngle.x), 0, 0);

			_cameraInputData.CameraGameObject.transform.localEulerAngles = _cameraInputData.CurrentRotation;
			_cameraInputData.RootRotationalObject.transform.Rotate(Vector3.up, rotationalMovement);

			OnCameraInputChange?.Invoke(this, new CameraInputChangedEventArgs(_cameraInputData.RootRotationalObject.transform.eulerAngles.y));
		}
	}
}
