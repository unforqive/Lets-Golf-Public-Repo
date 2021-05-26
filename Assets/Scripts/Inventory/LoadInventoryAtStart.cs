using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoadInventoryAtStart : MonoBehaviour
{
    public InventoryObject inventory;
    public Shop shop;
    void Awake()
    {
        inventory.LoadItems();
        shop.UpdateShopList();
    }
}
