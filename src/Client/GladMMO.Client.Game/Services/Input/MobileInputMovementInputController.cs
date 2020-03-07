using System; using FreecraftCore;
using System.Collections.Generic;
using System.Text;

namespace GladMMO
{
	public sealed class MobileInputMovementInputController : IMovementInputController
	{
		public float CurrentHorizontal => JoyStickController.JoystickAxis.x;

		public float CurrentVertical => JoyStickController.JoystickAxis.y;

		private MobileJoystickInputController JoyStickController { get; }

		public MobileInputMovementInputController([NotNull] MobileJoystickInputController joyStickController)
		{
			JoyStickController = joyStickController ?? throw new ArgumentNullException(nameof(joyStickController));
		}
	}
}
