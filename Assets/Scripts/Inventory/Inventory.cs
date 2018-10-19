using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    public List<Item> inv = new List<Item>();//list of items
    public static bool showInv;//show or hide inventory
    public Item selectedItem;//the item that is selected
    public int money;//currency

    public Vector2 scr = Vector2.zero;//screen width and height 16:9
    public Vector2 scrollPos = Vector2.zero;//scrollbar position for inventory list

    #endregion

    void Start()
    {
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(101));
        inv.Add(ItemData.CreateItem(200));
        inv.Add(ItemData.CreateItem(202));
        inv.Add(ItemData.CreateItem(301));
        inv.Add(ItemData.CreateItem(402));
        //creates a item corrosponding to its case number(ID number) in the ItemData Switch
        for (int i = 0; i < inv.Count; i++)
        {
            Debug.Log(inv[i].Name);
        }
    }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (!PausedMenu.paused)
            {
                ToggleInv();
            }
        }
    }
    public bool ToggleInv()
    {
        if (showInv)
        {
            showInv = false;
            Time.timeScale = 1;
            return (false);
        }
        else
        {
            showInv = true;
            Time.timeScale = 0;
            return (true);
        }
    }
    private void OnGUI()
    {
        if (!PausedMenu.paused)
        {
            if (showInv)
            {
                if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
                {
                    scr.x = Screen.width / 16;
                    scr.y = Screen.height / 9;
                }
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                #region Non Scroll Inventory
                if (inv.Count <= 35)
                {
                    for (int i = 0; i < inv.Count; i++)
                    {
                        if(GUI.Button(new Rect(0.5f*scr.x,0.25f*scr.y + i*(0.25f * scr.y),3*scr.x,0.25f*scr.y),inv[i].Name))
                        {
                            selectedItem = inv[i];
                            Debug.Log(selectedItem.Name);
                        }
                    }
                }
                #endregion
                #region Scroll Inv

                #endregion
            }

        }
    }
}