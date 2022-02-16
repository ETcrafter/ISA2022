using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform target;

    public Vector3 offset;
   /* public float smoothSpeed =  -0.5f;*/

    public bool useOffsetValues;

    public float rotateSpeed;

    public Transform pivot;

    public float maxViewAngle;
    public float minViewAngle;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;

        if (!useOffsetValues)
        {
            offset = target.position - transform.position;
        }

        pivot.transform.position = target.transform.position;
        pivot.transform.parent = target.transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        //Get the X position of the mouse and rotate target
        float horizontal = Input.GetAxis("Mouse X") * rotateSpeed;
        target.Rotate (0, horizontal, 0);

        //Get the Y position of the mouse and pivot
        float vertical = Input.GetAxis("Mouse Y") * rotateSpeed;
        pivot.Rotate (-vertical, 0, 0);

        //Limit up/down camera controls
        if (pivot.rotation.eulerAngles.x > maxViewAngle && pivot.rotation.eulerAngles.x < 180f)
        {
            pivot.rotation = Quaternion.Euler(maxViewAngle, 0, 0);
        }

        if (pivot.rotation.eulerAngles.x > 180 && pivot.rotation.eulerAngles.x < 360f + minViewAngle)
        {
            pivot.rotation = Quaternion.Euler(360f + minViewAngle, 0, 0);
        }


        //Move the camera based on rotation target (and original offset)
        float desiredYAngle = target.eulerAngles.y;
        float desiredXAngle = pivot.eulerAngles.x;

        Quaternion rotation = Quaternion.Euler(desiredXAngle, desiredYAngle, 0);
        transform.position = target.position - (rotation * offset);


        if (transform.position.y < target.position.y)
        {
            transform.position = new Vector3(transform.position.x, target.position.y, transform.position.z);
        }

        transform.LookAt(target);
    }
}
