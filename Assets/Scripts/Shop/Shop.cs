using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class Shop : MonoBehaviour
{
    #region Singleton:Shop

    public static Shop Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    #endregion

    [System.Serializable]
    public class ShopItem
    {
        public Sprite QualityImage;
        public Sprite QualityPlace;
        public string ItemType;
        public string ItemName;
        public Sprite ItemImage;
        public Sprite currency;
        public Color itemColor;
        public GameObject thisItem;
        public int Price;
        public int GemValue;
        public bool IsPurchased = false;
    }

    public List<ShopItem> ShopItemsList;

    [Header("Shop UI")]
    [SerializeField] GameObject ItemTemplate;
    GameObject g;
    [SerializeField] Transform ShopScrollView;
    Button PreviewButton;

    [Space]
    [Header("Preview UI")]
    [SerializeField] GameObject PreviewHolder;
    [SerializeField] GameObject PreviewItemQuality;
    [SerializeField] TMP_Text PreviewItemName;

    [SerializeField] TMP_Text PreviewItemType;
    [SerializeField] GameObject PreviewItem;

    [SerializeField] GameObject PreviewValueCoins;
    [SerializeField] GameObject PreviewValueGems;
    public GameObject MultiBuy;
    [SerializeField] TMP_Text PreviewValue;
    [SerializeField] TMP_Text PreviewGemsValue;
    [SerializeField] GameObject ChestContents;
    [SerializeField] GameObject ContentsHolder;

    public Multibuy multiBuy;

    [Space]
    [Header("Confirm UI")]
    [SerializeField] GameObject CoinConfirm;
    [SerializeField] GameObject GemConfirm;

    [SerializeField] TMP_Text ConfirmValue;
    [SerializeField] TMP_Text ConfirmGemValue;

    [SerializeField] TMP_Text ConfirmItemName;
    [SerializeField] GameObject ConfirmItemQuality;
    [SerializeField] GameObject ConfirmItemImage;
    [SerializeField] GameObject CloseButton;


    [SerializeField] Animator ConfirmationAnimation;
    [SerializeField] Animator InvalidCoinAnimation;
    [SerializeField] Animator InvalidGemAnimation;

    [Space]
    [Header("Inventory UI")]
    [SerializeField] InventoryObject inventory;

    public GameObject InvalidCoins;
    public GameObject InvalidGems;
    public GameObject PurchaseSuccessful;

    Button CoinConfirmButton;
    Button GemConfirmButton;

    private bool showGemValue;

    public ConfirmPurchase confirmPurchase;

    public int itemInt;

    private bool startInTimer;
    private int inTimer;

    public AudioHandler audioHandler;

    private int u;

    void Start()
    {
        InvalidCoins.SetActive(false);
        InvalidGems.SetActive(false);
        PurchaseSuccessful.SetActive(false);

        PreviewValueGems.SetActive(false);
        showGemValue = false;
        ChestContents.SetActive(false);
        ContentsHolder.SetActive(false);
    }

    public void UpdateShopList()
    {
        int len = ShopItemsList.Count;

        for (int i = 0; i < len; i++)
        {
            g = Instantiate(ItemTemplate, ShopScrollView);

            g.transform.GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].QualityImage;
            g.transform.GetChild(1).GetComponent<Image>().sprite = ShopItemsList[i].QualityPlace;
            g.transform.GetChild(1).GetChild(0).GetComponent<Image>().sprite = ShopItemsList[i].currency;
            g.transform.GetChild(2).GetComponent<Image>().sprite = ShopItemsList[i].ItemImage;
            g.transform.GetChild(3).GetComponent<TMPro.TMP_Text>().text = ShopItemsList[i].Price.ToString("#,##0");
            g.transform.GetChild(6).GetComponent<Image>().color = ShopItemsList[i].itemColor;

            PreviewButton = g.transform.GetComponent<Button>();
            CoinConfirmButton = CoinConfirm.transform.GetComponent<Button>();
            GemConfirmButton = GemConfirm.transform.GetComponent<Button>();
            
            if (ShopItemsList[i].IsPurchased)
            {
                if (ShopItemsList[i].ItemName == "Items Chest")
                {
                    return;
                }
                else
                {
                    DisableBuy();
                }
            }

            PreviewButton.AddEventListener(i, PreviewShopItem);
            u = i;
        }

        Destroy(ItemTemplate);
    }

    private void Update()
    {
        if (startInTimer)
        {
            inTimer += 1;
        }

        if (inTimer == 200)
        {
            ConfirmationAnimation.SetBool("In", true);
            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            ConfirmationAnimation.SetBool("Out", false);
            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            inTimer = 0;
            startInTimer = false;
        }
    }

    void PreviewShopItem(int itemIndex)
    {
        PreviewHolder.SetActive(true);

        audioHandler.PlayPopSFX();

        confirmPurchase.ConfirmAnimation.SetBool("In", true);

        PreviewItemName.text = ShopItemsList[itemIndex].ItemName.ToString();
        PreviewItemType.text = ShopItemsList[itemIndex].ItemType.ToString();

        ConfirmItemName.text = ShopItemsList[itemIndex].ItemName.ToString();
        ConfirmItemName.color = ShopItemsList[itemIndex].itemColor;
        ConfirmItemQuality.GetComponent<Image>().sprite = ShopItemsList[itemIndex].QualityImage;

        ConfirmItemImage.GetComponent<Image>().sprite = ShopItemsList[itemIndex].ItemImage;

        if (ShopItemsList[itemIndex].ItemName == "Items Chest")
        {
            PreviewValueGems.SetActive(true);
            MultiBuy.SetActive(true);
            PreviewGemsValue.text = ShopItemsList[itemIndex].GemValue.ToString("#,##0");
            ConfirmGemValue.text = ShopItemsList[itemIndex].GemValue.ToString("#,##0");
            ChestContents.SetActive(true);
        }
        else
        {
            PreviewValueGems.SetActive(false);
            MultiBuy.SetActive(false);
            ChestContents.SetActive(false);
            ContentsHolder.SetActive(false);
        }

        PreviewItem.GetComponent<Image>().sprite = ShopItemsList[itemIndex].ItemImage;
        PreviewItemName.color = ShopItemsList[itemIndex].itemColor;

        PreviewItemQuality.GetComponent<Image>().sprite = ShopItemsList[itemIndex].QualityImage;

        PreviewValueCoins.transform.GetChild(0).GetComponent<TMP_Text>().text = ShopItemsList[itemIndex].Price.ToString("#,##0") ;

        ConfirmValue.text = ShopItemsList[itemIndex].Price.ToString("#,##0");

        itemInt = itemIndex;
        u = itemIndex;
    }

    public void UpdatePositiveValue()
    {
        ShopItemsList[u].Price += 10000;
        ShopItemsList[u].GemValue += 400;

        ConfirmGemValue.text = ShopItemsList[u].GemValue.ToString("#,##0");
        ConfirmValue.text = ShopItemsList[u].Price.ToString("#,##0");
    }

    public void UpdateNegativeValue()
    {
        ShopItemsList[u].Price -= 10000;
        ShopItemsList[u].GemValue -= 400;

        ConfirmGemValue.text = ShopItemsList[u].GemValue.ToString("#,##0");
        ConfirmValue.text = ShopItemsList[u].Price.ToString("#,##0");
    }

    public void ResetValue()
    {
        ShopItemsList[u].Price = 10000;
        ShopItemsList[u].GemValue = 400;
    }

    public void ClosePreivew(int itemIndex)
    {
        PreviewHolder.SetActive(false);
    }

    public void OnCoinButtonClicked()
    {
        if (Game.Instance.HasEnoughCoins(ShopItemsList[itemInt].Price) && !ShopItemsList[itemInt].IsPurchased)
        {
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);   

            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);

                ConfirmationAnimation.SetBool("Out", true);
                ConfirmationAnimation.SetBool("In", false);

                audioHandler.PlayPurchaseSFX();

                PreviewExit();
                StartCoroutine(HidePurchase());

                startInTimer = true;
            }

            Game.Instance.UseCoins(ShopItemsList[itemInt].Price);
            multiBuy.ResetAmount();

            //Purchase item
            ShopItemsList[itemInt].IsPurchased = true;

            if (ShopItemsList[itemInt].ItemName == "Items Chest")
            {
                for (int i = 0; i < multiBuy.itemCount; i++)
                {
                    inventory.UpdateList(itemInt);
                }
            }
            else
            {
                inventory.UpdateList(itemInt);
            }

            //Disable button
            PreviewButton = ShopScrollView.GetChild(itemInt).GetComponent<Button>();

            if(ShopItemsList[itemInt].ItemName == "Items Chest")
            {
                ShopItemsList[itemInt].IsPurchased = false;
                Game.Instance.UpdateAllCoinsUIText();
                return;
            }
            else
            {
                DisableBuy();
            }

            //Change UI text: coins
            Game.Instance.UpdateAllCoinsUIText();
        }
        else
        {
            Debug.Log("You don't have enough coins");

            InvalidCoins.SetActive(true);
            InvalidGems.SetActive(false);
            PurchaseSuccessful.SetActive(false);

            InvalidCoinAnimation.SetBool("Out", true);
            InvalidCoinAnimation.SetBool("In", false);

            startInTimer = true;
        }
    }

    public void OnGemButtonClicked()
    {
        if (Game.Instance.HasEnoughGems(ShopItemsList[itemInt].GemValue) && !ShopItemsList[itemInt].IsPurchased)
        {
            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(false);

            InvalidCoinAnimation.SetBool("In", true);
            InvalidGemAnimation.SetBool("In", true);

            InvalidCoinAnimation.SetBool("Out", false);
            InvalidGemAnimation.SetBool("Out", false);

            if (!ShopItemsList[itemInt].IsPurchased)
            {
                PurchaseSuccessful.SetActive(true);

                ConfirmationAnimation.SetBool("Out", true);
                ConfirmationAnimation.SetBool("In", false);

                audioHandler.PlayPurchaseSFX();

                PreviewExit();
                StartCoroutine(HidePurchaseGems());
            }

            Game.Instance.UseGems(ShopItemsList[itemInt].GemValue);
            multiBuy.ResetAmount();

            //Purchase item
            ShopItemsList[itemInt].IsPurchased = true;

            inventory.UpdateList(itemInt);

            //Disable button
            PreviewButton = ShopScrollView.GetChild(itemInt).GetComponent<Button>();

            if (ShopItemsList[itemInt].ItemName == "Items Chest")
            {
                ShopItemsList[itemInt].IsPurchased = false;
                Game.Instance.UpdateAllGemsUIText();
                return;
            }
            else
            {
                DisableBuy();
            }

            //Change UI text: gems
            Game.Instance.UpdateAllGemsUIText();
        }
        else
        {
            Debug.Log("You don't have enough gems");

            InvalidCoins.SetActive(false);
            InvalidGems.SetActive(true);
            PurchaseSuccessful.SetActive(false);

            InvalidGemAnimation.SetBool("Out", true);
            InvalidGemAnimation.SetBool("In", false);

            startInTimer = true;
        }
    }

    void DisableBuy()
    {
        PreviewButton.interactable = false;
        PreviewButton.transform.GetChild(4).gameObject.SetActive(true);
    }

    public void ViewContent()
    {
        ContentsHolder.SetActive(true);
        PreviewHolder.SetActive(false);
    }

    public void ViewPreview()
    {
        PreviewHolder.SetActive(true);
        ContentsHolder.SetActive(false);
    }

    public void CloseConfirmation()
    {
        confirmPurchase.ConfirmHolder.SetActive(false);
    }

    public void PreviewExit()
    {
        PreviewHolder.SetActive(false);
    }

    public void CloseContent()
    {
        ContentsHolder.SetActive(false);
        PreviewHolder.SetActive(true);
    }

    IEnumerator HidePurchase()
    {
        CloseButton.SetActive(false);
        CoinConfirm.SetActive(false);
        yield return new WaitForSeconds(5f);
        confirmPurchase.OnCancel();
        CloseButton.SetActive(true);
        CoinConfirm.SetActive(true);
    }

    IEnumerator HidePurchaseGems()
    {
        CloseButton.SetActive(false);
        GemConfirm.SetActive(false);

        yield return new WaitForSeconds(5f);
        confirmPurchase.OnCancel();
        CloseButton.SetActive(true);
        GemConfirm.SetActive(true);
    }
}
