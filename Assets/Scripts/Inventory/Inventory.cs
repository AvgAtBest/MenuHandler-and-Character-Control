using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    #region Variables
    public static List<Item> inv = new List<Item>();//list of items
    public static bool showInv;//show or hide inventory
    public Item selectedItem;//the item that is selected
    public static int money;//currency


    public Vector2 scr = Vector2.zero;//screen width and height 16:9
    public Vector2 scrollPos = Vector2.zero;//scrollbar position for inventory list
    public string sortType = "All";

    //0 = RightHand // Weapon
    //1 = head // Helmet
    public Transform[] equippedLocation;
    public Transform dropLocation;
    public GameObject curWeapon;
    public GameObject curArmour;
    #endregion

    void Start()
    {
        inv.Add(ItemData.CreateItem(0));
        inv.Add(ItemData.CreateItem(2));
        inv.Add(ItemData.CreateItem(100));
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
        //if (Input.GetKey(KeyCode.P))
        //{
        //    //if  inv isnt been shown
        //    if (showInv)
        //    {
        //        //adds and create items with their respective ids
        //        inv.Add(ItemData.CreateItem(2));
        //        inv.Add(ItemData.CreateItem(101));
        //        inv.Add(ItemData.CreateItem(200));
        //        inv.Add(ItemData.CreateItem(202));
        //        inv.Add(ItemData.CreateItem(301));
        //        inv.Add(ItemData.CreateItem(402));
        //    }

        //}
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
                if (GUI.Button(new Rect(5.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "All"))
                {
                    sortType = "All";
                }
                if (GUI.Button(new Rect(6.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Weapons"))
                {
                    sortType = "Weapons";
                }
                if (GUI.Button(new Rect(7.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Armour"))
                {
                    sortType = "Armour";
                }
                if (GUI.Button(new Rect(8.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Craftable"))
                {
                    sortType = "Craftable";
                }
                if (GUI.Button(new Rect(9.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Quest"))
                {
                    sortType = "Quest";
                }
                if (GUI.Button(new Rect(10.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Consumables"))
                {
                    sortType = "Consumables";
                }
                if (GUI.Button(new Rect(11.5f * scr.x, 0.5f * scr.y, scr.x, 0.35f * scr.y), "Misc"))
                {
                    sortType = "Misc";
                }

                DisplayInv(sortType);
                #endregion

                #region Item Data Display
                if (selectedItem != null)
                {
                    GUI.DrawTexture(new Rect(11 * scr.x, 1.5f * scr.y, 2 * scr.x, 2 * scr.y), selectedItem.Icon);
                    if (GUI.Button(new Rect(14 * scr.x, 8.75f * scr.y, scr.x, 0.5f * scr.y), "Discard"))
                    {
                        if (curWeapon != null && selectedItem.MeshName == curWeapon.name)
                        {
                            Destroy(curWeapon);

                        }
                        else if (curArmour != null && selectedItem.MeshName == curArmour.name)
                        {
                            Destroy(curArmour);
                        }
                        GameObject clone = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, dropLocation.position, Quaternion.identity);
                        clone.AddComponent<Rigidbody>().useGravity = true;
                        //dropLocation.transform.SetParent(null);
                        if (selectedItem.Amount > 1)
                        {
                            selectedItem.Amount--;
                        }
                        else
                        {
                            inv.Remove(selectedItem);
                            selectedItem = null;
                        }
                        return;
                    }
                    switch (selectedItem.Type)
                    {
                        case ItemTypes.Armour:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:"
                                + selectedItem.Value + "\nArmour: " + selectedItem.Armour);
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.5f * scr.y), "Equip"))
                            {

                                //attach and wear armour on character
                            }
                            break;
                        case ItemTypes.Consumables:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:"
                                + selectedItem.Value + "\nHeal: " + selectedItem.Heal + "\nAmount: " + selectedItem.Amount);
                            if (CharacterHandler.curHealth < CharacterHandler.maxHealth)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.5f * scr.y), "Use"))
                                {
                                    CharacterHandler.curHealth += selectedItem.Heal;
                                    if (selectedItem.Amount > 1)
                                    {
                                        selectedItem.Amount--;
                                    }
                                    else
                                    {
                                        inv.Remove(selectedItem);
                                        selectedItem = null;
                                    }
                                    return;
                                }
                            }
                            break;
                        case ItemTypes.Craftable:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:"
                                + selectedItem.Value + "\nAmount: " + selectedItem.Amount);
                            if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.5f * scr.y), "Use"))
                            {
                                //craft system a+b = c
                            }
                            break;
                        case ItemTypes.Weapons:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:"
                                + selectedItem.Value + "\nWeapon: " + selectedItem.Damage);
                            if (curWeapon == null || selectedItem.MeshName != curWeapon.name)
                            {
                                if (GUI.Button(new Rect(15 * scr.x, 8.75f * scr.y, scr.x, 0.5f * scr.y), "Equip"))
                                {
                                    if (curWeapon != null)
                                    {
                                        Destroy(curWeapon);
                                    }
                                    //equip weapon
                                    curWeapon = Instantiate(Resources.Load("Prefab/" + selectedItem.MeshName) as GameObject, equippedLocation[0]);
                                    curWeapon.GetComponent<ItemHandler>().enabled = false;
                                    curWeapon.name = selectedItem.MeshName;
                                }
                            }
                            break;
                        case ItemTypes.Quest:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value);
                            break;
                        case ItemTypes.Misc:
                            GUI.Box(new Rect(8 * scr.x, 5 * scr.y, 8 * scr.x, 3 * scr.y), selectedItem.Name + "\n" + selectedItem.Description + "\nValue:" + selectedItem.Value);
                            break;
                    }
                }
                #endregion
            }
        }
    }
    void DisplayInv(string sortType)
    {
        #region Sorting 
        //if not catergory All or nothing
        if (!(sortType == "All" || sortType == ""))
        {
            //uses sort type string to connect to our enum type
            ItemTypes type = (ItemTypes)System.Enum.Parse(typeof(ItemTypes), sortType);

            int a = 0;//amount of that type
            int s = 0;//slot position of ui item

            //loops through each item in category
            for (int i = 0; i < inv.Count; i++)
            {
                //if it finds a item that matches type
                if (inv[i].Type == type)
                {
                    //increase the amount of this type
                    a++;
                }

            }
            //if amount of this type is less than or equal to 17
            if (a <= 17)
            {
                //loops through each item in inventory
                for (int i = 0; i < inv.Count; i++)
                {
                    //if it finds a item that matches that type
                    if (inv[i].Type == type)
                    {
                        //displays a button that that has the items name and also includes the item of type into its category
                        if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + s * (0.5f * scr.y), 3 * scr.x, 0.5f * scr.y), inv[i].Name))
                        {

                            selectedItem = inv[i];//this button is our selected item from our inventory
                            Debug.Log(selectedItem.Name);//displays item name in debug
                        }
                        //once added, increases our slot position
                        s++;
                        //each new item goes under the last item
                    }

                }
            }
            else//if there are more than 17 items of this type
            {
                //removes previous 17 from our type amount 8, creates another scrollbar in tabs
                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.75f * scr.y), scrollPos, new Rect(0, 0, 0, 9f * scr.y + ((a - 17) * (0.5f * scr.y))), false, true);
                for (int i = 0; i < inv.Count; i++)
                {
                    if (inv[i].Type == type)
                    {
                        //displays a button that that has the items name and also includes the item of type into its category
                        if (GUI.Button(new Rect(0.5f * scr.x, 0 * scr.y + s * (0.5f * scr.y), 3 * scr.x, 0.5f * scr.y), inv[i].Name))
                        {
                            selectedItem = inv[i];//button is our selected item from our inventory
                            Debug.Log(selectedItem.Name);//tell us its name
                        }
                        s++;//once added increases our slot position
                        //each new item goes under the last item

                    }
                }
                GUI.EndScrollView();//ends scrolling of elements inside view
            }
        }
        else
        #endregion
        {
            #region Non Scroll Inventory
            //for our inventory without a scroll bar
            //if inventory has less than or is equal to 17 items
            if (inv.Count <= 17)
            {
                //for each item in our inventory list
                for (int i = 0; i < inv.Count; i++)
                {
                    //display a button with the items name, and if that button is clicked
                    if (GUI.Button(new Rect(0.5f * scr.x, 0.25f * scr.y + i * (0.5f * scr.y), 3 * scr.x, 0.5f * scr.y), inv[i].Name))
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
            //for our inventory with a scrollbar that has more than 17 items
            else
            {
                //our moved position in scrolling is now applied to the new created scrollbar
                // creates a new viewable window
                //our current position in the scrolling (go all the way in scr height, adds the inv count and takes away 35 items. Then times the items left by 0.5f * scr height)
                //can we see the horizontal bar (false) and if we can see the vertical bar (true)

                scrollPos = GUI.BeginScrollView(new Rect(0, 0.25f * scr.y, 3.75f * scr.x, 8.75f * scr.y), scrollPos, new Rect(0, 0, 0, 9f * scr.y + ((inv.Count - 17) * (0.5f * scr.y))), false, true);

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
        }

    }
}
