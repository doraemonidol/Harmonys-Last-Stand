// Item.cs
using UnityEngine;

[System.Serializable]
public struct Item
{
    public int money;
    public string detail;
    public Sprite image;
    public ItemType itemType;
    public string name; 

    public Item(int money, string detail, Sprite image, ItemType itemType, string name)
    {
        this.name = name;
        this.money = money;
        this.detail = detail;
        this.image = image;
        this.itemType = itemType;
    }
}