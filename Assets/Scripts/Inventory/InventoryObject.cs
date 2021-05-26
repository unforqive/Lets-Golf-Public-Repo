using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class InventoryObject : MonoBehaviour
{
    public List<Shop.ShopItem> InventoryItemsList;
    public Sprite ChestImage;

    [Header("Inventory UI")]
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform InventoryScrollView;
    [SerializeField] Shop shop;
    [SerializeField] Button ItemButton;
    [SerializeField] LootBox lootBox;
    public int inventoryInt;
    private GameObject h;

    private GameObject selectedBox;

    public void LoadItems()
    {
        InventoryItemsList = PlayerPrefsExtra.GetList<Shop.ShopItem>("Inventory List", new List<Shop.ShopItem>());

        for (int i = 0; i < InventoryItemsList.Count; i++)
        {
            if (InventoryItemsList[i].IsPurchased)
            {
                h = Instantiate(ItemTemplate, InventoryScrollView);
                h.SetActive(true);

                h.transform.GetChild(0).GetComponent<Image>().sprite = InventoryItemsList[i].QualityImage;
                h.transform.GetChild(1).GetComponent<Image>().sprite = InventoryItemsList[i].ItemImage;
                InventoryItemsList[i].thisItem = h;
            }
        }
    }

    public void UpdateList(int itemInt)
    {
        InventoryItemsList.Add(shop.ShopItemsList[itemInt]);

        int len = InventoryItemsList.Count;

        g = Instantiate(ItemTemplate, InventoryScrollView);

        for (itemInt = 0; itemInt < len; itemInt++)
        {
            //Save list
            PlayerPrefsExtra.SetList("Inventory List", InventoryItemsList);

            g.SetActive(true);

            g.transform.GetChild(0).GetComponent<Image>().sprite = InventoryItemsList[itemInt].QualityImage;
            g.transform.GetChild(1).GetComponent<Image>().sprite = InventoryItemsList[itemInt].ItemImage;
            InventoryItemsList[itemInt].thisItem = g;
        }
    }

    public void OnItemClicked()
    {
        if (EventSystem.current.currentSelectedGameObject.transform.GetChild(0).GetComponent<Image>().sprite == ChestImage)
        {
            int i;
            lootBox.ShowLootBoxScene();
            selectedBox = EventSystem.current.currentSelectedGameObject;

            for (i = 0; i < InventoryItemsList.Count; i++)
            {
                if (selectedBox == InventoryItemsList[i].thisItem)
                {
                    InventoryItemsList.Remove(InventoryItemsList[i]);

                    //Save list
                    PlayerPrefsExtra.SetList("Inventory List", InventoryItemsList);
                }
            }
            StartCoroutine(WaitForSceneToLoad());
        }
    }

    IEnumerator WaitForSceneToLoad()
    {
        yield return new WaitForSeconds(0.8f);
        Destroy(selectedBox);
    }
}

