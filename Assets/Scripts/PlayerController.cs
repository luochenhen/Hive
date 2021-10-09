using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

public class PlayerController : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed;
    public float jumpForce;
    bool facingRight = true;
    Vector2 localScale;
    public float punchForce;


    public bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGrounded;

    private float jumpTimeCounter;
    public float jumpTime;
    //private bool isJumping;

    private float dirX;
    private float dirY;

    private float fixedDeltaTime;


    public MyJoyStick VJ;

    void Awake()
    {
      
        VJ.JumpEvent = Jump;
        this.fixedDeltaTime = Time.fixedDeltaTime;
    }
    void Jump(bool isJumping)
    {
        if (isJumping)
            rb.velocity = new Vector2(rb.velocity.x, jumpForce) ;
    }
    void Punch(Vector2 direction,float dragTime)
    {
        direction = new Vector2(direction.x * punchForce * dragTime , direction.y * punchForce * dragTime);
        Debug.Log(direction);
        rb.AddForce(direction);
        //rb.AddForce(direction, ForceMode2D.Impulse);

    }

    void Charge(bool isDragging)
    {
        if (isDragging)
        {
          
            Time.timeScale = 0.2f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
            Debug.Log(isDragging);
        }
        else if(!isDragging)
        {
            Debug.Log(isDragging);
            Time.timeScale = 1.0f;
            Time.fixedDeltaTime = this.fixedDeltaTime * Time.timeScale;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        localScale = transform.localScale;
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        dirX = CrossPlatformInputManager.GetAxis("Horizontal");
        dirY = CrossPlatformInputManager.GetAxis("Verticle");
       
        //if (CrossPlatformInputManager.GetButtonDown("Jump"))
        //    Jump();

        //isGrounded = Physics2D.OverlapCircle(feetPos.position,checkRadius,whatIsGrounded );

        //if (isGrounded == true && CrossPlatformInputManager.GetButtonDown("Jump"))
        //{
        //    isJumping = true;
        //    jumpTimeCounter = jumpTime;
        //    rb.velocity = Vector2.up * jumpForce;

        //}

        //if (Input.GetKey(KeyCode.Space)&& isJumping==true)
        //{
        //    if (jumpTimeCounter > 0)
        //    {
        //        rb.velocity = Vector2.up * jumpForce;
        //        jumpTimeCounter -= Time.deltaTime;
        //    }
        //    else
        //        isJumping = false;
        //}

        //if (Input.GetKeyUp(KeyCode.Space))
        //{
        //    isJumping = false;
        //}

    }

    void FixedUpdate()
    {
        //if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        //{
        //    int dieX = 0;
        //    int dieY = 0;
        //    Input.GetTouch(0).deltaPosition
        //    Vector2 touchDelPos = Input.GetTouch(0).deltaPosition;
        //    //比较两个方向滑动的绝对值的大小
        //    if (Mathf.Abs(touchDelPos.x) > Mathf.Abs(touchDelPos.y))
        //    {
        //       
        //        if (touchDelPos.x > 10)
        //        {
        //            dieX = 1;
        //            rb.velocity = new Vector2(dieX * speed, rb.velocity.y);
        //        }//X方向的作用滑动
        //        else if (touchDelPos.x < -10)
        //        {
        //            dieX = -1;
        //            rb.velocity = new Vector2(dieX * speed, rb.velocity.y);
        //        }
        //    }
        //    else
        //    {
        //        if (touchDelPos.y > 10)
        //        {
        //            dieY = 1;
        //        }
        //        else if (touchDelPos.y < -10)
        //        {
        //            dieY = -1;
        //        }
        //    }
        //}
        //moveInput = joystick.Horizontal;
        if(dirX!=0)
            rb.velocity = new Vector2(dirX * speed, rb.velocity.y);

        VJ.MoveEvent = Punch;
        VJ.DragEvent = Charge;

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

    
    /*if(按下==true)
    {
      if(抬起时间-按下时间<0.5s)
        判定为跳跃
        if(isGrounded==true)
          跳跃;
      if(抬起时间-按下时间>0.5s&&抬起时间-按下时间)
         冲拳;
         方向*时间*施加的力;
      else
         冲拳;
         方向*最大时间*力;
         按下==false;
    */
}
