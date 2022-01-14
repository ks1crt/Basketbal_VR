using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CloudFine.ThrowLab.UnityXR
{
    [RequireComponent(typeof(XRController))]
    public class UnityXR_GrabThresholdModifier : GrabThresholdModifier
    {
        float val = 0;
        private XRController _controller;

        private void Awake()
        {
            _controller = GetComponent<XRController>();
        }

        public override float GripValue()
        {
            switch (_controller.selectUsage)
            {
                case InputHelpers.Button.Grip:
                    _controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.grip, out val);
                    break;
                case InputHelpers.Button.Trigger:
                    _controller.inputDevice.TryGetFeatureValue(UnityEngine.XR.CommonUsages.trigger, out val);
                    break;
                default:
                    Debug.LogWarning("XRController SelectUsage " + _controller.selectUsage.ToString() + " not supported.", this);
                    break;
            }
            return val;
        }

        public override void SetGrabThreshold(float grip)
        {
            _controller.axisToPressThreshold = grip;
        }

        public override void SetReleaseThreshold(float grip)
        {
            _controller.axisToPressThreshold = grip;
        }
    }
}