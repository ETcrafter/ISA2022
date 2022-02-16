using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraCollision : MonoBehaviour
{
    public float MinDistance = 1f;
    public float MaxDistance = 6f;
    public float Smooth = 10f;
    Vector3 DollyDir;
    public Vector3 DollyDirAdjusted;
    public float distance;

    // Start is called before the first frame update
    void Awake()
    {
        DollyDir = transform.localPosition.normalized;
        distance = transform.localPosition.magnitude;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 DesiredCameraposition = transform.parent.TransformPoint(DollyDir * MaxDistance);
        RaycastHit hit;

        if (Physics.Linecast (transform.parent.position, DesiredCameraposition, out hit))
        {
            distance = Mathf.Clamp (hit.distance, MinDistance, MaxDistance);
        }
        else
        {
            distance = MaxDistance;
        }

        transform.localPosition = Vector3.Lerp(transform.localPosition, DollyDir * distance, Time.deltaTime * Smooth);
    }
}
