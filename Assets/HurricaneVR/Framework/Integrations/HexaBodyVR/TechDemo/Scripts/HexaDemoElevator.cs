using System.Collections;
using System.Collections.Generic;
using HurricaneVR.Framework.Core.Utils;
using HurricaneVR.Framework.Shared;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class HexaDemoElevator : MonoBehaviour
{
    [Header("Settings")]
    
    public HexaElevatorState State;
    public float MaxSpeed = 2f;
    public float TimeToSpeed = .5f;
    public float SnapThreshold = .001f;
    
    [Header("Debug")]
    public float Speed;
    public HexaElevatorState PreviousState;
    public Vector3 LowerPosition;
    public Vector3 UpperPosition;
    
    public Rigidbody Rigidbody { get; private set; }

    public Vector3 Target
    {
        get
        {
            if (State == HexaElevatorState.GoingUp)
            {
                return UpperPosition;
            }

            return LowerPosition;
        }
    }

#if UNITY_EDITOR

    [InspectorButton("SetLowerPositionPrivate")]
    public string SetLowerPosition;

    [InspectorButton("SetUpperPositionPrivate")]
    public string SetUpperPosition;

    private void SetLowerPositionPrivate()
    {
        LowerPosition = transform.position;
    }
    private void SetUpperPositionPrivate()
    {
        UpperPosition = transform.position;
    }

    [InspectorButton("GoLowerPositionPrivate")]
    public string GoLowerPosition;

    [InspectorButton("GoUpperPositionPrivate")]
    public string GoUpperPosition;

    private void GoLowerPositionPrivate()
    {
        transform.position = LowerPosition;
    }
    private void GoUpperPositionPrivate()
    {
        transform.position = UpperPosition;
    }



#endif

    void Start()
    {
        Rigidbody = this.GetRigidbody();
    }

    void FixedUpdate()
    {
        if (State != HexaElevatorState.Stationary)
        {
            var sign = 1f;
            if (State == HexaElevatorState.GoingDown)
                sign = -1f;
            var accel = MaxSpeed / TimeToSpeed * Time.deltaTime * sign;
            var distanceToStop = Speed * Speed / (2 * MaxSpeed / TimeToSpeed);

            if (Vector3.Distance(transform.position, Target) < distanceToStop)
            {
                Speed -= accel;
                if (State == HexaElevatorState.GoingUp)
                {
                    Speed = Mathf.Clamp(Speed, 0f, MaxSpeed);
                }
                else
                {
                    Speed = Mathf.Clamp(Speed, -MaxSpeed, 0);
                }
            }
            else
            {
                Speed += accel;
                Speed = Mathf.Clamp(Speed, -MaxSpeed, MaxSpeed);
            }

            Rigidbody.MovePosition(transform.position + Speed * Vector3.up * Time.deltaTime);
            var distance = Mathf.Abs(transform.position.y - Target.y);
            if (distance < SnapThreshold || State == HexaElevatorState.GoingUp && transform.position.y > Target.y || State == HexaElevatorState.GoingDown && transform.position.y < Target.y)
            {
                transform.position = Target;
                State = HexaElevatorState.Stationary;
            }
        }

        PreviousState = State;
    }

    public void ToggleUp()
    {
        State = HexaElevatorState.GoingUp;
    }

    public void ToggleDown()
    {
        State = HexaElevatorState.GoingDown;
    }

    public void Toggle()
    {
        if (State == HexaElevatorState.GoingUp)
        {
            State = HexaElevatorState.GoingDown;
        }
        else if (State == HexaElevatorState.GoingDown)
        {
            State = HexaElevatorState.GoingUp;
        }
        else
        {
            var distanceToTop = Vector3.Distance(transform.position, UpperPosition);
            var distanceToBottom = Vector3.Distance(transform.position, LowerPosition);
            if (distanceToTop < distanceToBottom)
            {
                State = HexaElevatorState.GoingDown;
            }
            else
            {
                State = HexaElevatorState.GoingUp;
            }
        }

    }

    public enum HexaElevatorState
    {
        Stationary,
        GoingUp,
        GoingDown
    }
}
