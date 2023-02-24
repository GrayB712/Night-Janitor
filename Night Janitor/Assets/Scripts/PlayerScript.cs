using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    //The Camera
    public GameObject theCamera;

    public float numberOfObjectsDragging = 0;

    //Variable that tells whether or not the tasklist is out
    public bool tasklistIsVisible = false;

    //GameObject which contains the tasklist
    public GameObject taskList;

    public float addedSpeed;

    //Rigidbody on the player
    private Rigidbody2D rb;

    //Movement Speed
    public float speed = 10f;
    float normalSpeed;

    //Horizontal Input
    private float horizontal = 0f;

    //Vertical Input
    private float vertical = 0f;

    //Vector2 comprised of "vertical" (vertical input) and "horizontal" (horizontal input)
    private Vector2 input;

    Animator animator;
    bool playIdle;

    public bool paused = false;

    // public float walkAcceleration = 5f;
    // public float groundDeceleration = 5f;
    // public Vector3 velocity = new Vector3(0, 0, 0);
    // public float speed = 5f;

    // public float verticalMoveInput = 0f;
    // public float horizontalMoveInput = 0f;
    // Start is called before the first frame update
    void Start()
    {
        normalSpeed = speed;
        //Gets the rigidbody component from player object
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //Moves Camera to Player 
        theCamera.transform.position = new Vector3(transform.position.x, transform.position.y, theCamera.transform.position.z);

        if (Input.GetKeyDown(KeyCode.E))
        {
            switchTaskListVisibility();
        }

        if(Input.GetKey(KeyCode.W))
        {
             vertical = 1;
        }

        else if(Input.GetKey(KeyCode.S))
        {
            vertical = -1;
        }

        else
        {
            vertical = 0;
        }

        //Check for horizontal input
        if(Time.timeScale != 0)
        {
            if(Input.GetKey(KeyCode.D))
            {
                horizontal = 1;
                transform.rotation = new Quaternion(0, 180, 0, 1);
            }
            else if(Input.GetKey(KeyCode.A))
            {
                horizontal = -1;
                transform.rotation = new Quaternion(0, 0, 0, 1);
            }
            else
            {
                horizontal = 0;
            }
        }


        // if (horizontalMoveInput != 0)
        // {
	    //     velocity.x = Mathf.MoveTowards(velocity.x, speed * horizontalMoveInput, walkAcceleration * Time.deltaTime);
        // }
        // else
        // {
	    //     velocity.x = Mathf.MoveTowards(velocity.x, 0, groundDeceleration * Time.deltaTime);
        // }

        // if (verticalMoveInput != 0)
        // {
	    //     velocity.y = Mathf.MoveTowards(velocity.y, speed * verticalMoveInput, walkAcceleration * Time.deltaTime);
        // }
        // else
        // {
	    //     velocity.y = Mathf.MoveTowards(velocity.y, 0, groundDeceleration * Time.deltaTime);
        // }


        // velocity.x = Mathf.MoveTowards(velocity.x, speed * horizontalMoveInput, walkAcceleration * Time.deltaTime);
        // velocity.y = Mathf.MoveTowards(velocity.x, speed * verticalMoveInput, walkAcceleration * Time.deltaTime);
        
        // transform.Translate(velocity * Time.deltaTime);
        

    }
    private void FixedUpdate()
    {
        //Makes a new Vector 2 for input containing the player's horizontal and vertical input
        input = new Vector2(horizontal, vertical);
        
        //Tells animator what to play
        if (vertical == 0 && horizontal == 0)
        {
            playIdle = true;
        }
        else
        {
            playIdle = false;
        }

        animator.SetBool("Idle", playIdle);
        input = input * speed;


        //Moves the player to the new calculated position
        if(!paused)
        {
            //rb.MovePosition(rb.position + input * Time.fixedDeltaTime);
            rb.velocity = (input * Time.fixedDeltaTime);
            //rb.velocity + 
        }
        
    }


    void switchTaskListVisibility()
    {
        //(You have to make the key code a string to make
        // it comparable) It switches the the tasklistIsVisible variable
        tasklistIsVisible = !tasklistIsVisible;
        if(tasklistIsVisible)
        {
            //If the tasklist should be visible, put it in this position:
            taskList.transform.localPosition = new Vector3(295, 29, taskList.transform.localPosition.z);
        }
        if(!tasklistIsVisible)
        {
            //If the tasklist shouldn't be visible, put it in this position:
            taskList.transform.localPosition = new Vector3(492, 29, taskList.transform.localPosition.z);
        }
        
    }
}
