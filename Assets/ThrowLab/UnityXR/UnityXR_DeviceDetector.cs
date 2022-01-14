using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CloudFine.ThrowLab.UnityXR
{
    [RequireComponent(typeof(XRController))]
    public class UnityXR_DeviceDetector : DeviceDetector
    {
        private XRController _controller;
        private Device _device = Device.UNSPECIFIED;
        private bool _deviceLoaded = false;


        private void Awake()
        {
            _controller = GetComponent<XRController>();
        }

        private void Update()
        {
            if (_deviceLoaded) return;
            if (_controller.inputDevice == null) return;
            if (_controller.inputDevice.name == null) return;
        
            string deviceString = _controller.inputDevice.name;
            if (deviceString.IndexOf("vive", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _device = Device.VIVE;
            }
            if (deviceString.IndexOf("knuckles", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _device = Device.KNUCKLES;
            }
            if (deviceString.IndexOf("oculus", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _device = Device.OCULUS_TOUCH;
            }
            if (deviceString.IndexOf("windows", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                _device = Device.WINDOWS_MR;
            }

            OnControllerTypeDetermined(_device);
            _deviceLoaded = true;
        }
       
    }
}
