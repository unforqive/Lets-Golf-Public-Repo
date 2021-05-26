using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.Jobs;
using UnityEngine;



public class LootSystem : MonoBehaviour
{
    [System.Serializable]
    public class Loot
    {
        public Item item;
    }

    public LootBox lootbox;
    public List<Loot> LootTable = new List<Loot>();

    [Header("Unreal VFXs")]
    [SerializeField] GameObject UnrealLight;
    [SerializeField] GameObject UnrealCrash;
    [SerializeField] GameObject UnrealItemVFX;

    [Space]
    [Header("Mythical VFXs")]
    [SerializeField] GameObject MythicalLight;
    [SerializeField] GameObject MythicalCrash;
    [SerializeField] GameObject MythicalItemVFX;

    [Space]
    [Header("Legendary VFXs")]
    [SerializeField] GameObject LegendaryLight;
    [SerializeField] GameObject LegendaryCrash;
    [SerializeField] GameObject LegendaryItemVFX;

    [Space]
    [Header("Rare VFXs")]
    [SerializeField] GameObject RareLight;
    [SerializeField] GameObject RareCrash;
    [SerializeField] GameObject RareItemVFX;

    [Space]
    [Header("Common VFXs")]
    [SerializeField] GameObject CommonLight;
    [SerializeField] GameObject CommonCrash;
    [SerializeField] GameObject CommonItemVFX;

    public float dropChance;
    public int j;

    public GameObject clonedLoot;

    private void Start()
    {
        //Unreal
        UnrealLight.SetActive(false);
        UnrealCrash.SetActive(false);
        UnrealItemVFX.SetActive(false);

        //Mythical
        MythicalLight.SetActive(false);
        MythicalCrash.SetActive(false);
        MythicalItemVFX.SetActive(false);

        //Legendary
        LegendaryLight.SetActive(false);
        LegendaryCrash.SetActive(false);
        LegendaryItemVFX.SetActive(false);

        //Rare
        RareLight.SetActive(false);
        RareCrash.SetActive(false);
        RareItemVFX.SetActive(false);

        //Common
        CommonLight.SetActive(false);
        CommonCrash.SetActive(false);
        CommonItemVFX.SetActive(false);
    }

    public void CalculateLoot()
    {
        float calc_dropChance = 0;
        
        if (calc_dropChance > dropChance)
        {
            Debug.Log("No loot");
            return;
        }

        if (calc_dropChance <= dropChance)
        {
            float itemWeight = 0f;

            for (int i = 0; i < LootTable.Count; i++)
            {
                itemWeight += LootTable[j].item.rarity;

                j = i;
            }

            float randomValue = Random.Range(0, itemWeight);

            for (j = 0; j < LootTable.Count; j++)
            {
                if(randomValue <= LootTable[j].item.rarity)
                {
                    Debug.Log("Item Dropped: " + LootTable[j].item.itemName + " | Item Quality: " + LootTable[j].item.itemQuality + " | Item Rarity: " + LootTable[j].item.rarity + " | Item Weight: " + itemWeight);

                    //Unreal Rarity\\
                    if (LootTable[j].item.itemQuality == "Unreal")
                    {
                        lootbox.crashVFX = UnrealCrash;
                        lootbox.lootVFX = UnrealItemVFX;
                        lootbox.lightFX = UnrealLight;
                        UnrealShowVFX();
                    }

                    //Mythical Rarity\\
                    if (LootTable[j].item.itemQuality == "Mythical")
                    {
                        lootbox.crashVFX = MythicalCrash;
                        lootbox.lootVFX = MythicalItemVFX;
                        lootbox.lightFX = MythicalLight;
                        MythicalShowVFX();
                    }

                    //Legendary Rarity\\
                    if (LootTable[j].item.itemQuality == "Legendary")
                    {
                        lootbox.crashVFX = LegendaryCrash;
                        lootbox.lootVFX = LegendaryItemVFX;
                        lootbox.lightFX = LegendaryLight;
                        LegendaryShowVFX();
                    }

                    //Rare Rarity\\
                    if (LootTable[j].item.itemQuality == "Rare")
                    {
                        lootbox.crashVFX = RareCrash;
                        lootbox.lootVFX = RareItemVFX;
                        lootbox.lightFX = RareLight;
                        RareShowVFX();
                    }

                    //Common Rarity\\
                    if (LootTable[j].item.itemQuality == "Common")
                    {
                        lootbox.crashVFX = CommonCrash;
                        lootbox.lootVFX = CommonItemVFX;
                        lootbox.lightFX = CommonLight;
                        CommonShowVFX();
                    }
                    return;
                }

                randomValue -= LootTable[j].item.rarity;
            }
        } 
    }

