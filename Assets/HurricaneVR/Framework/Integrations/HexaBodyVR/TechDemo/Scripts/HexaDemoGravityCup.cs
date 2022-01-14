using HurricaneVR.Framework.Core;
using HurricaneVR.Framework.Core.Grabbers;
using HurricaneVR.Framework.Core.ScriptableObjects;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

[RequireComponent(typeof(HVRGrabbable))]
public class HexaDemoGravityCup : MonoBehaviour
{
    public Transform RayOrigin;
    public LayerMask IgnoreLayerMask;
    public float RayDistance = .2f;

    public bool Activated;
    private ConfigurableJoint _joint;
    public HVRGrabbable Grabbable { get; private set; }

    void Start()
    {
        Grabbable = GetComponent<HVRGrabbable>();
        Grabbable.HandReleased.AddListener(OnReleased);
    }

    private void OnReleased(HVRHandGrabber arg0, HVRGrabbable arg1)
    {
        Activated = false;
        if (_joint)
        {
            Destroy(_joint);
        }
    }


    void Update()
    {
        if (Grabbable.HandGrabbers.Count > 0)
        {
            if (Grabbable.HandGrabbers[0].Controller.Trigger > .7f && !Activated)
            {
                Activate();
            }
            else if (Grabbable.HandGrabbers[0].Controller.Trigger < .4f && Activated)
            {
                Deactivate();
            }
        }
    }


    public void Activate()
    {
        if (Physics.Raycast(RayOrigin.position, RayOrigin.forward, out var hit, RayDistance, ~IgnoreLayerMask, QueryTriggerInteraction.Ignore))
        {
            transform.rotation = Quaternion.FromToRotation(RayOrigin.forward, -hit.normal) * transform.rotation;
            Activated = true;
            _joint = gameObject.AddComponent<ConfigurableJoint>();
            _joint.anchor = transform.InverseTransformPoint(RayOrigin.position);
            _joint.autoConfigureConnectedAnchor = false;
            if (hit.collider.attachedRigidbody)
            {
                _joint.connectedBody = hit.collider.attachedRigidbody;
                _joint.connectedAnchor = hit.collider.attachedRigidbody.transform.InverseTransformPoint(hit.point);
            }
            else
            {
                _joint.connectedAnchor = hit.point;
            }

            _joint.rotationDriveMode = RotationDriveMode.Slerp;
            _joint.SetSlerpDrive(100000f, 5, 10000f);
            _joint.LockLinearMotion();
        }
    }

    public void Deactivate()
    {
        Activated = false;
        if (_joint)
        {
            Destroy(_joint);
        }
    }
}
