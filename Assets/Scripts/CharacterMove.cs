using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    public float jumpPower;
    public float speed;
    bool isJump;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
    }
    void Update()
    { 
  
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            this.transform.Translate(-speed * Time.deltaTime, 0, 0, Space.World);
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            this.transform.Translate(speed * Time.deltaTime, 0, 0, Space.World);
        }
        if (Input.GetKey(KeyCode.Space))
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