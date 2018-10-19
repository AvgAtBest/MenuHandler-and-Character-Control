using UnityEngine;

public static class ItemData
{
    public static Item CreateItem(int ItemID)
    {
        string name = "";
        int value = 0;
        string description = "";
        int damage = 0;
        int armour = 0;
        int amount = 0;
        int heal = 0;
        string icon = "";
        string meshName = "";
        ItemTypes type = ItemTypes.Armour;



        Item temp = new Item
        {
            Name = name,
            Id = ItemID,
            Value = value,
            Description = description,
            Damage = damage,
            Armour = armour,
            Amount = amount,
            Heal = heal,
            Type = type,
            Icon = Resources.Load("Icon/" + icon) as Texture2D,
            Mesh = meshName
        };
        return temp;


    }

}
