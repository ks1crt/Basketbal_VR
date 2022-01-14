using System.Collections;
using System.Collections.Generic;
using HexabodyVR.PlayerController;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Shared;

public class HVRHexaBodyInputWrapper : HVRPlayerInputs
{
    public float CrouchThreshold = .7f;
    public float StandThreshold = .7f;

    public HexaBodyPlayerInputs PlayerInputs;

    public bool EnableDebugCalibrationButton;

    private bool _isCrouching;

    protected override void AfterInputUpdate()
    {
        if (EnableDebugCalibrationButton)
        {
            PlayerInputs.RecalibratePressed = LeftController.PrimaryButtonState.JustActivated;

            if (LeftControllerType == HVRControllerType.WMR || LeftControllerType == HVRControllerType.Vive)
            {
                PlayerInputs.RecalibratePressed = LeftController.MenuButtonState.JustActivated;
            }
        }

        PlayerInputs.SprintRequiresDoubleClick = SprintRequiresDoubleClick;
        PlayerInputs.SprintingPressed = IsSprintingActivated;

        PlayerInputs.MovementAxis = MovementAxis;
        PlayerInputs.TurnAxis = TurnAxis;

        PlayerInputs.CrouchPressed = IsCrouchActivated;
        PlayerInputs.StandPressed = IsStandActivated;
        PlayerInputs.JumpPressed = IsJumpActivated;
    }

    protected override bool GetCrouch()
    {
        if (RightControllerType == HVRControllerType.Vive)
        {
            return RightController.TrackpadButtonState.Active && RightController.TrackpadAxis.y < -CrouchThreshold;
        }
        return RightController.JoystickAxis.y < -CrouchThreshold;
    }

    protected override bool GetStand()
    {
        if (RightControllerType == HVRControllerType.Vive)
        {
            return RightController.TrackpadButtonState.Active && RightController.TrackpadAxis.y > StandThreshold;
        }
        return RightController.JoystickAxis.y > StandThreshold;
    }

    protected override bool GetIsJumpActivated()
    {
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
}