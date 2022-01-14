using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexaVRIKUpdater : MonoBehaviour
{
    public SphereCollider Locosphere;

    void Update()
    {
        transform.position = Locosphere.transform.position + new Vector3(0, -Locosphere.radius, 0);
    }
}
