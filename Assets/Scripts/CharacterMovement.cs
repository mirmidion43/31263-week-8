using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    private Vector3 movement;
    private float movementSqrMagnitude;
    public float walkSpeed = 1.5f;
    public Animator animator;
    public AudioSource footstepSource;
    public AudioClip[] footstepClips;
    public AudioSource background;
    public RaycastHit hitInfo;

    // Update is called once per frame
    void Update()
    {
        if(RaycastCheck())
        {
            GetMovementInput();
            CharacterRotation();
        }
        else
        {
            GetMovementInput();
            CharacterPosition();
            CharacterRotation();
            WalkingAnimation();
            FootstepAudio();
        }
           
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
        if(movementSqrMagnitude > 0.3f && !footstepSource.isPlaying)
        {
            if(footstepSource.clip == footstepClips[0])
                footstepSource.clip = footstepClips[1];
            else
            {
                footstepSource.clip = footstepClips[0];
            }
               
            footstepSource.volume = movementSqrMagnitude;
            footstepSource.Play();
            background.volume = 0.5f;
        }

        else if(movementSqrMagnitude <= 0.3f && footstepSource.isPlaying)
        {
            footstepSource.Stop();
            background.volume = 1.0f;
        }
    }

    private void OnTriggerExit(Collider other) 
    {
        Debug.Log("Trigger Exit: " + other.name + " : " + other.transform.position);
    }

    private void OnCollisionEnter(Collision other) 
    {
        Debug.Log("Collision Enter: " + other.collider.gameObject.name + " : " + other.contacts[0].point);
    }
    private void OnCollisionStay(Collision other) 
    {
        if(other.collider.gameObject.tag == "Impassable")
            Debug.Log("Collision Stay: " + other.collider.gameObject.name);
    }

    private bool RaycastCheck()
    {
        Vector3 origin = gameObject.transform.position + new Vector3(0.0f,0.5f,0.0f);
        bool hit = Physics.Raycast(origin, gameObject.transform.TransformDirection(Vector3.forward), out hitInfo, 5.0f);
        if(hit)
        {
            Debug.Log("Raycast Hit: " + hitInfo.collider.gameObject.name);
            if(hitInfo.collider.gameObject.tag == "Freeze")
                return true;
            else
                return false;
        }
        return false;
    }
}
