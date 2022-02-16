using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RigidBodyCamera : MonoBehaviour
{
    public float DistanceFromTarget = 6;
    public Transform Target;

    float Yaw;
    float Pitch;
    public Vector2 PitchMinMax = new Vector2(-40, 85);

    public float RotationSmoothTime = 1.3f;
    Vector3 RotationSmoothVelocity;
    Vector3 CurrentRotation;

    public float MouseSensitivity = 10;

    void Start()
    {

        //Laat de cursor verdwijnen
        Cursor.lockState = CursorLockMode.Locked;
    }

    void LateUpdate()
    {
        Yaw += Input.GetAxis("Mouse X") * MouseSensitivity;
        Pitch -= Input.GetAxis("Mouse Y") * MouseSensitivity;
        Pitch = Mathf.Clamp(Pitch, PitchMinMax.x, PitchMinMax.y);

        CurrentRotation = Vector3.SmoothDamp(CurrentRotation, new Vector3(Pitch, Yaw), ref RotationSmoothVelocity, RotationSmoothTime);
        transform.eulerAngles = CurrentRotation;

        transform.position = Target.position - transform.forward * DistanceFromTarget;

    }
}
