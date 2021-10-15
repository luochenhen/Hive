using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private Rigidbody rb;
    public float speed;
    public float jumpForce;
    bool facingRight = true;
    Vector2 localScale;
    public float punchForce;


    public bool isGrounded;

    public GameObject groundCheck;
    public float checkRadius;
    //public LayerMask whatIsGrounded;

    private float jumpTimeCounter;
    public float jumpTime;
    //private bool isJumping;

    private float dirX;
    private float dirY;

    private float fixedDeltaTime;


    public MyJoyStick VJ;

    public Rigidbody bullet;
    public Rigidbody clone;
    public Transform fPoint;

    

    void Awake()
    {
      
        VJ.JumpEvent = Jump;
        this.fixedDeltaTime = Time.fixedDeltaTime;
        isGrounded = false;
        

    }
    void Jump(bool isJumping)
    {
        if (isJumping)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce) ;
    }
    void Punch(Vector2 direction,float dragTime)
    {
        direction = new Vector2(direction.x * punchForce * dragTime , direction.y * punchForce * dragTime);
        
        rb.AddForce(direction);
        //Debug.Log(direction);
        //clone = (Rigidbody)Instantiate(bullet, fPoint.position,fPoint.rotation);
        //clone.velocity = rb.velocity;
        //rb.AddForce(direction, ForceMode2D.Impulse);

    }

    void Charge(bool isDragging)
    {
        if (isDragging)
        {
          
            Time.timeScale = 0.4f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            //Debug.Log(isDragging);
        }
        else if(!isDragging)
        {
            //Debug.Log(isDragging);
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }

 
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody>();

    }

    void Update()
    {
        dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        dirY = CrossPlatformInputManager.GetAxis("Vertical");

        Collider[] collider = Physics.OverlapSphere(groundCheck.transform.position, checkRadius);
        int i = 0;
        while (i < collider.Length)
        {
            if (collider[i].tag == "Platform")
            {
                isGrounded = true;
               
            }

            i++;
            
        }
        

    }

    void FixedUpdate()
    {          
        if(dirX!=0)
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        VJ.MoveEvent = Punch;
            
        VJ.DragEvent = Charge;

        //Debug.Log(clone.velocity == new Vector3(0, 0, 0));
       
        //if (clone.velocity==new Vector3(0,0,0))
        //{
        //    GameObject obj = GameObject.Find("Circle(Clone)");
        //    Destroy(obj);
        //}
            
        
        //clone.position = rb.position;
    }
 
    void LateUpdate()
    {
        CheckWhereToFace();
    }

    void CheckWhereToFace()
    {
        if (dirX > 0)
            facingRight = true;
        else
            if (dirX < 0)
            facingRight = false;
        if (((facingRight) && (localScale.x < 0)) || ((!facingRight) && (localScale.x > 0)))
            localScale.x *= -1;
        transform.localScale = localScale;
    }
  

}
