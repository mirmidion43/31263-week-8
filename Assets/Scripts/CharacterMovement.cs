using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.5f;
    public Animator animator;

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

        movement = Vector3.ClampMagnitude(movement, 1.0f);

        movementSqrMagnitude = movement.sqrMagnitude;
        //Debug.Log(movement);
    }

    private void CharacterPosition()
    {
        gameObject.transform.Translate(movement * walkSpeed * Time.deltaTime, Space.World);
    }
    
    private void CharacterRotation()
    {
        if(movement!= new Vector3(0,0,0))
            gameObject.transform.rotation = Quaternion.LookRotation(movement);
    }

    private void WalkingAnimation()
    {
        animator.SetFloat("MoveSpeed", movementSqrMagnitude);
    }
    private void FootstepAudio()
    {

    }
}
