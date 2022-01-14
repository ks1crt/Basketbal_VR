using UnityEngine;

namespace CloudFine.ThrowLab.Oculus
{
    [RequireComponent(typeof(ThrowHandle))]
    public class ThrowLabOVRGrabbable : OVRGrabbable
    {
        private ThrowHandle _handle
        {
            get
            {
                if (m_handle == null)
                {
                    m_handle = GetComponent<ThrowHandle>();
                    if (m_handle == null)
                    {
                        m_handle = gameObject.AddComponent<ThrowHandle>();
                    }
                }
                return m_handle;
            }
        }
        private ThrowHandle m_handle;


        public override void GrabBegin(OVRGrabber hand, Collider grabPoint)
        {
            base.GrabBegin(hand, grabPoint);
            _handle.OnAttach(hand.gameObject, hand.gameObject);
        }

        public override void GrabEnd(Vector3 linearVelocity, Vector3 angularVelocity)
        {
            base.GrabEnd(linearVelocity, angularVelocity);
            _handle.OnDetach();
        }
    }
}