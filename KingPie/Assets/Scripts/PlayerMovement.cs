
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]private float speed; // [SerializeField] is used to show the  variable directly on the unity window and control it from there; here the variable is speed
    [SerializeField] private float jump;
    private Rigidbody2D body;

    private void Awake() // awake start when the script is loaded
    {
        body = GetComponent<Rigidbody2D>(); // GetComponent check that is RigidBody2D is present in that object ; if present it will awake as it is call inside the awake function 

    }
    private void Update() // it use to update the every frame
    {
        body.velocity = new Vector2(Input.GetAxis("Horizontal")*speed,body.velocity.y); // its use to set the velocity of the rigid body per unit sec;default vector2d(x,y) x=0,y=0 ; here input is use to get input from getAxis and Returns the value of the virtual axis identified by axisName;and for y axis we dont want o change it so we have use body.vellocity.y

        if (Input.GetKey(KeyCode.Space)) // input from getkey ;get key is used to  get input from the keybord
        {
            body.velocity = new Vector2(body.velocity.x, jump); //we have keep the x velocity same and the y velocity = the value of jump
        }

    }
}
