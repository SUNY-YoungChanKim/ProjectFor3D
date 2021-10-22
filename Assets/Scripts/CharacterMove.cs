using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private KeyCode LEFT;
    [SerializeField] private KeyCode RIGHT;
    [SerializeField] private KeyCode JUMP;
   [SerializeField] private float jumpPower;
    [SerializeField] private float speed;
     [SerializeField] private float AnimationSpeed=1.0f;
    private bool isJump;
    private Rigidbody rb;
    private Animator AnimationManager; 
    private float MovingState=0;
    
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        isJump = false;
        // 이동시 Z축 포지션 잠그고 XYZ축 로테이션 잠금
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotation;

        AnimationManager = this.GetComponent<Animator>();


        
    }
    void Update()
    { 
  
        if (Input.GetKey(LEFT))
        {
            this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
            MovingState=Mathf.MoveTowards(MovingState,1,Time.deltaTime*AnimationSpeed);
            
        }
        else if (Input.GetKey(RIGHT))
        {
            this.transform.position +=new Vector3(speed * Time.deltaTime, 0, 0);
            MovingState=Mathf.MoveTowards(MovingState,1,Time.deltaTime*AnimationSpeed);
        }
        else if (Input.GetKey(JUMP))
        {
            if (!isJump)
            {
                isJump = true;
                this.rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
            }
           
        }
        else
        {
            MovingState=Mathf.MoveTowards(MovingState,0,Time.deltaTime*AnimationSpeed);
        }
        AnimationManager.SetFloat("MovingState",MovingState);
    }

    void FixedUpdate()
    {
    }

    void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.name == "Floor")
        {
            isJump = false;
        }
    }
}