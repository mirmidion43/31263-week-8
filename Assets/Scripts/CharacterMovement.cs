using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    // Update is called once per frame
    void Update()
    {
        GetMovementInput();
        CharacterPosition();
        CharacterRotation();
        WalkingAnimation();
        FootstepAudio();    
    }

    private void GetMovementInput()
    {
        movement.x = Input.GetAxis("Horizontal");
        movement.z = Input.GetAxis("Vertical");
        movementSqrMagnitude = movement.sqrMagnitude;
        Debug.Log(movement);
    }

    private void CharacterPosition()
    {

    }
    
    private void CharacterRotation()
    {

    }

    private void WalkingAnimation()
    {

    }
    private void FootstepAudio()
    {

    }
}
