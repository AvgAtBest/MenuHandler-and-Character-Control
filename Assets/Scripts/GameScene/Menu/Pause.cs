using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public bool paused;
    public Canvas pauseMenu;
    public Button resume;
    void Start ()
    {
        Time.timeScale = 1;
        pauseMenu.enabled = false;
    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                pauseMenu.enabled = false;
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                pauseMenu.enabled = true;
                paused = true;
            }
        }
    }
}
