using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDropInv : MonoBehaviour
{
    #region Variables
    [Header("Inventory")]
    public bool showInv; // toggles ui
    public static List<Item> inventory = new List<Item>(); // list of items
    public int slotX, slotY;//size x and y of slots
    private Rect invSize;

    [Header("Dragging")]
    public bool isDragging;//are we dragging a item
    public Item draggedItem;
    public int draggedFrom;
    public GameObject droppedItem;

    [Header("Tool Tip")]
    public int toolTipItem;//index reference
    public bool showToolTip;
    private Rect toolTipRect;
    [Header("Other References")]
    private Vector2 scr;//screen
    #endregion

    #region Clamp to Screen
    private Rect ClampToScreen(Rect r)
    {
        r.x = Mathf.Clamp(r.x, 0, Screen.width - r.width);
        r.y = Mathf.Clamp(r.y, 0, Screen.height - r.height);
        return r;
    }
    #endregion
    #region Add Item
    public static void AddItem(int itemID)
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            if (inventory[i].Name == null)
            {
                inventory[i] = ItemData.CreateItem(itemID);
                Debug.Log("Add Item:" + inventory[i].Name);
                return;
            }
        }
    }
    #endregion
    #region Drop Item
    public void DropItem(int itemID)
    {
        //gets the prefab of the item that wants to be dropped
        droppedItem = Resources.Load("Prefab/" + ItemData.CreateItem(itemID).MeshName) as GameObject;
        //spawns the item into the world, and remembers what item that was dropped
        droppedItem = Instantiate(droppedItem, transform.position + transform.forward * 3, Quaternion.identity);
        //adding rigidbody
        droppedItem.AddComponent<Rigidbody>().useGravity = true;
        //empty the dropped item
        droppedItem = null;
    }
    #endregion
    #region Draw Item
    void DrawItem(int windowID)
    {
        if (draggedItem.Icon != null)
        {
            GUI.DrawTexture(new Rect(0, 0, scr.x * 0.5f, scr.y * 0.5f), draggedItem.Icon);
        }
    }
    #endregion
    #region Tools
    #region ToolTipContent
    private string ToolTipText(int itemID)
    {
        string toolTipText = "Name:" + inventory[itemID].Name + "Description: " + inventory[itemID].Description
            + "Type: " + inventory[itemID].Type + "Value: " + inventory[itemID].Value;

        return toolTipText;
    }
    #endregion
    #region ToolTipWindow
    void DrawToolTip(int windowID)
    {
        GUI.Box(new Rect(0, 0, scr.x * 2, scr.y * 3), ToolTipText(toolTipItem));
    }
    #endregion
    #endregion
    #region ToggleInventory
    public bool ToggleInv()
    {
        // if the inventory isnt showing
        if (showInv)
        {
            //then it is false
            showInv = false;
            //time unfreezes in game
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 1;
            return (false);
        }
        else//if it is showing
        {
            //inventory is then showing
            showInv = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            //time freezes game
            Time.timeScale = 0;
            return (true);
        }
    }
    #endregion
    #region Drag Inv
    void InventoryDrag(int windowID)
    {

        GUI.Box(new Rect(0, scr.y * 0.25f, scr.x * 6, scr.y * 0.5f), "Banner");
        GUI.Box(new Rect(0, scr.y * 4.25f, scr.x * 6, scr.y * 0.5f), "Gold and EXP");
        showToolTip = false;
        #region Nested for Loop
        Event e = Event.current;
        int i = 0;
        for (int y = 0; y < slotY; y++)
        {
            for (int x = 0; x < slotX; x++)
            {
                Rect slotLocation = new Rect(scr.x * 0.125f + x * (scr.x * 0.75f), scr.y * 0.75f + y * (scr.y * 0.65f), scr.x * 0.75f, scr.y * 0.65f);
                GUI.Box(slotLocation, "");
                #region Pickup Item
                //if we are interaction with left mouse button and the interaction is clicked down
                //and mouse cursor is over a item slot while we are not already holding and item slot isnt empty
                //as well as aren't hitting the change inv key with Left Shift
                if (e.button == 0 && e.type == EventType.MouseDown && slotLocation.Contains(e.mousePosition) && !isDragging && inventory[i].Name != null && !Input.GetKey(KeyCode.LeftShift))
                {
                    //we pick up the item
                    draggedItem = inventory[i];
                    //inventory slot is now empty
                    inventory[i] = new Item();
                    //we are holding a item
                    isDragging = true;
                    //we remember where this item come from
                    draggedFrom = i;
                    //Debug
                    Debug.Log("Dragging: " + draggedItem.Name);
                }
                #endregion
                #region Swap Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inventory[i].Name != null)
                {
                    Debug.Log("Swapping:" + draggedItem.Name + "With: " + inventory[i].Name);
                    //the slot that is full now moves to where our dragged item came from
                    inventory[draggedFrom] = inventory[i];
                    //the slot we are drooping into is now filled with our dragged item
                    inventory[i] = draggedItem;
                    //the dragged item is now empty
                    draggedItem = new Item();
                    //we are no longer dragging
                    isDragging = false;

                }
                #endregion
                #region Place Item
                if (e.button == 0 && e.type == EventType.MouseUp && slotLocation.Contains(e.mousePosition) && isDragging && inventory[i].Name == null)
                {
                    Debug.Log("Place:" + draggedItem.Name + "Into:" + i);
                    //slot we are dropping the item into is now iflled with the dragged item
                    inventory[i] = draggedItem;
                    //the item we used to drag is now empty
                    draggedItem = new Item();
                    //we are no longer holding a item
                    isDragging = false;
                }
                #endregion
                #region Return Item
                if (e.button == 0 && e.type == EventType.MouseUp && i == ((slotX * slotY) - 1) && isDragging)
                {
                    //put the item back to where it came from
                    inventory[draggedFrom] = draggedItem;
                    //the dragged item is now empty
                    draggedItem = new Item();
                    //we are no longer dragging
                    isDragging = false;
                }
                #endregion
                #region Draw Item Icon
                if (inventory[i].Name != null)
                {
                    GUI.DrawTexture(slotLocation, inventory[i].Icon);
                    #region Set tooltip on Mouse Hover
                    if (slotLocation.Contains(e.mousePosition) && !isDragging && showInv)
                    {
                        toolTipItem = i;
                        showToolTip = true;
                    }
                    #endregion
                }
                #endregion
                i++;
            }
        }
        #endregion
        #region Drag Points
        //Top drag
        GUI.DragWindow(new Rect(0, 0, scr.x * 6, scr.y * 0.5f));
        //Left Drag
        GUI.DragWindow(new Rect(0, scr.y * 0.5f, scr.x * 0.25f, scr.y * 3.5f));
        //Right Drag
        GUI.DragWindow(new Rect(scr.x * 5.75f, scr.y * 0.5f, scr.x * 0.25f, scr.y * 3.5f));
        //Bottom Drag
        GUI.DragWindow(new Rect(0, scr.y * 4, scr.x * 6, scr.y * 0.5f));
        #endregion
    }
    #endregion
    #region Start
    private void Start()
    {
        scr.x = Screen.width / 16;
        scr.y = Screen.height / 9;
        invSize = new Rect(scr.x, scr.y, 6 * scr.x, 4.5f * scr.y);
        for (int i = 0; i < (slotX* slotY); i++)
        {
            inventory.Add(new Item());
        }
        AddItem(0);
        AddItem(2);
        AddItem(100);
        AddItem(101);
        AddItem(200);
        AddItem(202);
        AddItem(301);
        AddItem(402);
    }
    #endregion
    #region Update
    private void Update()
    {
        if (scr.x != Screen.width / 16 || scr.y != Screen.height / 9)
        {
            scr.x = Screen.width / 16;
            scr.y = Screen.height / 9;
        }
        if (Input.GetKeyDown(KeyCode.I))
        {
            ToggleInv();
        }
    }
    #endregion

    #region ONGUI
    private void OnGUI()
    {
        Event e = Event.current;
        #region Inventory if showInv is true
        if (showInv)
        {
            invSize = ClampToScreen(GUI.Window(1, invSize, InventoryDrag, "Drag Inventory"));
        }
        #endregion
        #region tooltip
        if (showToolTip && showInv)
        {
            toolTipRect = new Rect(e.mousePosition.x + 0.01f, e.mousePosition.y + 0.001f, scr.x * 2, scr.y * 3);
            GUI.Window(15, toolTipRect, DrawToolTip, "");
        }
        #endregion
        #region Drop Item (mouseup)
        if ((e.button == 0 && e.type == EventType.MouseUp && isDragging) || (isDragging && !showInv))
        {
            DropItem(draggedItem.Id);
            Debug.Log("Dropped: " + draggedItem.Name);
            draggedItem = new Item();
            isDragging = false;
        }
        #endregion
        #region Draw item on mouse
        if (isDragging)
        {
            if(draggedItem != null)
            {
                Rect mouseLocation = new Rect(e.mousePosition.x + 0.125f, e.mousePosition.y + 0.125f, scr.x * 0.5f, scr.y * 0.5f);
                GUI.Window(2, mouseLocation, DrawItem, "");
                //mouseLocation = ClampToScreen(GUI.Window(2, mouseLocation, DrawItem, ""));
            }
        }
        #endregion
    }
    #endregion

}