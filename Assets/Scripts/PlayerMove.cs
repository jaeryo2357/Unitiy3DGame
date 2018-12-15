using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float movementSpeed= 10.0f;
    public float mouseSensitivity = 2.0f;
    Animator myAnimator;

    public GameObject Camera;

    CharacterController characterController;
    CharacterStatus status;
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
        status = GetComponent<CharacterStatus>();
        myAnimator = GetComponent<Animator>();
	}

    // Update is called once per frame
    void Update()
    {
        float rotLeftRight=Input.GetAxis("Mouse X")*mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);



        if (!status.SkillQ)
        {
            //Movement
            float forwardSpeed = Input.GetAxis("Vertical") * movementSpeed;
            if (forwardSpeed > 0)
            {
                myAnimator.SetBool("N", true);
                myAnimator.SetBool("S", false);
            }
            else if (forwardSpeed < 0)
            {
                myAnimator.SetBool("N", false);
                myAnimator.SetBool("S", true);
            }
            else
            {
                myAnimator.SetBool("N", false);
                myAnimator.SetBool("S", false);
            }
            float sideSpeed = Input.GetAxis("Horizontal") * movementSpeed;
            if (sideSpeed > 0)
            {
                myAnimator.SetBool("E", true);
                myAnimator.SetBool("W", false);
            }
            else if(sideSpeed<0)
            {
                myAnimator.SetBool("E", false);
                myAnimator.SetBool("W", true);
            }
            else
            {
                myAnimator.SetBool("E", false);
                myAnimator.SetBool("W", false);
            }
            Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
            speed = transform.rotation * speed;
            characterController.SimpleMove(speed);


        }
    }
}
