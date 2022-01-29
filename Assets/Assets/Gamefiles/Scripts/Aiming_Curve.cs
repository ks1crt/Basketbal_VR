using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HurricaneVR.Framework.Core.Player;

namespace VR.Basketball.Core
{
    public class Aiming_Curve : MonoBehaviour
    {
       // aiming components
        public GameObject ring;
        public LineRenderer firstAimingLine;
        public GameObject firstAimingArrow;
        public LineRenderer boardReflectedAimLine;
        public GameObject boardReflectedArrow;
        protected Transform ballGoalTrasform;
        // AIMING materials
        private Material ringMaterial;
        private Material ArrowMaterial;
        private Material BoardCollisionMaterial;

        //Visual
        public bool UseValidAimColors = true;
        public Color ValidColor;
        public Color InvalidColor;

        //varuables
        protected bool IsAimValid = false;


        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
        public void OnActivated(GameObject grabbedBall)
        {
            throw new System.NotImplementedException();
        }

        public void OnDeactivated(GameObject releasedBall)
        {
          var rigi =  releasedBall.GetComponent<Rigidbody>();
            rigi.drag = 1;
        }
    }
}
