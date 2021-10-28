using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMove : MonoBehaviour
{
    [SerializeField] private KeyCode LEFT;
    [SerializeField] private KeyCode RIGHT;
    [SerializeField] private KeyCode JUMP;
    [SerializeField] private KeyCode DASH;
   [SerializeField] private float jumpPower;
    [SerializeField] private float speed;
     [SerializeField] private float AnimationSpeed=1.0f;
     [SerializeField] private AudioSource WalkSound,JumpSound;
    [SerializeField] private string  State="Stand";
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
             this.transform.localRotation= Quaternion.Euler(0, -80.842f,0);
            if(Input.GetKey(DASH))
            {
                this.transform.position += new Vector3(-speed*2 * Time.deltaTime, 0, 0);
                if(State!="Jump")State="Run";
            }
            else 
            {
                this.transform.position += new Vector3(-speed * Time.deltaTime, 0, 0);
                 if(State!="Jump")State="Walk";
            }
        }
        else if (Input.GetKey(RIGHT))
        {
            this.transform.localRotation= Quaternion.Euler(0, 123.842f,0);
            if(Input.GetKey(DASH))
            {
                this.transform.position += new Vector3(speed*2 * Time.deltaTime, 0, 0);
                if(State!="Jump") State="Run";
            }
            else
            {
                this.transform.position +=new Vector3(speed * Time.deltaTime, 0, 0);
                 if(State!="Jump")State="Walk";
            }
        }
        else if(State!="Jump")
        {
            State="Stand";
        }
        if (Input.GetKey(JUMP))
        {
            if (!isJump)
            {
                isJump = true;
                this.rb.AddForce(new Vector3(0, jumpPower, 0), ForceMode.Impulse);
                State="Jump";
                AnimationManager.SetBool("IsJump",true);
                JumpSound.Play();
                WalkSound.Stop();
      
            }
        }


        if(State=="Walk")
        {
            MovingState=Mathf.MoveTowards(MovingState,1,Time.deltaTime*AnimationSpeed);
            WalkSound.pitch=2.0f;
            if(WalkSound.isPlaying!=true)WalkSound.Play();
            AnimationManager.SetFloat("MovingState",MovingState);
        }
        else if(State=="Run")
        {
            MovingState=Mathf.MoveTowards(MovingState,2,Time.deltaTime*AnimationSpeed);
            WalkSound.pitch=3.0f;
            if(WalkSound.isPlaying!=true)WalkSound.Play();
            AnimationManager.SetFloat("MovingState",MovingState);
        }
        else if( State=="Stand")
        {
            MovingState=Mathf.MoveTowards(MovingState,0,Time.deltaTime*AnimationSpeed); 
            AnimationManager.SetFloat("MovingState",MovingState); 
            WalkSound.Stop();
        }
    }

    void FixedUpdate()
    {
    }

    private void OnCollisionEnter(Collision other) 
    {
    
        if(other.gameObject.tag=="Floor") 
        {
            isJump=false;
            State="Stand";
            AnimationManager.SetBool("IsJump",false);
        }  
    }
    private void OnCollisionExit(Collision other) 
    {
      
        if(other.gameObject.tag=="Floor")
        {
            isJump=true;
        }
    }
}