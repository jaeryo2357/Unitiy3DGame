using UnityEngine;
using System.Collections;

using UnityEngine;
using System.Collections;

public class FollowPlayer2 : MonoBehaviour
{

    public float distance = 5.0f;
    public float horizontalAngle = 0.0f;
    public float rotAngle = 100.0f;
    public float verticalAngle = 10.0f;
    public Transform lookTarget;
    public Vector3 offset = Vector3.zero;


    // Use this for initialization
    void Start()
    {
 
    }

    void LateUpdate()
    {
        
       
        
        if (lookTarget != null)
        {
            Vector3 lookPosition = lookTarget.position + offset;
            Vector3 relativePos = Quaternion.Euler(verticalAngle, horizontalAngle, 0) * new Vector3(0, 0, -distance);

            transform.position = lookPosition + relativePos;
            transform.LookAt(lookPosition);

           

        }
    }
    public void RotateAngle(float x,float y)
    {
        
            float anglePerPixel = rotAngle / (float)Screen.width;
        horizontalAngle += x;
            horizontalAngle = Mathf.Repeat(horizontalAngle, 360.0f);
        verticalAngle -= y;
            verticalAngle = Mathf.Clamp(verticalAngle, -60.0f, 60.0f);
        
    }
    // Update is called once per frame
    void Update()
    {

    }
}

