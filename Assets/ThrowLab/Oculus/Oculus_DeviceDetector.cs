using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CloudFine.ThrowLab.Oculus
{
    public class Oculus_DeviceDetector : DeviceDetector
    {
        private void Awake()
        {
            OnControllerTypeDetermined(Device.OCULUS_TOUCH);
        }
    }
}
