using UnityEngine;
using UnityEngine.UI;

public class Item : ScriptableObject
{
    public string itemID;
    public string itemName;
    public Image itemImage;
    public string itemQuality;

    public Color itemColour;
    public GameObject itemObject;
    public float rarity;
}
