using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;
using Autofac;
using Glader.Essentials;
using GladMMO.Services.Input;
using UnityEngine;

namespace GladMMO
{
	public sealed class InputDependencyAutofacModule : Module
	{
		protected override void Load(ContainerBuilder builder)
		{
			//If mobile we need to find the joystick
			if (Application.isMobilePlatform)
				RegisterMobileInput(builder);
			else
				RegisterDesktopInput(builder);

			//ThreadSafeActionBarCollection : IActionBarCollection
			builder.RegisterType<ThreadSafeActionBarCollection>()
				.AsImplementedInterfaces()
				.SingleInstance();
		}

		private void RegisterDesktopInput([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			builder.RegisterType<DesktopInputMovementInputController>()
				.As<IMovementInputController>()
				.SingleInstance();

			//DesktopInputCameraInputController : ICameraInputController
			builder.RegisterType<DesktopInputCameraInputController>()
				.As<ICameraInputController>();
		}

		private static void RegisterMobileInput([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			MobileJoystickInputController inputController = GameObject.FindObjectOfType<MobileJoystickInputController>();

			builder.RegisterInstance(inputController)
				.As<MobileJoystickInputController>();

			builder.RegisterType<MobileInputMovementInputController>()
				.As<IMovementInputController>()
				.SingleInstance();

			//Don't want one ticking per dependency, so singletone is a MUST here.
			builder.RegisterType<MobileInputCameraInputController>()
				.As<ICameraInputController>()
				.As<IGameTickable>()
				.SingleInstance();
		}
	}
}
