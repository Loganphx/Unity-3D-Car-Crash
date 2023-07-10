using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform playerTransform;
    [SerializeField] private Vector3 cameraPositionOffset;

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = playerTransform.position + cameraPositionOffset;
        //transform.LookAt(playerTransform);
    }
}
