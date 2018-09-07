using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseLook : MonoBehaviour
{
    [Header("Rotational Axis")]
    //Public for RotationalAxis of mouse on X axis
    public RotationalAxis axis = RotationalAxis.MouseX;

    [Header("Sensitivity")]
    //Public floats for x and y sensitivity
    public float sensitivityX = 10f;
    public float sensitivityY = 10f;

    [Header("Y Axis Clamp")]
    //Minimal and Maximum Limit for rotational axis on Y axis
    public float minimumY = -60f;
    public float maximumY = 60f;

    //Default float value for mouse invertion.
    float rotationY = 0f;

    private void Start()
    {
        //If there is a Rigidbody attached to object/character
        if (this.GetComponent<Rigidbody>())
        {
            //freeze the rotation if there turns out to be a rigidbody attached
            this.GetComponent<Rigidbody>().freezeRotation = true;
        }
    }

    public enum RotationalAxis
    {
        //State of RotationalAxis
        MouseXandY,
        MouseX,
        MouseY
    }


    private void Update()
    {
        #region Mouse X and Y
        //If axis is set to Mouse X and Mouse Y
        if (axis == RotationalAxis.MouseXandY)
        {
            //Float rotation x is equal to our Y + the mouse input on the X axis times our X sensitivity
            float rotationX = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * sensitivityX;

            //Y Rotation is plus equals our mouse input Y times Y sensitivity
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            //Minimum and Maximum of Y Rotation is clamped using Mathf. (put rotation first, then minimum, then maximum)
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //Transform our local position to the new vector3 rotation -y rotation on the X axis and X rotation on the Y axis
            transform.localEulerAngles = new Vector3(-rotationY, rotationX, 0);
        }
        #endregion

        #region Mouse X
        //else if we are rotating on the X 
        else if (axis == RotationalAxis.MouseX)
        {
            //transform the rotation on our game objects around Y axis by our mouse input. Mouse X times X sensitivity
            //X sensitivity [x                y                          z]
            transform.Rotate(0, Input.GetAxis("Mouse X") * sensitivityX, 0);
        }
        #endregion
        #region Mouse Y
        //else we are only rotating on Y axis
        else
        {
            //Rotation Y is plus equal to our mouse input for  Mouse Y times Y sensitivity
            rotationY += Input.GetAxis("Mouse Y") * sensitivityY;

            //Minimum and Maximum Y Rotaton is clamped by MathF.
            rotationY = Mathf.Clamp(rotationY, minimumY, maximumY);

            //transform our local position to the new vector3 rotation -y rotation on the x axis and local euler angle y on the y axis 
            transform.localEulerAngles = new Vector3(-rotationY, 0, 0);
        }
        #endregion
        //Homework! 
        //1.Fix the if statement (make it more effiecient eg use switch statement)
        //2.Have the rotatio affected by time (time.Deltatime) (if you pause the game, it should stop)
        //3. Create one script that controls both the player and the camera
    }
}
