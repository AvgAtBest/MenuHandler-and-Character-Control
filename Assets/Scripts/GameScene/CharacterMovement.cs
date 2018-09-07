using System.Collections;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class CharacterMovement : MonoBehaviour
{
    [Header("Movement Variables")]
    [Space(30)]
    [Header("Mini Header")]
    [Range(0f, 100f)]
    public float speed = 10.0f;
    //How fast the character travels when moving. Set to 6.0f
    public float jumpSpeed = 8.0f;
    //Jump speed of character upon jumping, set to 8.0f
    public float gravity = 20.0f;
    //Gravity affecting player, set to 20.0f value
    private Vector3 moveDirection = Vector3.zero;
    //Characters move direction on the xyz axis is set to 0, so no input will occur when starting the scene
    public CharacterController controller;
    //Referencing the character controller in Unity
    public float sprintSpeed = 15f;
    public float crouchSpeed = 5f;

    public enum State
    {
        MouseXandY = 0,
        MouseX = 1,
        MouseY = 2
    }
    #region Mouse Look variables
    //Default current state for Mouse is set to X and Y axis
    public State currentState = State.MouseXandY;

    [Header("Sensitivity")]
    //Floats for x and y sensitivity
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    [Header("Y Axis Clamp")]
    //Rotation limit for Y axis
    public float minimumY = -60f;
    public float maximumY = 60f;

    float rotationY = 0f;
    //Default float value for mouse invertion.
    #endregion
    private void Start ()
    {
        controller = GetComponent<CharacterController>();
        //Obtains the CharacterController Game Component on scene start for the first time, does not continously try and obtain after void start
	}

    void FixedUpdate()
    {
        if (controller.isGrounded)
        //If the character is resting on 0xyz axis
        {
            moveDirection = new Vector3(Input.GetAxis("Horizontal"),
                //Obtains characters x coordinates. Upon character input, updates character coords on x axis
                0, Input.GetAxis("Vertical"));//Does not affect Y axis, character stays grounded
            moveDirection = transform.TransformDirection(moveDirection);
            //Moves Character on x axis

            moveDirection *= speed;
            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
            }

            if (Input.GetKeyUp(KeyCode.LeftShift))
            {
                speed = 10f;
            }

            if (Input.GetKey(KeyCode.C))
            {
                speed = crouchSpeed;
            }

            if (Input.GetKeyUp(KeyCode.C))
            {
                speed = 10f;
            }
                //When moving, moves character at speed of 6.0f
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = jumpSpeed;
            }
        }
        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * Time.deltaTime);

        switch (currentState)
        {
            case State.MouseXandY:
                MouseXandY();
                break;
            case State.MouseX:
                MouseX();
                break;
            case State.MouseY:
                MouseY();
                break;
            default:
                break;

        }
    }
    private void MouseXandY()
    {
        //The float for X axis is equal to Y axis + the mouse input on the X axis times our X sensitivity
        float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;
        //Y Rotation is += our mouse Y times Y sensitivity
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //Min and Max Y axis limit is clamped using Mathf.
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //Transform players local position to the new vector3 rotation. -y rotation on the X axis and X rotation on the Y axis
        transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
    }
    private void MouseX()
    {
        //transform rotation around Y axis by mouse input. Mouse X times sensitivityX
        transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
    }
    private void MouseY()
    {
        //Rotation Y is += to mouse input for Mouse Y times Y sensitivity
        rotationY += Input.GetAxis("Mouse Y") * sensitivityY;
        //Min and Max Y axis limit is clamped using Mathf.
        rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);
        //Transform players local position to the new vector3 rotation. -y rotation on the X axis and X rotation on the Y axis
        transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
    }
}
