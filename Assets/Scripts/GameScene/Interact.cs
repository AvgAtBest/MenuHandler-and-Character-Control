using System.Collections;
using UnityEngine;

public class Interact : MonoBehaviour
{
    [Header("References")]
    public GameObject player;
    public GameObject mainCam;
    //Alternative reference method: public Camera cam;
    Dialogue dialogue;
    GameObject npc;

	void Start ()
    {
        //Set Cursor lock state to locked
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player = GameObject.FindGameObjectWithTag("Player");
        mainCam = GameObject.FindGameObjectWithTag("MainCamera");
        //Alternative method for obtaining Camera on void start: cam = mainCam.GetComponent<Camera>();
        //Alternative method for obtaining Camera on void start: cam = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<Camera>();

    }

    // Update is called once per frame
    void Update ()
    {
        if (Input.GetKeyDown(KeyCode.E))
            //Actions input when E key is pressed down
        {
            Ray interact;
            interact = Camera.main.ScreenPointToRay
                (new Vector2(Screen.width / 2, Screen.height / 2));
            RaycastHit hitInfo;
            //information we get back upon hiting a object
            if(Physics.Raycast(interact, out hitInfo, 10.0f))
                //Physics of raycast. Interacts with object and returns info from 10.0f
            {
                #region NPC Dialogue
                if(hitInfo.collider.CompareTag("NPC"))
                {
                    //dlg = hitinfo check for dlg
                    Dialogue dlg = hitInfo.transform.GetComponent<Dialogue>();
                    //if player has dialogue
                    if (dlg != null)
                    {
                        //show dialogue
                        dlg.showSpeech = true;
                        player.GetComponent<CharacterMovement>().enabled = false;
                        //turn off camera look
                        //turn off character look
                        //turn off character movement
                        
                        //Set cursor to unlocked
                        Cursor.lockState = CursorLockMode.None;
                        //Set cursor to visible
                        Cursor.visible = true;
                        Debug.Log("Talk to NPC");
                    }
                }
                #endregion
                #region Chest
                if(hitInfo.collider.CompareTag("Chest"))
                {
                    Debug.Log("Open Chest");
                }
                #endregion
                #region Item
                if (hitInfo.collider.CompareTag("Item"))
                {
                    Debug.Log("Pick up Item");
                }
                #endregion
            }
        }
	}
}