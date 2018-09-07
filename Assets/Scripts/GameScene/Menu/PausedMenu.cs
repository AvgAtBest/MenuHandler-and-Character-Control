using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class PausedMenu : MonoBehaviour
{
    #region Variables
    [Header("Options")]
    public bool showOptions;
    public Vector2[] res = new Vector2[7];
    public int resIndex;
    public bool isFullScreen;
    [Header("Keys")]
    public KeyCode holdingKey;
    public KeyCode forward, backward, left, right, jump, crouch, sprint, interact;

    [Header("References")]
    public GameObject mainMenu;
    public GameObject optionsMenu;
    public GameObject resume;
    //public GameObject playerUI;
    public bool paused;
    //public GUIStyle healthBar;
    //public GUIStyle healthColor;
    public Slider volumeSlider, brightnessSlider, ambientSlider;
    public AudioSource mainAudio;
    public Light dirLight;
    public Dropdown resolutionDropdown;
    public static Resolution[] resolutions;
    [Header("KeyBind References")]
    public Text forwardText;
    public Text backwardText, leftText, rightText, jumpText, crouchText, sprintText, interactText;
    #endregion

    void Start()
    {
        Time.timeScale = 1;
        mainMenu.SetActive(false);
        paused = false;
        //playerUI.SetActive(true);
        mainAudio = GameObject.FindGameObjectWithTag("MenuMusic").GetComponent<AudioSource>();
        dirLight = GameObject.FindGameObjectWithTag("DirectionalLight").GetComponent<Light>();
        #region Set Up Keys
        //set up keys to the present keys that may be saved, else set the keys to default
        forward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Forward", "W"));
        backward = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Backward", "S"));
        left = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Left", "A"));
        right = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Right", "D"));
        jump = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Jump", "Space"));
        crouch = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Crouch", "C"));
        sprint = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Sprint", "LeftShift"));
        interact = (KeyCode)System.Enum.Parse(typeof(KeyCode), PlayerPrefs.GetString("Interact", "E"));
        #endregion
    }
    public void LoadGame()
    {
        //SceneManager.LoadScene(1);
    }
    public void ExitGame()
    {
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        //Will only exit if in Unity Editor
        #endif
        Application.Quit();
        //Exit built application
    }

    public void ToggleOptions()
    {
        OptionToggle();

    }
    bool OptionToggle()
    {
        if (showOptions)//showOptions == true or showOptions is true
        {
            showOptions = false;
            //Set to not display Options Menu as it is not actived
            mainMenu.SetActive(true);
            //Show Main Menu as Options is not being viewed
            optionsMenu.SetActive(false);
            return true;
            //
        }
        else
        {
            showOptions = true;
            mainMenu.SetActive(false);
            optionsMenu.SetActive(true);
            volumeSlider = GameObject.FindGameObjectWithTag("VolumeSlider").GetComponent<Slider>();

            volumeSlider.value = mainAudio.volume;

            brightnessSlider = GameObject.FindGameObjectWithTag("BrightnessSlider").GetComponent<Slider>();

            brightnessSlider.value = dirLight.intensity;

            ambientSlider = GameObject.FindGameObjectWithTag("AmbientSlider").GetComponent<Slider>();

            ambientSlider.value = RenderSettings.ambientIntensity;

            resolutionDropdown = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
            return false;
            // Resolution[] resolutions = Screen.resolutions;
            //foreach (Resolution res in resolutions)
            //  {
            //print(res.width + "x" + res.height);
            // }
            // Screen.SetResolution(resolutions[].width, resolutions[].height, true);

        }
    }

    public void Volume()
    {
        mainAudio.volume = volumeSlider.value;
    }

    public void Brightness()
    {
        dirLight.intensity = brightnessSlider.value;

    }

    public void Ambient()
    {
        RenderSettings.ambientIntensity = ambientSlider.value;
    }

    public void Resolution()
    {
        resIndex = resolutionDropdown.value;
        Screen.SetResolution((int)res[resIndex].x, (int)res[resIndex].y, isFullScreen);
    }
    public void Save()
    {
        PlayerPrefs.SetString("Forward", forward.ToString());
        PlayerPrefs.SetString("Backward", backward.ToString());
        PlayerPrefs.SetString("Left", left.ToString());
        PlayerPrefs.SetString("Right", right.ToString());
        PlayerPrefs.SetString("Jump", jump.ToString());
        PlayerPrefs.SetString("Crouch", crouch.ToString());
        PlayerPrefs.SetString("Sprint", sprint.ToString());
        PlayerPrefs.SetString("Interact", interact.ToString());

    }
    private void OnGUI()
    {
        Event e = Event.current;
        if (forward == KeyCode.None)
        {
            Debug.Log("KeyCode: " + e.keyCode);
            if (!(e.keyCode == backward || e.keyCode == left || e.keyCode == right || e.keyCode == jump || e.keyCode == crouch || e.keyCode == sprint || e.keyCode == interact))
            {
                forward = e.keyCode;
                holdingKey = KeyCode.None;
                forwardText.text = forward.ToString();
            }
        }
    }
    public void Forward()
    {
        if (!(backward == KeyCode.None || left == KeyCode.None || right == KeyCode.None || jump == KeyCode.None || crouch == KeyCode.None || sprint == KeyCode.None || interact == KeyCode.None))
        {
            holdingKey = forward;

            Debug.Log(holdingKey);
            Debug.Log(forward);

            forward = KeyCode.None;

            Debug.Log(holdingKey);
            Debug.Log(forward);

            forwardText.text = forward.ToString();
        }
    }
    public void Resume()
    {
        paused = false;
        mainMenu.SetActive(false);
        Time.timeScale = 1;
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (paused)
            {
                Time.timeScale = 1;
                mainMenu.SetActive(false);
                //playerUI.SetActive(true);
                paused = false;
            }
            else
            {
                Time.timeScale = 0;
                mainMenu.SetActive(true);
                //playerUI.SetActive(false);
                paused = true;
            }
        }
    }
}
