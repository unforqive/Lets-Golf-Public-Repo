using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Codes : MonoBehaviour
{
    private bool redemeed;
    private bool declined;
    private bool alreadyReedeemed;
    public TMP_InputField codeInput;
    public TMP_Text placeHolderText;
    public TMP_Text actualText;

    public Animator CodesAnimation;
    public GameObject Background;
    public GameObject CodesHolder;

    private bool alreadyRedeemedRelease;
    private bool alreadyRedeemedBetaTester;
    private bool alreadyRedeemedJude;
    private bool alreadyRedeemedJake;
    private bool alreadyRedeemedJames;

    public void Start()
    {
        alreadyRedeemedRelease = (PlayerPrefs.GetInt("HasReleaseCode") != 0);
        alreadyRedeemedBetaTester = (PlayerPrefs.GetInt("HasBetaCode") != 0);
        alreadyRedeemedJude = (PlayerPrefs.GetInt("HasJudeCode") != 0);
        alreadyRedeemedJake = (PlayerPrefs.GetInt("HasJakeCode") != 0);
        alreadyRedeemedJames = (PlayerPrefs.GetInt("HasJamesCode") != 0);

        Background.SetActive(false);
        CodesHolder.SetActive(false);
    }

    public void RedeemCode()
    {
        //Codes
        if (codeInput.text == "RELEASE")
        {
            if (!alreadyRedeemedRelease)
            {
                //reward player
                CurrencyData.Coins += 1000;
                CurrencyData.Gems += 100;
                Game.Instance.UpdateAllCoinsUIText();
                Game.Instance.UpdateAllGemsUIText();

                redemeed = true;

                alreadyRedeemedRelease = true;
                PlayerPrefs.SetInt("HasReleaseCode", alreadyRedeemedRelease ? 1 : 0);   
            }
            else
            {
                alreadyReedeemed = true;
            } 
        }

        if (codeInput.text == "BETA TESTER")
        {
            if (!alreadyRedeemedBetaTester)
            {
                //reward player
                CurrencyData.Coins += 10000;
                CurrencyData.Gems += 5000;
                Game.Instance.UpdateAllCoinsUIText();
                Game.Instance.UpdateAllGemsUIText();

                redemeed = true;

                alreadyRedeemedBetaTester = true;
                PlayerPrefs.SetInt("HasBetaCode", alreadyRedeemedBetaTester ? 1 : 0);      
            }
            else
            {
                alreadyReedeemed = true;
            }
        }

        if (codeInput.text == "JUDE")
        {
            if (!alreadyRedeemedJude)
            {
                //reward player
                CurrencyData.Coins += 696969;
                CurrencyData.Gems += 420420;
                Game.Instance.UpdateAllCoinsUIText();
                Game.Instance.UpdateAllGemsUIText();

                redemeed = true;

                alreadyRedeemedJude = true;
                PlayerPrefs.SetInt("HasJudeCode", alreadyRedeemedJude ? 1 : 0);
            }
            else
            {
                alreadyReedeemed = true;
            }
        }

        if (codeInput.text == "JAKE")
        {
            if (!alreadyRedeemedJake)
            {
                //reward player
                CurrencyData.Coins += 19861986;
                CurrencyData.Gems += 19861986;
                Game.Instance.UpdateAllCoinsUIText();
                Game.Instance.UpdateAllGemsUIText();

                redemeed = true;

                alreadyRedeemedJake = true;
                PlayerPrefs.SetInt("HasJakeCode", alreadyRedeemedJake ? 1 : 0);
            }
            else
            {
                alreadyReedeemed = true;
            }
        }

        if (codeInput.text == "JAMES")
        {
            if (!alreadyRedeemedJames)
            {
                //reward player
                CurrencyData.Coins += 999999999;
                CurrencyData.Gems += 999999999;
                Game.Instance.UpdateAllCoinsUIText();
                Game.Instance.UpdateAllGemsUIText();

                redemeed = true;

                alreadyRedeemedJames = true;
                PlayerPrefs.SetInt("HasJamesCode", alreadyRedeemedJames ? 1 : 0);
            }
            else
            {
                alreadyReedeemed = true;
            }
        }

        else
        {
            if (!alreadyReedeemed && !redemeed)
            {
                declined = true;
            }
        }
    }

    private void Update()
    {
        if (redemeed)
        {
            StartCoroutine(WaitForRedeemed());
            redemeed = false;
        }

        if (alreadyReedeemed)
        {
            StartCoroutine(WaitForAlreadyRedeemed());
            alreadyReedeemed = false;
        }

        if (declined)
        {
            StartCoroutine(WaitForInvalid());
            declined = false;
        }
    }

    IEnumerator WaitForRedeemed()
    {
        placeHolderText.enabled = true;
        actualText.enabled = false;

        placeHolderText.text = "Code redeemed!";

        yield return new WaitForSeconds(1);

        actualText.enabled = true;
        placeHolderText.enabled = false;
    }

    IEnumerator WaitForInvalid()
    {
        placeHolderText.enabled = true;
        actualText.enabled = false;

        placeHolderText.text = "Code invalid";

        yield return new WaitForSeconds(1);

        actualText.enabled = true;
        placeHolderText.enabled = false;
    }

    IEnumerator WaitForAlreadyRedeemed()
    {
        placeHolderText.enabled = true;
        actualText.enabled = false;

        placeHolderText.text = "Code already redeemed";

        yield return new WaitForSeconds(1);

        actualText.enabled = true;
        placeHolderText.enabled = false;
    }
    
    public void OnEntry()
    {
        CodesHolder.SetActive(true);

        Background.SetActive(true);

        CodesAnimation.SetBool("Out", true);
        CodesAnimation.SetBool("In", false);
    }

    public void OnExit()
    {
        Background.SetActive(false);
        CodesAnimation.SetBool("Out", false);
        CodesAnimation.SetBool("In", true);
    }

    public void CloseCodes()
    {
        CodesHolder.SetActive(false);
    }
}
