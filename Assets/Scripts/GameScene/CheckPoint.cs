using UnityEngine;
using System.Collections;
//this script can be found in the Component section under the option Character Set Up 
//CheckPoint
[AddComponentMenu("FirstPerson/Character Movement")]
public class CheckPoint : MonoBehaviour 
{
    #region Variables
    [Header("Check Point Elements")]
    public GameObject curCheckpoint;
    [Header("Character Handler")]
    public CharacterHandler charH;

    #endregion
    #region Start
    private void Start()
    {
        charH = this.GetComponent<CharacterHandler>();
        #region Check if we have Key
        if (PlayerPrefs.HasKey("SpawnPoint"))
        {
            curCheckpoint = GameObject.Find(PlayerPrefs.GetString("SpawnPoint"));
            transform.position = curCheckpoint.transform.position;
        }
    }
    #endregion
    #endregion
    #region LateUpdate
    private void LateUpdate()
    {
        //if our characters health is less than or equal to 0
        if (charH.curHealth <= 0)
        {
            transform.position = curCheckpoint.transform.position;
            //our transform.position is equal to that of the checkpoint
            charH.curHealth = charH.maxHealth;
            //our characters health is equal to full health
            charH.alive = true;
            //character is alive
            charH.controller.enabled = true;
            //characters controller is active
        }
    }
    #endregion
    #region OnTriggerEnter
    private void OnTriggerEnter (Collider other)
    {
        if(other.gameObject.tag==("Checkpoint"))
        {
            curCheckpoint = other.gameObject;
            transform.position = other.gameObject.transform.position;
            PlayerPrefs.SetString("SpawnPoint", curCheckpoint.name);
        }
    }
    //Collider other
    //if our other objects tag when compared is CheckPoint
    //our checkpoint is equal to the other object
    //save our SpawnPoint as the name of that object
    #endregion
}





