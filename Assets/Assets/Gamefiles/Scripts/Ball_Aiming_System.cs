using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using Assets.HurricaneVR.Framework.Shared.Utilities;
using HurricaneVR.Framework.Components;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Bags;
using HurricaneVR.Framework.Core.HandPoser;
using HurricaneVR.Framework.Core.HandPoser.Data;
using HurricaneVR.Framework.Core.Player;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Shared.Utilities;
using UnityEngine.Events;

namespace VR.Basketball.Core
{
    [RequireComponent(typeof(HVRGrabbable))]
    public class Ball_Aiming_System : MonoBehaviour
    {
        protected Transform ballOriginTrasform;
        public HVRPlayerController Player;
        protected HVRGrabbable Grabbable { get; private set; }
        public Aiming_Curve Aimingcurve { get; set; }
        protected GameObject currentBall;
        protected Rigidbody ballRigi;
        public AimingPositionUpdate BeforeTeleport = new AimingPositionUpdate();
        public UnityEvent AfterObjectRelease = new UnityEvent();                //?
        public AimingPositionUpdate PositionUpdate = new AimingPositionUpdate();
        private bool ballGrabed;

        protected Collider HitCollider { get; set; }
        protected Collider DownHitCollider { get; set; }
        protected Vector3[] LineRendererPoints { get; set; }
        public Vector3 HitPosition { get; protected set; }
        public Vector3 LastValidPoint => LineRendererPoints[LastValidIndex];
        public Vector3 LastPoint => LineRendererPoints[LastIndex];
        public Vector3 LastDownwardPoint { get; set; }
        public Vector3 LastValidDownwardPoint { get; set; }
        public int LastValidIndex { get; protected set; }
        public int LastIndex { get; protected set; }
        public Transform ballCenterOfMass => Grabbable.transform.parent;
        public bool IsRaycastValid { get; set; }
        public Vector3 SurfaceNormal { get; protected set; }

        public Vector3 Origin => ballCenterOfMass.position;
        public Vector3 Forward => ballCenterOfMass.forward;
        public bool CanAim { get; protected set; }
        public bool IsAimValid { get; protected set; } // it will be valid if its aim othe on the board or the hoop
        public bool IsPreviousAimValid { get; protected set; }

        public bool IsAiming { get; protected set; } //  it will be true if the ball is upon the head or something
        protected float activeCurveTime;
       
        private void Awake()
        {
            Grabbable = GetComponent<HVRGrabbable>();
            Aimingcurve = GetComponent<Aiming_Curve>();
            CanAim = false;
            IsAiming = false;
            IsAimValid = false;

        }

        void Start()
        {
            Grabbable.Grabbed.AddListener(OnObjectGrabbed);
            Grabbable.Released.AddListener(OnObjectReleased);
        }
        // Update is called once per frame
        protected void Update()
        {
           if (ballGrabed) //ball is still grabbed
           {
                CheckBAllPosition();
                if (IsAiming) // create function that must first check if the  if the ball is above the head 
                {
                    if (CheckAim()) // check if raycast collides either the board or the hoop
                    {
                        if (!ballGrabed && IsPreviousAimValid)
                        {
                            // set the course of the ball according to last valid raycast raycast
                        }
                    }
                      
                    EnableCheck();
                
                    IsPreviousAimValid = IsAimValid;
                    BeforeRaycast();
                    Raycast();
                    AfterRaycast();
                    //if (HandPrevents)
                    //{
                    //    IsTeleportValid = false;
                    //}
                
                    //CheckValidTeleportChanged(IsTeleportPreviouslyValid);
                    //HandleValidStatus(IsTeleportValid);
                    //UpdateTeleportMarker(IsTeleportValid);
                }

                    //CheckPlayerRotation();

                    //PreviousAiming = IsAiming;
            }
        }

        private void CheckBAllPosition()
        {
            // check if the ball world Y position is greater than the hmd y position and make IsAiming= true;
            throw new NotImplementedException();
        }

        private void AfterRaycast()
        {
            throw new NotImplementedException();
        }

        private void Raycast()
        {
            throw new NotImplementedException();
        }

        private void BeforeRaycast()
        {
            throw new NotImplementedException();
        }

        private void EnableCheck()
        {
            throw new NotImplementedException();
        }

        private bool CheckAim()
        {
            throw new NotImplementedException();
            //if (!CheckCanAim())
            //{
            //    return;
            //}

            //if (IsAimingActivated())
            //{
            //    OnAimingActivated();
            //}
            //else if (IsAimingDeactivated())
            //{
            //    OnAimingDeactivated();
            //}
        }

        private void OnAimingDeactivated()
        {
            throw new NotImplementedException();
        }

        private bool IsAimingDeactivated()
        {
            throw new NotImplementedException();
        }

        private void OnAimingActivated()
        {
            throw new NotImplementedException();
        }

        private bool IsAimingActivated()
        {
            throw new NotImplementedException();
        }

        private bool CheckCanAim()
        {
            throw new NotImplementedException();
        }

        public void OnObjectGrabbed (HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
                ballGrabed = true;
                //hvrGrabbable.transform.localPosition.y >
                hvrGrabbable.Rigidbody.drag = 50;
                //hvrGrabbable.Rigidbody.drag = 50;
                //curve.OnActivated(ballGrabded);
        }

        public void OnObjectReleased(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            ballGrabed = false;
            CanAim = false;
            IsAiming = false;
            IsAimValid = false;
        }

        public void Enable()
        {
            IsAiming = true;
            AimingCurveVisibility(IsAiming);
        }

        public void Disable()
        {
            IsAiming = false;
            AimingCurveVisibility(IsAiming);
        }

        private void AimingCurveVisibility(bool isAiming)
        {
            if (isAiming && IsAimValid && Aimingcurve)
            {
                Aimingcurve.Activate();
            }
            else
            {
                Aimingcurve.Deactivate();
            }
        }
        // WHEN THE RAYCASST COLLIDES WITH THE COLLIDER OIN THE BOARD OR ON THE HOOP , I MUST CHECK THE RAYCAST COLLISION NOT THE BALL COLLISIUON
        public void OnCollisionEnter(Collision collision)
        {
            if (CanAim)
            {
                IsAiming = true;
            }
        }





    }

    public enum AimingType
    {
        Ballistic, StraightLine
    }

    public enum AimingState
    {
        Awaiting,
        HoopAiming,
        GlassAiming,
        
    }
    public class AimingPositionUpdate : UnityEvent<Vector3>
    {

    }
}

