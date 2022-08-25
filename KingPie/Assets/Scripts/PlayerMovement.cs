
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // [SerializeField] is used to show the  variable directly on the unity window and control it from there;
    // here the variable is speed

    [SerializeField] private float speed; 
    [SerializeField] private float jump;
    private Rigidbody2D body;
    private Animator anim;
    private bool grounded;

    // awake start when the script is loaded
    private void Awake() 
    {
        // GetComponent check that is RigidBody2D is present in that object
        // if present it will awake as it is call inside the awake function 

        body = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }
    
    // it use to update the every frame
    private void Update() 
    {
        // getAxis meaning of this value depends on the type of input control,
        // for example with a joystick's horizontal axis a value of 1 means the stick is pushed all the way to the right
        // and a value of -1 means it's all the way to the left;
        // a value of 0 means the joystick is in its neutral position.

        float horizantalInput = Input.GetAxis("Horizontal"); 


        // its use to set the velocity of the rigid body per unit sec;default vector2d(x,y) x=0,y=0 ;
        // here input is use to get input from getAxis and
        // Returns the value of the virtual axis identified by axisName;
        // and for y axis we dont want o change it so we have use body.vellocity.y

        body.velocity = new Vector2(horizantalInput * speed, body.velocity.y);

        //flip the player along horizontal axix left-right


        if (horizantalInput > 0.01f)
            transform.localScale = Vector3.one;
        else if (horizantalInput < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);


        // input from getkey ;get key is used to  get input from the keybord


        if (Input.GetKey(KeyCode.Space)&& grounded)        
        {
            //we have keep the x velocity same and the y velocity = the value of jump
            Junp();
            
        }

        //for animatoin
        //the string name is same as parameter set in animator
        //if arrow is not press then horizontalInput = 0
        //so 0!=0 false
        //then setbool (run) = 0 false means yhe character is idle;vice-versa
        anim.SetBool("run", horizantalInput != 0);
        anim.SetBool("grounded", grounded);
    }
 
    //for jumping
    
    private void Junp()
    {
        body.velocity = new Vector2(body.velocity.x, jump);
        anim.SetTrigger("jump");
        grounded = false;

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground");
        grounded = true;
    }
}
