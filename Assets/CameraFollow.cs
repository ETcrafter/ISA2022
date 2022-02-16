using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public float CameraMoveSpeed = 120f;
    public GameObject CameraFollowObject; 
    Vector3 FollowPOS;
    public float ClampAngleMax = 30f;
    public float ClampAngleMin = 0f;
    public float InputSensitivity = 150f;
    public GameObject CameraObject;
    public GameObject PlayerObject;
    public float CamDistanceXToPlayer;
    public float CamDistanceYToPlayer;
    public float CamDistanceZToPlayer;
    public float MouseX;
    public float MouseY;
    public float FinalInputX;
    public float FinalInputZ;
    public float SmoothX;
    public float SmoothY;
    private float RotY = 0f;
    private float RotX = 0f;



    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        Vector3 Rot = transform.localRotation.eulerAngles;
        RotY = Rot.y;
        RotX = Rot.x;
    }

 
    void Update()
    {
        float InputX = Input.GetAxis("RightStickHorizontal");
        float InputZ = Input.GetAxis("RightStickVertical");
        MouseX = Input.GetAxis("Mouse X");
        MouseY = Input.GetAxis("Mouse Y");
        FinalInputX = InputX + MouseX;
        FinalInputZ = InputZ + MouseY;

        RotY += FinalInputX * InputSensitivity * Time.deltaTime;
        RotX += FinalInputZ * InputSensitivity * Time.deltaTime;

        RotX = Mathf.Clamp(RotX, ClampAngleMin, ClampAngleMax);
        Quaternion localRotation = Quaternion.Euler(RotX, RotY, 0f);
        transform.rotation = localRotation;
    }

    void LateUpdate()
    {
        CameraUpdater();
    }

    void CameraUpdater()
    {
        //set the target object
        Transform target = CameraFollowObject.transform;

        //move toward the target
        float Step = CameraMoveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, target.position, Step);
    }
}
