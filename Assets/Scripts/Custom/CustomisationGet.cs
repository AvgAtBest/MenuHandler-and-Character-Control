using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomisationGet : MonoBehaviour
{
    [Header("Character")]
    public Renderer charMesh;
    public CharacterHandler charH;

    //public variable for the Skinned Mesh Renderer which is our character reference

    private void Start()
    {
        charMesh = GameObject.Find("Mesh").GetComponent<SkinnedMeshRenderer>();
        //our character reference connected to the Skinned Mesh Renderer via finding the Mesh
        LoadTexture();
        //Run the function LoadTexture
    }
    void LoadTexture()
    {

        if (!PlayerPrefs.HasKey("CharacterName"))
        {
            SceneManager.LoadScene(1);

        }
        SetTexture("Skin", PlayerPrefs.GetInt("SkinIndex"));
        SetTexture("Hair", PlayerPrefs.GetInt("HairIndex"));
        SetTexture("Mouth", PlayerPrefs.GetInt("MouthIndex"));
        SetTexture("Eyes", PlayerPrefs.GetInt("EyesIndex"));
        SetTexture("Armour", PlayerPrefs.GetInt("ArmourIndex"));
        SetTexture("Clothes", PlayerPrefs.GetInt("ClothesIndex"));
        gameObject.name = PlayerPrefs.GetString("CharacterName");
        
        
    }
    #region SetTexture Func
    //Create a function that is called SetTexture it should contain a string and int
    //the string is the name of the material we are editing, the int is the direction we are changing
    void SetTexture(string type, int dir)
    {

        Texture2D tex = null;
        int matIndex = 0;
        //we need variables that exist only within this function
        //these are int material index and Texture2D array of textures
        //inside a switch statement that is swapped by the string name of our material

        switch (type)
        {
            //case skin
            //textures is our Resource.Load Character Skin save index we loaded in set as our Texture2D
            //material index element number is 1
            //break
            //now repeat for each material 
            //hair is 2
            //mouth is 3
            //eyes are 4
            case "Skin":
                tex = Resources.Load("Character/Skin_" + dir.ToString()) as Texture2D;
                matIndex = 1;
                break;
            case "Hair":
                tex = Resources.Load("Character/Hair_" + dir.ToString()) as Texture2D;
                matIndex = 4;
                break;
            case "Mouth":
                tex = Resources.Load("Character/Mouth_" + dir.ToString()) as Texture2D;
                matIndex = 2;
                break;
            case "Eyes":
                tex = Resources.Load("Character/Eyes_" + dir.ToString()) as Texture2D;
                matIndex = 3;
                break;
            case "Armour":
                tex = Resources.Load("Character/Armour_" + dir.ToString()) as Texture2D;
                matIndex = 5;
                break;
            case "Clothes":
                tex = Resources.Load("Character/Clothes_" + dir.ToString()) as Texture2D;
                matIndex = 6;
                break;

        }
        Material[] mats = charMesh.materials;
        mats[matIndex].mainTexture = tex;
        charMesh.materials = mats;

        //Material array is equal to our characters material list
        //our material arrays current material index's main texture is equal to our texture arrays current index
        //our characters materials are equal to the material array
        #endregion 
    }

}