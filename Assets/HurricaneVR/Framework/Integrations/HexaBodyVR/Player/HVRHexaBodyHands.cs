using System;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HexabodyVR.PlayerController;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine;
using UnityEngine.Serialization;

namespace HurricaneVR.Framework.Core.Player
{

    [RequireComponent(typeof(HVRHandStrengthHandler))]
    public class HVRHexaBodyHands : HexaHandsBase
    {
        [Header("Settings")]
        public HVRJointSettings JointSettings;

        public bool GrabbingStopsUnstuck = true;

        [Header("Required Components")]

        public HVRHandGrabber Grabber;
        //public HVRHandGrabber OtherGrabber;
        
        public HVRHandStrengthHandler StrengthHandler;

        [Header("Strength Overrides")]

        [FormerlySerializedAs("ClimbStrength")]

        [Tooltip("Strength when one hand is holding a grabbable object with the HVRClimbable component on it.")]
        public HVRJointSettings OneHandClimbStrength;

        [Tooltip("Strength when both hands are holding grabbable objects with the HVRClimbable components on them.")]
        public HVRJointSettings TwoHandClimbStrength;



        public override bool IsLeft => Grabber.HandSide == HVRHandSide.Left;


        protected override void Awake()
        {
            base.Awake();

            if (!StrengthHandler)
            {
                if (!TryGetComponent(out StrengthHandler))
                {
                    StrengthHandler = gameObject.AddComponent<HVRHandStrengthHandler>();
                }
            }

            StrengthHandler.Joint = Joint;
            StrengthHandler.Initialize(JointSettings);

            if (!Grabber)
            {
                TryGetComponent(out Grabber);
            }

            if (Grabber)
            {
                Grabber.Grabbed.AddListener(OnGrabbed);
                Grabber.Released.AddListener(OnReleased);

                //if (!OtherGrabber)
                //    OtherGrabber = transform.root.GetComponentsInChildren<HVRHandGrabber>().FirstOrDefault(e => e.HandSide != Grabber.HandSide);

                //if (!OtherHand)
                //{
                //    if (OtherGrabber)
                //    {
                //        OtherGrabber.gameObject.TryGetComponent(out OtherHand);
                //    }
                //}
            }
            else
            {
                Debug.LogWarning($"{name}'s Grabber is not assigned.");
            }

            //if (!OtherGrabber)
            //{
            //    Debug.LogWarning($"{name}'s OtherGrabber is not assigned.");
            //}
        }
        
        protected virtual void OnReleased(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            SetHandState(HandGrabState.None);
        }

        protected virtual void OnGrabbed(HVRGrabberBase arg0, HVRGrabbable arg1)
        {
            UpdateHandState();
        }

        protected override HandGrabState GetHandState()
        {
            if (Grabber)
            {
                if (Grabber.IsClimbing && OneHandClimbStrength)
                    return HandGrabState.Climbing;

                if (Grabber.Joint)
                {
                    if (Grabber.Joint.connectedBody)
                    {
                        //grabber holds the joint component unless jointed to nothing
                        if (Grabber.Joint.connectedBody == RigidBody)
                        {
                            if (Grabber.GrabbedTarget.Rigidbody.isKinematic)
                            {
                                return HandGrabState.KinematicGrab;
                            }
                            else
                            {
                                return HandGrabState.DynamicGrab;
                            }
                        }
                    }
                    else
                    {
                        return HandGrabState.KinematicGrab;
                    }
                }
                else
                {
                    return HandGrabState.None;
                }
            }

            return HandGrabState.None;
        }

        protected override void UpdateTargetVelocity()
        {
            base.UpdateTargetVelocity();

            //shouldn't even be used...
            if (Joint.rotationDriveMode == RotationDriveMode.XYAndZ)
            {
                Joint.targetAngularVelocity *= -1;
            }
        }

        protected override void SetStrength(StrengthState state)
        {
            switch (state)
            {
                case StrengthState.Default:
                    StrengthHandler.StopOverride();
                    break;
                case StrengthState.OneHandClimbing:
                    StrengthHandler.OverrideSettings(OneHandClimbStrength);
                    break;
                case StrengthState.TwoHandClimbing:
                    StrengthHandler.OverrideSettings(TwoHandClimbStrength);
                    break;
            }
        }


        protected override bool CanUnstuck()
        {
            if (!GrabbingStopsUnstuck) return true;
            return !Grabber || !Grabber.IsGrabbing;
        }

        protected override void UpdateHandStrength()
        {
            if (HandState == HandGrabState.Climbing)
            {
                if (OtherHand && OtherHand.HandState == HandGrabState.Climbing)
                {
                    SetStrengthState(StrengthState.TwoHandClimbing);
                    return;
                }

                SetStrengthState(StrengthState.OneHandClimbing);
                return;
            }

            SetStrengthState(StrengthState.Default);
        }
    }
}