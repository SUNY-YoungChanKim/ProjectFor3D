using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMove : MonoBehaviour
{
    [SerializeField] private GameObject Target;
    Transform playerTransform;
    Vector3 offset;
    // Start is called before the first frame update
    void Awake()
    {
        playerTransform = Target.transform;
        offset = transform.position - playerTransform.position;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        this.transform.position = playerTransform.position + offset;
    }
}