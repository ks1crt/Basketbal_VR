using System.Collections;
using System.Collections.Generic;
using HexabodyVR.PlayerController;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;
using UnityEngine;


#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
#endif

public class HVRHexaBodyInputs : HexaBodyInputsBase
{
    [Tooltip("X axis must be lower than this to stand / crouch")]
    public float CrouchXThreshold = .3f;

    [Tooltip("Y axis must be higher than this to stand / crouch")]
    public float CrouchYThreshold = .15f;

    [Tooltip("Enable to use the left primary button (default unless you change code) to calibrate the height")]
    public bool EnableDebugCalibrationButton;

    [Header("Debugging")]
    public bool KeyboardDebug;
    public float KeyboardCrouchRate = 1f;

#if ENABLE_LEGACY_INPUT_MANAGER

    public KeyCode CrouchKey = KeyCode.X;
    public KeyCode StandKey = KeyCode.Z;
    public KeyCode JumpKey = KeyCode.Space;
    public KeyCode RunKey = KeyCode.LeftShift;
    public KeyCode RecalibrateKey = KeyCode.R;

#elif ENABLE_INPUT_SYSTEM

        public Key CrouchingKey = Key.X;
        public Key StandingKey = Key.Z;
        public Key JumpingKey = Key.Space;
        public Key RunningKey = Key.LeftShift;
        public Key HeightCalibrateKey = Key.R;

#endif

    public HVRController RightController => HVRInputManager.Instance.RightController;
    public HVRController LeftController => HVRInputManager.Instance.LeftController;

    public HVRControllerType RightControllerType => RightController.ControllerType;
    public HVRControllerType LeftControllerType => LeftController.ControllerType;


    protected override float UpdateCrouchRate()
    {

#if ENABLE_LEGACY_INPUT_MANAGER

        if (KeyboardDebug && Input.GetKey(CrouchKey))
        {
            return KeyboardCrouchRate;
        }
        else if (KeyboardDebug && Input.GetKey(StandKey))
        {
            return -KeyboardCrouchRate;
        }

#elif ENABLE_INPUT_SYSTEM

            if (KeyboardDebug && Keyboard.current[CrouchingKey].isPressed)
            {
                return KeyboardCrouchRate;
            }
            else if (KeyboardDebug && Keyboard.current[StandingKey].isPressed)
            {
                return -KeyboardCrouchRate;
            }

#endif

        float yAxis;
        float xAxis;

        if (RightController.ControllerType == HVRControllerType.Vive)
        {
            yAxis = RightController.TrackpadAxis.y;
            xAxis = RightController.TrackpadAxis.x;
            if (!RightController.TrackpadButtonState.Active)
            {
                return 0f;
            }
        }
        else
        {
            yAxis = RightController.JoystickAxis.y;
            xAxis = RightController.JoystickAxis.x;
        }

        if (Mathf.Abs(yAxis) > CrouchYThreshold && Mathf.Abs(xAxis) < CrouchXThreshold)
        {
            return -yAxis;
        }

        return 0f;
    }

    protected override bool UpdateJump()
    {
#if ENABLE_LEGACY_INPUT_MANAGER

        if (KeyboardDebug && Input.GetKey(JumpKey))
            return true;
#elif ENABLE_INPUT_SYSTEM
            if (KeyboardDebug && Keyboard.current[JumpingKey].isPressed)
                return true;
#endif

        if (RightControllerType == HVRControllerType.Vive)
        {
            return RightController.MenuButtonState.Active;
        }
        else if (RightControllerType == HVRControllerType.WMR)
        {
            return RightController.TrackPadDown.Active;
        }
        else
        {
            return RightController.PrimaryButtonState.Active;
        }
    }

    private Vector2 CheckWASD()
    {
        var x = 0f;
        var y = 0f;

#if ENABLE_LEGACY_INPUT_MANAGER
        if (Input.GetKey(KeyCode.W))
            y += 1f;
        if (Input.GetKey(KeyCode.S))
            y -= 1f;
        if (Input.GetKey(KeyCode.A))
            x += -1f;
        if (Input.GetKey(KeyCode.D))
            x += 1f;
#elif ENABLE_INPUT_SYSTEM
        if (Keyboard.current[Key.W].isPressed)
            y += 1f;
        if (Keyboard.current[Key.S].isPressed)
            y -= 1f;
        if (Keyboard.current[Key.A].isPressed)
            x += -1f;
        if (Keyboard.current[Key.D].isPressed)
            x += 1f;
#endif

        return new Vector2(x, y);
    }

    protected override Vector2 UpdateMovementAxis()
    {
        if (KeyboardDebug)
        {
            var val = CheckWASD();
            if (val.sqrMagnitude > 0)
                return val;
        }

        if (LeftController.ControllerType == HVRControllerType.Vive)
        {
            return LeftController.TrackpadButtonState.Active ? LeftController.TrackpadAxis : Vector2.zero;
        }

        return LeftController.JoystickAxis;
    }

    protected override Vector2 UpdateTurnAxis()
    {
        if (RightController.ControllerType == HVRControllerType.Vive)
        {
            return RightController.TrackpadButtonState.Active ? RightController.TrackpadAxis : Vector2.zero;
        }
        else
        {
            return RightController.JoystickAxis;
        }
    }

    protected override bool UpdateSprinting()
    {
#if ENABLE_LEGACY_INPUT_MANAGER
        if (KeyboardDebug && Input.GetKeyDown(KeyCode.LeftShift))
        {
            return true;
        }
#endif

#if ENABLE_INPUT_SYSTEM
        if (KeyboardDebug && Keyboard.current[Key.LeftShift].wasPressedThisFrame)
        {
            return true;
        }
#endif

        if (LeftController.ControllerType == HVRControllerType.Vive)
        {
            return LeftController.TrackpadButtonState.JustActivated;
        }

        if (RightController.ControllerType == HVRControllerType.WMR)
        {
            return RightController.TrackPadRight.JustActivated;
        }

        return LeftController.JoystickButtonState.JustActivated;
    }

    protected override bool UpdateRecalibrate()
    {
        if (!EnableDebugCalibrationButton)
            return false;

#if ENABLE_LEGACY_INPUT_MANAGER
        if (KeyboardDebug && Input.GetKeyDown(RecalibrateKey))
        {
            return true;
        }
#elif ENABLE_INPUT_SYSTEM
            if (KeyboardDebug && Keyboard.current[HeightCalibrateKey].wasPressedThisFrame)
                return true;
#endif

        if (LeftController.ControllerType == HVRControllerType.WMR)
        {
            return LeftController.MenuButtonState.JustActivated;
        }

        return LeftController.PrimaryButtonState.JustActivated;
    }
}
