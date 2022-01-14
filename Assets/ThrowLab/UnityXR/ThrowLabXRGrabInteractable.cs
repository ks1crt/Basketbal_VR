using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

namespace CloudFine.ThrowLab.UnityXR
{
    [RequireComponent(typeof(ThrowHandle))]
    public class ThrowLabXRGrabInteractable : XRGrabInteractable
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

        protected override void OnSelectEntered(SelectEnterEventArgs args)
        {
            var interactor = args.interactor;
            if (!interactor)
                return;
            base.OnSelectEntered(args);
            throwOnDetach = false;
            _handle.OnAttach(interactor.gameObject, interactor.gameObject);
        }

        protected override void OnSelectExiting(SelectExitEventArgs args)
        {
            base.OnSelectExiting(args);
            _handle.OnDetach();
           
        }



    }
}
