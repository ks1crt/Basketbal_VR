using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudFine.ThrowLab.Oculus
{
    [RequireComponent(typeof(OVRGrabber))]
    public class Oculus_GrabThresholdModifier : GrabThresholdModifier
    {
        private OVRGrabber _grabber;
        [SerializeField]
        private OVRInput.Controller m_controller = OVRInput.Controller.None;


        private void Awake()
        {
            _grabber = GetComponent<OVRGrabber>();
        }

        public override float GripValue()
        {
            return OVRInput.Get(OVRInput.Axis1D.PrimaryHandTrigger, m_controller); ;
        }

        public override void SetGrabThreshold(float grip)
        {
            _grabber.grabBegin = grip;
        }

        public override void SetReleaseThreshold(float grip)
        {
            _grabber.grabEnd = grip;
        }
    }
}
