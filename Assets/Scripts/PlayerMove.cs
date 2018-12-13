using UnityEngine;
using System.Collections;

public class PlayerMove : MonoBehaviour {

    public float movementSpeed= 10.0f;
    public float mouseSensitivity = 2.0f;

    public GameObject Camera;

    CharacterController characterController;
    // Use this for initialization
    void Start () {
        characterController = GetComponent<CharacterController>();
	}

    // Update is called once per frame
    void Update()
    {
        float rotLeftRight=Input.GetAxis("Mouse X")*mouseSensitivity;
        transform.Rotate(0, rotLeftRight, 0);
        float rotupDown = Input.GetAxis("Mouse Y") * mouseSensitivity;
     
    
        Camera.GetComponent<FollowPlayer2>().RotateAngle(rotLeftRight, rotupDown);
        //Movement
        float forwardSpeed = Input.GetAxis("Vertical")*movementSpeed;
        float sideSpeed = Input.GetAxis("Horizontal")*movementSpeed;
        Vector3 speed = new Vector3(sideSpeed, 0, forwardSpeed);
        speed = transform.rotation * speed;
        characterController.SimpleMove(speed);



    }
}
