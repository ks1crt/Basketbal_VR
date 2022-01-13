using System.Collections.Generic;
using HurricaneVR.Framework.Core.Player;
using UnityEngine;

namespace HurricaneVR.Framework.Oculus
{
    public class HVROculusManager : MonoBehaviour
    {
        public MonoBehaviour[] ComponentsToDisable;
        public Dictionary<MonoBehaviour, Vector3> Stuff = new Dictionary<MonoBehaviour, Vector3>();

        public void OnEnable()
        {
            OVRManager.InputFocusAcquired += OnResume;
            OVRManager.InputFocusLost += OnPaused;
        }

     


        private void OnResume()
        {
            foreach (var c in ComponentsToDisable)
            {
                c.enabled = true;
                c.transform.position = Stuff[c];
            }
            Time.timeScale = 1f;
        }

        private void OnPaused()
        {
            foreach (var c in ComponentsToDisable)
            {
                var p  = c.transform.position;
                c.enabled = false;
                Stuff[c] = c.transform.position;
                c.transform.position = p;
            }

            //Time.timeScale = 0f;

        }

        public void OnDisable()
        {
            OVRManager.InputFocusAcquired -= OnResume;
            OVRManager.InputFocusLost -= OnPaused;
        }
    }
}