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

        switch (ItemID)
        {
            #region Consumables 0 - 99
            case 0:
                name = "Apple";
                value = 5;
                description = "It's a fucking Apple.";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 10;
                icon = "apple";
                meshName = "Apple_Mesh";
                type = ItemTypes.Cosumables;
                break;
            case 1:
                name = "Cheese";
                value = 5;
                description = "CHEESE IT.";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 10;
                icon = "I_C_Cheese";
                meshName = "Cheese_Mesh";
                type = ItemTypes.Cosumables;
                break;
            case 2:
                name = "Health Vial";
                value = 50;
                description = "Sponsored by coca cola";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 75;
                icon = "hp";
                meshName = "HP_Mesh";
                type = ItemTypes.Cosumables;
                break;
            #endregion
            #region Armour 100-199
            case 100:
                name = "Anti Stab Vest";
                value = 150;
                description = "Great against melee attacks, not so much against bullets. Don't bring a padded jacket to a gunfight.";
                damage = 0;
                armour = 100;
                amount = 1;
                heal = 0;
                icon = "armor";
                meshName = "SPV_Mesh";
                type = ItemTypes.Armour;
                break;
            case 101:
                name = "Cinderblock Armour";
                value = 100;
                description = "Becoming resourceful in a apocalypse is a good thing. Strapping cinderblocks to yourself as a form of armour is not a sign of resourcefulness.";
                damage = 0;
                armour = 250;
                amount = 1;
                heal = 0;
                icon = "A_Armour02";
                meshName = "Cinder_Armour_Mesh";
                type = ItemTypes.Armour;
                break;
            case 102:
                name = "BulletProof Armour";
                value = 300;
                description = "Misleading product. Should be named Bullet Resistant, not Bullet Proof. Do not buy.";
                damage = 0;
                armour = 350;
                amount = 1;
                heal = 0;
                icon = "A_Armour01";
                meshName = "BPArmour_Mesh";
                type = ItemTypes.Armour;
                break;
            #endregion
            #region Weapons 200 - 299
            case 200:
                name = "Freedom";
                value = 200;
                description = "Contrary to popular belief, the Freedom pistol is not free. The cost for its development skyrocketed into the millions, ultimately resulting in company closure.";
                damage = 150;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "W_Gun003";
                meshName = "Freedom_Mesh";
                type = ItemTypes.Weapon;
                break;
            case 201:
                name = "Spoon Knife";
                value = 50;
                description = "I see you have played knifey spoony before.";
                damage = 5;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "sword";
                meshName = "Sword_Mesh";
                type = ItemTypes.Weapon;
                break;
            case 202:
                name = "Axe";
                value = 125;
                description = "Honestly. Why have anyother type of melee weapon in a zombie apocalypse when you have a axe?";
                damage = 25;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "axe";
                meshName = "Axe_Mesh";
                type = ItemTypes.Weapon;
                break;
            #endregion
            #region Craftables 300 - 399
            case 300:
                name = "Stone";
                value = 2;
                description = "Used to craft rudimentary weapons. Or you can sharpen another stone to make a sharp stone.";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "I_Rock01";
                meshName = "Stone_Mesh";
                type = ItemTypes.Craftable;
                break;
            case 301:
                name = "Duct Tape";
                value = 10;
                description = "Why have one gun when you can duct tape two together?";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "I_Fabric";
                meshName = "DuctTape_Mesh";
                type = ItemTypes.Craftable;
                break;
            case 302:
                name = "Glue";
                value = 3;
                description = "Lemon Flavoured! (Do not eat).";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "P_White03";
                meshName = "Glue_Mesh";
                type = ItemTypes.Craftable;
                break;
            #endregion
            #region Misc 400 - 499
            case 400:
                name = "Mirror";
                value = 2;
                description = "Disgusting";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "I_Mirror";
                meshName = "Mirror_Mesh";
                type = ItemTypes.Misc;
                break;
            case 401:
                name = "Book";
                value = 2;
                description = "NEEEEERRRRRDDDDD.";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "I_Book";
                meshName = "Book_Mesh";
                type = ItemTypes.Misc;
                break;
            case 402:
                name = "Scroll";
                value = 2;
                description = "Barely readable.";
                damage = 0;
                armour = 0;
                amount = 1;
                heal = 0;
                icon = "scroll";
                meshName = "Scroll_Mesh";
                type = ItemTypes.Misc;
                break;
                #endregion
        }

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
            Icon = Resources.Load("Icon/" + icon) as Texture2D,
            Mesh = meshName,
            Type = type,
        };
        return temp;


    }

}
