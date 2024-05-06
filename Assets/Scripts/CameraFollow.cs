using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]

public class CameraFollow : MonoBehaviour
{
    [SerializeField] public GameObject FollowTarget;
    [SerializeField] public float CameraSpeedMultiplier = 3;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Vector3 newPosition = Vector3.Lerp(transform.position, FollowTarget.transform.position, Time.deltaTime*CameraSpeedMultiplier);

        // Preserve the Original Z position
        float originalZ = transform.position.z;
        newPosition.z = originalZ;

        transform.position = newPosition;
    }
}
