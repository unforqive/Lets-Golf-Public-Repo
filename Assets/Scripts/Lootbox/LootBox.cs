using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EZCameraShake;
using TMPro;

[RequireComponent(typeof(Collider))]
public class LootBox : MonoBehaviour
{
    public GameObject HUDCanvas;
    public GameObject MenuCanvas;

    public LootSystem lootSystem;
    public GameObject LootBoxInfo;

    [Space]
    [Header("LootBox")]
    public GameObject lootBox;
    public GameObject lootReward;
    public Animator lootAnimation;
    public GameObject lootPosition;

    [Space]
    [Header("Camera")]
    public Camera mainCamera;
    public Camera lootBoxCamera;

    [Space]
    [Header("VFX Objects")]
    public GameObject crashVFX;
    public GameObject lootVFX;
    public GameObject lightFX;

    [Space]
    [Header("Loot Item Info")]
    public GameObject itemName;
    public GameObject itemQuality;

    [Space]
    public Animator animator;
    private RaycastHit hit;
    private Ray ray;

    [Space]
    [Header("Fade To Black")]
    public GameObject fadeToBlack;
    public Animator fadeAnimation;

    [Header("SFX")]
    public AudioHandler audioHandler;

    private bool resetLoot;

    private bool startTimer;
    private int timer;

    private bool showChest;

    private bool startTransitionTimer;
    private int transitionTimer;

    private bool startTransitionOutTimer;
    public int transitionOutTimer;

    private bool canClick;

    void Start()
    {
        HUDCanvas.SetActive(true);
        MenuCanvas.SetActive(true);

        LootBoxInfo.SetActive(false);

        lootSystem.CalculateLoot();

        itemName.GetComponent<TMP_Text>().text = lootSystem.LootTable[lootSystem.j].item.itemName;

        itemQuality.GetComponent<TMP_Text>().text = lootSystem.LootTable[lootSystem.j].item.itemQuality;
        itemQuality.GetComponent<TMP_Text>().color = lootSystem.LootTable[lootSystem.j].item.itemColour;

        animator = gameObject.GetComponent<Animator>();

        showChest = false;
    }

    void Update()
    {
        #region Timers

        if (startTimer)
        {
            timer += 1;
        }

        if (timer == 500)
        {
            lootAnimation.SetBool("HideReward", true);
        }

        if (timer == 680)
        {
            animator.SetBool("Close", true);
            animator.SetBool("Open", false);
        }

        if (timer == 800)
        {
            lightFX.SetActive(false);
            lootVFX.SetActive(false);
        }

        if (timer == 400)
        {
            fadeAnimation.SetBool("FadeTo", true);
            fadeAnimation.SetBool("FadeFrom", false);

            startTransitionOutTimer = true;

            timer = 0;
            startTimer = false;
        }

        if (startTransitionTimer)
        {
            transitionTimer += 1;
        }

        if (transitionTimer == 50)
        {
            mainCamera.enabled = false;
            lootBoxCamera.enabled = true;

            //turn off canvas items
            HUDCanvas.SetActive(false);
            MenuCanvas.SetActive(false);
        }

        if (transitionTimer == 150)
        {
            fadeAnimation.SetBool("FadeTo", false);
            fadeAnimation.SetBool("FadeFrom", true);

            showChest = true;

            transitionTimer = 0;
            startTransitionTimer = false;
        }

        if (startTransitionOutTimer)
        {
            transitionOutTimer += 1;
        }

        if (transitionOutTimer == 50)
        {
            LootBoxInfo.SetActive(false);
            HUDCanvas.SetActive(true);
            MenuCanvas.SetActive(true);

            mainCamera.enabled = true;
            lootBoxCamera.enabled = false;
        }

        if (transitionOutTimer == 100)
        {
            fadeAnimation.SetBool("FadeTo", false);
            fadeAnimation.SetBool("FadeFrom", true);
            resetLoot = true;
            showChest = false;
        }

        if (transitionOutTimer == 50)
        {
            fadeToBlack.SetActive(false);

            animator.SetBool("Idle", true);
            animator.SetBool("Close", false);

            transitionOutTimer = 0;
            startTransitionOutTimer = false;
        }

        #endregion

        if (resetLoot)
        {
            ResetLoot();

            resetLoot = false;
        }

        if (showChest)
        {
            canClick = true;
            ray = lootBoxCamera.ScreenPointToRay(Input.mousePosition);
        }

        if(Physics.Raycast(ray, out hit))
        {
            if(hit.transform.name == gameObject.name)
            {
                animator.SetBool("Idle", false);
                animator.SetBool("Hover", true);

                if(Input.GetMouseButtonDown(0) && canClick)
                {
                    animator.SetBool("Open", true);

                    canClick = false;
                }
            }
            else
            {
                animator.SetBool("Idle", true);
                animator.SetBool("Hover", false);
            }
        }
    }

    //Called via animation
    public void LootReward(int j)
    {
        lootSystem.SpawnItem();
    }

    public void LootVFX()
    {
        startTimer = true;
        lootVFX.SetActive(true);
    }

    public void CrashVFX()
    {
        lightFX.SetActive(true);
        crashVFX.SetActive(true);
        lootVFX.SetActive(false);

        CameraShaker.Instance.ShakeOnce(10f, 10f, .1f, 1f);
    }

    public void InfoIn()
    {
        LootBoxInfo.SetActive(true);
    }

    public void ShowLootBoxScene()
    {
        fadeToBlack.SetActive(true);
        fadeAnimation.SetBool("FadeTo", true);
        fadeAnimation.SetBool("FadeFrom", false);

        startTransitionTimer = true;
    }

    void ResetLoot()
    {
        lootSystem.CalculateLoot();

        itemName.GetComponent<TMP_Text>().text = lootSystem.LootTable[lootSystem.j].item.itemName;

        itemQuality.GetComponent<TMP_Text>().text = lootSystem.LootTable[lootSystem.j].item.itemQuality;
        itemQuality.GetComponent<TMP_Text>().color = lootSystem.LootTable[lootSystem.j].item.itemColour;

        lightFX.SetActive(false);
        crashVFX.SetActive(false);
        lootVFX.SetActive(false);

        Destroy(lootSystem.clonedLoot);
    }

    public void PlayChestRattleSound()
    {
        audioHandler.PlayChestRattleSFX();
    }

    public void PlayChestCloseSound()
    {
        audioHandler.PlayChestCloseSFX();
    }

    public void PlayLootSound()
    {
        audioHandler.PlayLootRewardSFX();
    }
}
