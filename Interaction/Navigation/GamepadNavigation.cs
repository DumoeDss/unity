using UnityEngine;

namespace UFZ.Interaction
{
	public class GamepadNavigation : NavigationBase
	{
		void Start ()
		{
			DeadZone = 0.15f;
		}

		protected override void GetInputs()
		{
#if UNITY_STANDALONE_WIN || UNITY_EDITOR_WIN
			if(MiddleVR.VRDeviceMgr == null)
				return;

			var joystick = MiddleVR.VRDeviceMgr.GetJoystick();
			if(joystick == null)
				return;

			Forward = -joystick.GetAxisValue(1);
			Sideward = joystick.GetAxisValue(0);
			HorizontalRotation = joystick.GetAxisValue(3);
			Running = Mathf.Abs(joystick.GetAxisValue(2));
#endif
		}
	}
}
