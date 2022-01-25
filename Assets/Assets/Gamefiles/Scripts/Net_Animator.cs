using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using HexabodyVR.PlayerController;
using HurricaneVR.Framework.ControllerInput;
using HurricaneVR.Framework.Core.Player;

public class Net_Animator : MonoBehaviour
{
    protected GameObject currentCollidingBall;
    protected GameObject previousCollidingBall;
    public Cloth netcloth;
    protected Rigidbody currentBallRigi;
    protected Rigidbody previousballRigi;
    protected Vector3 velocity;
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.GetComponent<SphereCollider>())
        {
            currentCollidingBall = collider.gameObject;
            var colliders = new ClothSphereColliderPair[1];
            colliders[0] = new ClothSphereColliderPair(currentCollidingBall.GetComponent<SphereCollider>());
            netcloth.sphereColliders = colliders;
            //currentBallRigi = currentCollidingBall.gameObject.GetComponent<Rigidbody>();
            //currentBallRigi.drag = 0.2f;
        }
    }

    private void OnTriggerExit(Collider collider)
    {
        previousCollidingBall = collider.gameObject;
        previousballRigi = previousCollidingBall.gameObject.GetComponent<Rigidbody>();
        //previousballRigi.drag = 0.4f;
    }

}


