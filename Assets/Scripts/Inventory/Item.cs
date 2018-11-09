using UnityEngine;

public class Item
{
    #region Item values
    private string _name;
    private int _id;
    private int _value;
    private string _description;
    private int _damage;
    private int _armour;
    private int _amount;
    private int _heal;
    private Texture2D _icon;
    private string _mesh;
    private ItemTypes _type;
    #endregion

    public Item()
    {
        _id = 0;
        _name = "Unknown";
        _description = "Empty";
        _value = 0;
        _damage = 0;
        _armour = 0;
        _amount = 0;
        _heal = 0;
        _mesh = "MeshName";
        _type = ItemTypes.Quest;

    }
    public Item(int id, string name, string description, int value, int damage, int armour, int amount, int heal, string meshName, ItemTypes type)
    {
        _id = id;
        _name = name;
        _description = description;
        _value = value;
        _damage = damage;
        _armour = armour;
        _amount = amount;
        _mesh = meshName;
        _heal = heal;
        _type = type;

    }

    #region Properties
    #region Name
    public string Name
    {
        get { return _name; }
        set { _name = value; }

    }
    #endregion
    #region ID
    public int Id
    {
        get { return _id; }
        set { _id = value; }
    }
    #endregion
    #region Value
    public int Value
    {
        get { return _value; }
        set { _value = value; }
    }
    #endregion
    #region Description
    public string Description
    {
        get { return _description; }
        set { _description = value; }
    }
    #endregion
    #region Damage
    public int Damage
    {
        get { return _damage; }
        set { _damage = value; }
    }
    #endregion
    #region Armour
    public int Armour
    {
        get { return _armour; }
        set { _armour = value; }
    }
    #endregion
    #region Heal
    public int Heal
    {
        get { return _heal; }
        set { _heal = value; }
    }
    #endregion
    #region Amount
    public int Amount
    {
        get { return _amount; }
        set { _amount = value; }
    }
    #endregion
    #region Mesh
    public string MeshName
    {
        get { return _mesh; }
        set { _mesh = value; }
    }
    #endregion
    #region Icon
    public Texture2D Icon
    {
        get { return _icon; }
        set { _icon = value; }
    }
    #endregion
    #region Type
    public ItemTypes Type
    {
        get { return _type; }
        set { _type = value; }
    }
    #endregion
    #endregion
}
public enum ItemTypes
{
    Consumables,
    Armour,
    Weapons,
    Craftable,
    Money,
    Quest,
    Misc
}