
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] is used to show the  variable directly on the unity window and control it from there;
    // here the variable is speed

    [SerializeField] private float speed; 
    [SerializeField] private float jump;

    [Header("jumpSound")]
    [SerializeField] private AudioClip jumpsound;


    private Rigidbody2D body;
    private Animator anim;
    private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask groundLayer; 
    [SerializeField] private LayerMask wallLayer;
    private float wallJumpCoolDown;
    private float horizontalInput;
    


    // awake start when the script is loaded
    private void Awake() 
    {
        // GetComponent check that is RigidBody2D is present in that object
        // if present it will awake as it is call inside the awake function 

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }
    
    // it use to update the every frame
    private void Update() 
    {
        // getAxis meaning of this value depends on the type of input control,
        // for example with a joystick's horizontal axis a value of 1 means the stick is pushed all the way to the right
        // and a value of -1 means it's all the way to the left;
        // a value of 0 means the joystick is in its neutral position.

        horizontalInput = Input.GetAxis("Horizontal"); 


        

        //flip the player along horizontal axix left-right


        if (horizontalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizontalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // input from getkey ;get key is used to  get input from the keybord


       

        // for animatoin
        // the string name is same as parameter set in animator
        // if arrow is not press then horizontalInput = 0
        // so 0!=0 false
        // then setbool (run) = 0 false means yhe character is idle;vice-versa
        anim.SetBool("run", horizontalInput != 0);

       
        anim.SetBool("grounded", isGrounded());
        

        // wall jump logic

        if (wallJumpCoolDown > 0.2f)
        {
            
            // its use to set the velocity of the rigid body per unit sec;default vector2d(x,y) x=0,y=0 ;
            // here input is use to get input from getAxis and
            // Returns the value of the virtual axis identified by axisName;
            // and for y axis we dont want o change it so we have use body.vellocity.y

            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            //this will stuck the player in the wall and not fall down
            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
                body.gravityScale = 3;
               if ((Input.GetKey(KeyCode.Space)) || (Input.GetKey(KeyCode.W)) || Input.GetKey(KeyCode.UpArrow))
                {
                    // we have keep the x velocity same and the y velocity = the value of jump
                    Junp();
                if (Input.GetKeyDown(KeyCode.Space)|| Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
                {
                    SoundManager.instance.PlaySound(jumpsound);
                }

                 }
            

        }
        else
            wallJumpCoolDown += Time.deltaTime;
    }
 
    //for jumping
    
    private void Junp()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jump);
            anim.SetTrigger("jump");
        }
        else if(onWall() && !isGrounded())
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0); 
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }
            else
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);

            wallJumpCoolDown = 0;
        }
        
        

    }
   
    private bool isGrounded()
    {
        // raycast  is a bool that Returns true if the ray intersects with a Collider, otherwise false.
        // boxcast is also a bool that Casts the box along a ray and returns detailed information on what was hit.

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit.collider != null;
    }  
    private bool onWall()
    {
        // raycast  is a bool that Returns true if the ray intersects with a Collider, otherwise false.
        // boxcast is also a bool that Casts the box along a ray and returns detailed information on what was hit.
        
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(transform.localScale.x,0 ), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }
    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
        
    }
}
