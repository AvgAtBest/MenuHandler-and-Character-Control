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
        //if tab is pressed down
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            //if the pause menu isn't paused
            if (!PausedMenu.paused)
            {
                //turn on inventory menu
                ToggleInv();
            }
        }
        //if P input on keyboard is pushed
        if (Input.GetKey(KeyCode.P))
        {
            //if  inv isnt been shown
            if (showInv)
            {
                //adds and create items with their respective ids
                inv.Add(ItemData.CreateItem(2));
                inv.Add(ItemData.CreateItem(101));
                inv.Add(ItemData.CreateItem(200));
                inv.Add(ItemData.CreateItem(202));
                inv.Add(ItemData.CreateItem(301));
                inv.Add(ItemData.CreateItem(402));
            }

        }
    }
    public bool ToggleInv()
    {
        // if the inventory isnt showing
        if (showInv)
        {
            //then it is false
            showInv = false;
            //time unfreezes in game
            Time.timeScale = 1;
            return (false);
        }
        else//if it is showing
        {
            //inventory is then showing
            showInv = true;
            //time freezes game
            Time.timeScale = 0;
            return (true);
        }
    }

    private void OnGUI()
    {
        //if the paused menu isnt paused
        if (!PausedMenu.paused)
        {
            //if the inventory isnt showing
            if (showInv)
            {
                #region Inv GUI
                //if the screen isnt equal to the width and height of 16:9
                if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
                {
                    //then the UI screen width and height is scaled to 16:9
                    scr.x = Screen.width / 16;
                    scr.y = Screen.height / 9;
                }
                //creates a gui rectangle that serves as a background with inventory text (text set to top centre by default). Background is length of screen width and height
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "Inventory");
                #endregion
                #region Non Scroll Inventory
                //for our inventory without a scroll bar
                //if inventory has less than or is equal to 35 items
                if (inv.Count <= 35)
                {
                    //for each item in our inventory list
                    for (int i = 0; i < inv.Count; i++)
                    {
                        //display a button with the items name, and if that button is clicked
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.5f * scr.y + i * (0.5f * scr.y), 3 * scr.x, 0.5f * scr.y), inv[i].Name))
                        {
                            //the selected item is in inventory
                            selectedItem = inv[i];
                            //brings up the selected item name
                            Debug.Log(selectedItem.Name);
                        }
                    }
                }
                #endregion
                #region Scroll Inv
                //for our inventory with a scrollbar
                else//if inventory has more than 35 items
                {

                    //our moved position in scrolling is now applied to the new created scrollbar  
                    scrollPos = GUI.BeginScrollView
                        // creates a new viewable window 
                        (new Rect(0, 0.25f * scr.y, 6 * scr.x, 8.75f * scr.y),
                        //our current position in the scrolling (go all the way in scr height, adds the inv count and takes away 35 items. Then times the items left by 0.5f * scr height)
                        scrollPos, new Rect(0, 0, 0, 9f * scr.y + ((inv.Count - 35) * (0.5f * scr.y))),
                        //can we see the horizontal bar (false) and if we can see the vertical bar (true)
                        false, true);
                    #region Items in viewing Area
                    for (int i = 0; i < inv.Count; i++)
                    {
                        //display a button with the items name. Buttons start as 0.5f height, and if there is more than 35 items when scrolling, it will move the buttons in list to the top of the screen
                        if (GUI.Button(new Rect(0.5f * scr.x, 0f * scr.y + i * (0.5f * scr.y), 3 * scr.x, 0.5f * scr.y), inv[i].Name))
                        {
                            //the selected item is in inventory
                            selectedItem = inv[i];
                            //brings up the selected item name
                            Debug.Log(selectedItem.Name);
                        }
                    }
                    #endregion
                    GUI.EndScrollView();

                }
                #endregion

                if (selectedItem != null)
                {
                    GUI.DrawTexture(new Rect(11 * scr.x, 1.5f * scr.y, 2 * scr.x, 2 * scr.y), selectedItem.Icon);
                    if (selectedItem.Type != ItemTypes.Quest)
                    {
                        if (GUI.Button(new Rect(14 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Discard"))
                        {
                            inv.Remove(selectedItem);
                            selectedItem = null;
                            return;
                        }
                    }
                    switch (selectedItem.Type)
                    {
                        case ItemTypes.Armour:
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                            {
                                //attach and wear armour on character
                            }
                            break;
                        case ItemTypes.Cosumables:
                            if (CharacterHandler.curHealth < CharacterHandler.maxHealth)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                                {
                                    CharacterHandler.curHealth += selectedItem.Heal;
                                    inv.Remove(selectedItem);
                                    selectedItem = null;
                                    return;
                                }
                            }
                            break;
                        case ItemTypes.Craftable:
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Use"))
                            {
                                //craft system a+b = c
                            }
                            break;
                        case ItemTypes.Weapon:
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.25f * scr.y), "Equip"))
                            {
                                //equip weapon
                            }
                            break;
                        case ItemTypes.Quest:
                            break;
                        case ItemTypes.Misc:
                            break;
                    }
                }
            }
        }
    }
}