using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private KeyCode LEFT;
    [SerializeField] private KeyCode RIGHT;
    [SerializeField] private KeyCode JUMP;
    public float jumpPower;
    public float speed;
    bool isJump;
    Rigidbody rb;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
        // 이동시 Z축 포지션 잠그고 XYZ축 로테이션 잠금
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;
        
    }
    void Update()
    { 
  
        if (Input.GetKey(LEFT))
        {
            this.transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
        }
        else if (Input.GetKey(RIGHT))
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
        }
        if (Input.GetKey(JUMP))
        {
            if (!isJump)
            {
                isJump = true;
                this.rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            }
           
        }
    }

    void FixedUpdate()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        rb.AddForce(new Vector3(h, v, 0), ForceMode.Impulse);
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isJump = false;
        }
    }
}