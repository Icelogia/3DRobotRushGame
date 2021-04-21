using System;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    [Header("Vehicle Objects")]
    [SerializeField] Transform frontWheels = null;
    [SerializeField] Transform middleWheels = null;
    [SerializeField] Transform backLeftWheel = null;
    [SerializeField] Transform backRightWheel = null;

    [Header("Movement")]
    [SerializeField] private PlayerInputControl inputControl = null;
    [SerializeField] private float rotationSpeed = 30f;
    [SerializeField] private float turnRotationAngle = 15f;

    private void Update()
    {
        WheelsMovement();
        WheelsTurn();
    }

    private void WheelsTurn()
    {
        if(inputControl.rotationMovement > 0.1f)
        {
            TurnWheelRotation(-turnRotationAngle);
        }
        else if(inputControl.rotationMovement < -0.1f)
        {
            TurnWheelRotation(turnRotationAngle);
        }
        else
        {
            TurnWheelRotation(0);
        }
    }

    private void TurnWheelRotation(float turnRotationAng)
    {
        turnRotationAng -= 90;
        backLeftWheel.localEulerAngles = new Vector3(0, turnRotationAng, backLeftWheel.localEulerAngles.z);
        backRightWheel.localEulerAngles = new Vector3(0, turnRotationAng, backRightWheel.localEulerAngles.z);
    }

    private void WheelsMovement()
    {
        if (inputControl.verticalMovement > 0.1f)
        {
            RotateWheels(-rotationSpeed);
        }
        else if (inputControl.verticalMovement < -0.1f)
        {
            RotateWheels(rotationSpeed);
        }
    }

    private void RotateWheels(float rotationSpd)
    {
        frontWheels.Rotate(0f, 0f, rotationSpd * Time.deltaTime);
        middleWheels.Rotate(0f, 0f, rotationSpd * Time.deltaTime);
        backLeftWheel.Rotate(0f, 0f, rotationSpd * Time.deltaTime);
        backRightWheel.Rotate(0f, 0f, rotationSpd * Time.deltaTime);
    }
}
