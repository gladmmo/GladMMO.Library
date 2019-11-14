using System;
using System.Collections.Generic;
using System.Text;
using Autofac;
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
		}

		private void RegisterDesktopInput([NotNull] ContainerBuilder builder)
		{
			if (builder == null) throw new ArgumentNullException(nameof(builder));

			builder.RegisterType<DesktopInputMovementInputController>()
				.As<IMovementInputController>()
				.SingleInstance();
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
		}
	}
}
