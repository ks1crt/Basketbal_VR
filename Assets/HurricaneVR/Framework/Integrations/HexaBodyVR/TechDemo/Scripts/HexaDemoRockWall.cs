using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

public class HexaDemoRockWall : MonoBehaviour
{

    public Vector3 TargetPosition;
    public bool Show;
    public AudioClip SFX;
    public float SlideInTime = 8f;
    private float _speed;

#if UNITY_EDITOR

    [InspectorButton("SetTargetPositionPrivate")]
    public string SetTargetPosition;

    
    private void SetTargetPositionPrivate()
    {
        TargetPosition = transform.localPosition;
    }

#endif
    void Start()
    {
        _speed = Vector3.Distance(TargetPosition, transform.localPosition) / SlideInTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Show)
        {

            transform.localPosition = Vector3.MoveTowards(transform.localPosition, TargetPosition, _speed * Time.deltaTime);
            if (Vector3.Distance(transform.localPosition, TargetPosition) < .001f)
            {
                Show = false;
            }
        }
    }

    public void Move()
    {
        Show = true;
        if(SFXPlayer.Instance) SFXPlayer.Instance.PlaySFX(SFX, transform.position);
    }
}
