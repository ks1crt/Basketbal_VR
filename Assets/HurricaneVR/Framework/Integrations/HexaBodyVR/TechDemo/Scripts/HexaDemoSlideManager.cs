using System.Collections;
using System.Collections.Generic;
using HexabodyVR.PlayerController;
using UnityEngine;

public class HexaDemoSlideManager : MonoBehaviour
{
    public bool EnableSlippery;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter(Collider other)
    {
        if (other.transform.parent &&  other.transform.parent.TryGetComponent(out HexaBodyPlayer3 player))
        {
            if (EnableSlippery)
            {
                player.EnableSlippery();
            }
            else
            {
                player.DisableSlippery();
            }
        }
    }
}
