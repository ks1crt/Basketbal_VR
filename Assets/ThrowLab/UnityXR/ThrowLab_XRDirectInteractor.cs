using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;

namespace CloudFine.ThrowLab.UnityXR
{

    public class ThrowLab_XRDirectInteractor : XRDirectInteractor
    {
        
        /// <summary>
        /// Retrieve the list of interactables that this interactor could possibly interact with this frame.
        /// This list is sorted by priority (in this case distance).
        /// </summary>
        /// <param name="validTargets">Populated List of interactables that are valid for selection or hover.</param>
        public override void GetValidTargets(List<XRBaseInteractable> validTargets)
        {
            this.validTargets.RemoveAll(x => x == null);
            base.GetValidTargets(validTargets);
        }
    }
}