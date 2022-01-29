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
        protected GameObject ballGrabded;
        protected HVRGrabbable Grabbable { get; private set; }
        protected GameObject currentBall;
        protected Rigidbody ballRigi;
        public Aiming_Curve curve { get; set; }
        // Start is called before the first frame update
        void Start()
        {
            Grabbable = GetComponent<HVRGrabbable>();
            Grabbable.Grabbed.AddListener(getGrabedObject);
            Grabbable.Released.AddListener(OnReleased);
            
        }
        // Update is called once per frame
        void Update()
        {
           
        }

        public void OnCollisionEnter(Collision cother)
        {
            
            
        }

        protected void OnCollisionExit(Collision collision)
        {
            
        }
        public void getGrabedObject (HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {
            //if (hvrGrabbable.gameObject.tag == "Ball")
            //{

            ballGrabded = hvrGrabbable.gameObject;
            ballRigi = ballGrabded.GetRigidbody();
            ballRigi.drag = 50f;
            //hvrGrabbable.Rigidbody.drag = 50;
                //curve.OnActivated(ballGrabded);
            //}
            
        }

        public void OnReleased(HVRGrabberBase grabber, HVRGrabbable hvrGrabbable)
        {

            //if (hvrGrabbable.gameObject.tag == "Ball" && grabed == true)
            //{
                hvrGrabbable.Rigidbody.drag = 10;
                //ballRigi.drag = 10f;
                //curve.OnDeactivated(currentBall);
            //}

        }
    }
}
