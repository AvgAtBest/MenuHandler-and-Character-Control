using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//this script can be found in the Component section under the option NPC/Dialogue
[AddComponentMenu("First Person/Character Movement")]
public class Dialogue : MonoBehaviour
{
    #region Variables
    [Header("References")]
    public bool showSpeech = false;
    //boolean to toggle if we can see a characters dialogue box

    public int index, optionIndex;
    //index for our current line of dialogu and an index for a set question marker of the dialogue 

    public CharacterMovement controller;
    //script reference to the player movement

    public CharacterMovement camLook;
    //mouselook script reference for the maincamera
    public CharacterMovement charLook;
    //mouselook script reference for player

    [Header("NPC Name and Dialogue")]
    //name of this specific NPC
    public string npcName;
    public string[] curDlgText;
    //array for text for our dialogue
    [Header("Screen Ratio")]
    public Vector2 scr;
    //Screen x and y
    #endregion
    #region Start
    private void Start()
    {
        //find and reference the player object by tag
        controller = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        //char mouselook
        charLook = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();

        camLook = GameObject.FindGameObjectWithTag("Player").GetComponent<CharacterMovement>();
        //find and reference the maincamera by tag and get the mouse look component 
    }
    #endregion
    public void OnGUI()
    {
        //if our dialogue can be seen on screen
        if (showSpeech)
        {
            //set up our ratio messurements for 16:9
            if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
            {
                scr.x = Screen.width / 16;
                scr.y = Screen.height / 9;
            }

            //the dialogue box takes up the whole bottom 3rd of the screen and displays the NPC's name and current dialogue line
            GUI.Box(new Rect(0, 6 * scr.y, Screen.width, 3 * scr.y), npcName + ": " + curDlgText[index]);

            //if not at the end of the dialogue or not at the options part
            if (!(index >= curDlgText.Length - 1 || index == optionIndex))
            {
                //next button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Next"))
                {
                    //move forward in diagloue array
                    index++;
                }

            }
            //else if we are at options
            else if (index == optionIndex)
            {
                //Accept button allows us to skip forward to the next line of dialogue
                if (GUI.Button(new Rect(13 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Accept"))
                {
                    index++;
                }
                //Decline button skips us to the end of the characters dialogue 
                if (GUI.Button(new Rect(14 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Decline"))
                {
                    //skip to end of dialogue
                    index = curDlgText.Length - 1;
                }
            }
            else
            //else we are at the end
            {
                if (GUI.Button(new Rect(15 * scr.x, 8.5f * scr.y, scr.x, 0.5f * scr.y), "Goodbye"))
                {
                    //close the dialogue box
                    showSpeech = false;
                    //set index back to 0
                    index = 0;
                    //allow cameras mouselook to be turned back on
                    camLook.enabled = true;
                    //get the component mouselook on the character and turn that back on

                    charLook.enabled = true;
                    //get the component movement on the character and turn that back on
                    controller.enabled = true;

                    //lock the mouse cursor
                    Cursor.lockState = CursorLockMode.Locked;
                    //set the cursor to being invisible
                    Cursor.visible = false;
                }
            }
        }
    }
}