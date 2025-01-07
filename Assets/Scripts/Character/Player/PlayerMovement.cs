using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UIElements;
/*
Player defualt Movement
Jump, Walk, Run, etc.
 
 */


public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float walkSpeed;
    public GameObject world;
    public GameObject playerCamera;


    //Input State
    public float maxSpeed;
    public float jumpPower;


    //Check State
    public Vector3 velocity; // Y
    public Vector3 direction; //X-Z
    public Vector3 directionVelocity; //X-Z

    public bool isGrounded;
    public bool isJumping;
    public float jumpDegree;
    public float maxJumpDegree;
    public float jumpSpeed;
    public float frictionFactor;
    public Rigidbody2D rg;


    public float maxJumpCooltime;
    public float jumpCooltime;

    public PlayerState.CameraView cameraView;




    void Start()
    {
        rg = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //JumpCheck
        CheckGround();

        if (Input.GetKey(KeyCode.Space) && isGrounded && !isJumping && jumpCooltime<=0) {//NEED ADD : 점프 대기시간을 넣자
            isJumping = true;
            isGrounded = false;
            //velocity.y = jumpPower;
            rg.AddForce(Vector3.up * jumpPower, ForceMode2D.Impulse);
            /*
            if (rg.velocity.y < 0)
            {
                rg.AddForce(Vector3.up * jumpPower + Vector3.up * Mathf.Abs(rg.velocity.y), ForceMode.Impulse);
            }
            else
            {
                
            }*/
            jumpDegree = 0;
            jumpCooltime = maxJumpCooltime;
        }

        if (isGrounded) {
            isJumping = false;
        }
        

        
        
        

    }

    // Update is called once per frame
    //음 사실 공격을 당했을 때 날아가는 것도 rigid가 아니라 그냥 조정하면 되는 거 아닌가? 점점 charcater collider가 끌릴지도...
    void FixedUpdate()
    {

        Move();
        GroundFriction();

        
    }
    private void CheckGround()
    {
        Debug.DrawLine(transform.position, transform.position + Vector3.down * 0.1f, Color.red);
        isGrounded = Physics2D.BoxCast(transform.position, new Vector2(1f, 0.1f), 0f, Vector2.down, 0.1f);
        
    }
    

    //MOVE
    private void Move()
    {
        direction = new Vector2(Input.GetAxis("Horizontal"), 0);
        if(direction.x > 0)
        {
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        else if(direction.x < 0)
        {
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
        directionVelocity = direction * Time.deltaTime * walkSpeed;
        rg.velocity = new Vector2(directionVelocity.x, rg.velocity.y);
        
        //rg.AddForce(directionVelocity, ForceMode2D.Impulse);
    }

    private void GroundFriction()
    {
        if (isGrounded)
        {
            //rg.AddForce((new Vector2(-rg.velocity.x, 0)) * frictionFactor / 10, ForceMode2D.Impulse);
        }
    }
}
