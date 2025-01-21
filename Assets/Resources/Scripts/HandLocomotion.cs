using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public class HandLocomotion : MonoBehaviour
{
    public InputActionReference leftLocomotionMove;
    public InputActionReference rightLocomotionMove;
    protected float leftMove;
    protected float rightMove;
    [SerializeField]
    float moveSpeed;
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public void leftPoseMade()
    {
        leftMove = 1;
        
    }

    public void rightPoseMade()
    {
        rightMove = 1;
        print("pose made");
    }

    public void leftPoseRelease()
    {
        leftMove = 0;
    }

    public void rightPoseRelease()
    {
        rightMove = 0;
    }
}