    public void SpawnItem()
    {
        clonedLoot = Instantiate(LootTable[j].item.itemObject, lootbox.lootPosition.transform);

        lootbox.lootAnimation = lootbox.lootPosition.transform.GetComponentInChildren<Animator>();
    }

    private void UnrealShowVFX()
    {
        //Unreal
        UnrealLight.SetActive(true);
        UnrealCrash.SetActive(true);
        UnrealItemVFX.SetActive(true);

        //Mythical
        MythicalLight.SetActive(false);
        MythicalCrash.SetActive(false);
        MythicalItemVFX.SetActive(false);

        //Legendary
        LegendaryLight.SetActive(false);
        LegendaryCrash.SetActive(false);
        LegendaryItemVFX.SetActive(false);

        //Rare
        RareLight.SetActive(false);
        RareCrash.SetActive(false);
        RareItemVFX.SetActive(false);

        //Common
        CommonLight.SetActive(false);
        CommonCrash.SetActive(false);
        CommonItemVFX.SetActive(false);
    }

    private void MythicalShowVFX()
    {
        //Unreal
        UnrealLight.SetActive(false);
        UnrealCrash.SetActive(false);
        UnrealItemVFX.SetActive(false);

        //Mythical
        MythicalLight.SetActive(true);
        MythicalCrash.SetActive(true);
        MythicalItemVFX.SetActive(true);

        //Legendary
        LegendaryLight.SetActive(false);
        LegendaryCrash.SetActive(false);
        LegendaryItemVFX.SetActive(false);

        //Rare
        RareLight.SetActive(false);
        RareCrash.SetActive(false);
        RareItemVFX.SetActive(false);

        //Common
        CommonLight.SetActive(false);
        CommonCrash.SetActive(false);
        CommonItemVFX.SetActive(false);
    }

    private void LegendaryShowVFX()
    {
        //Unreal
        UnrealLight.SetActive(false);
        UnrealCrash.SetActive(false);
        UnrealItemVFX.SetActive(false);

        //Mythical
        MythicalLight.SetActive(false);
        MythicalCrash.SetActive(false);
        MythicalItemVFX.SetActive(false);

        //Legendary
        LegendaryLight.SetActive(true);
        LegendaryCrash.SetActive(true);
        LegendaryItemVFX.SetActive(true);

        //Rare
        RareLight.SetActive(false);
        RareCrash.SetActive(false);
        RareItemVFX.SetActive(false);

        //Common
        CommonLight.SetActive(false);
        CommonCrash.SetActive(false);
        CommonItemVFX.SetActive(false);
    }

    private void RareShowVFX()
    {
        //Unreal
        UnrealLight.SetActive(false);
        UnrealCrash.SetActive(false);
        UnrealItemVFX.SetActive(false);

        //Mythical
        MythicalLight.SetActive(false);
        MythicalCrash.SetActive(false);
        MythicalItemVFX.SetActive(false);

        //Legendary
        LegendaryLight.SetActive(false);
        LegendaryCrash.SetActive(false);
        LegendaryItemVFX.SetActive(false);

        //Rare
        RareLight.SetActive(true);
        RareCrash.SetActive(true);
        RareItemVFX.SetActive(true);

        //Common
        CommonLight.SetActive(false);
        CommonCrash.SetActive(false);
        CommonItemVFX.SetActive(false);
    }

    private void CommonShowVFX()
    {
        //Unreal
        UnrealLight.SetActive(false);
        UnrealCrash.SetActive(false);
        UnrealItemVFX.SetActive(false);

        //Mythical
        MythicalLight.SetActive(false);
        MythicalCrash.SetActive(false);
        MythicalItemVFX.SetActive(false);

        //Legendary
        LegendaryLight.SetActive(false);
        LegendaryCrash.SetActive(false);
        LegendaryItemVFX.SetActive(false);

        //Rare
        RareLight.SetActive(false);
        RareCrash.SetActive(false);
        RareItemVFX.SetActive(false);

        //Common
        CommonLight.SetActive(true);
        CommonCrash.SetActive(true);
        CommonItemVFX.SetActive(true);
    }
}
