using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Multibuy : MonoBehaviour
{
    public int itemCount;
    public TMP_Text amountOfItems;
    public Shop shop;
    public int buttonPressCount;

    public bool startTimer;
    public int timer;

    void Start()
    {
        buttonPressCount = 0;
        itemCount = 1;
    }

    void Update()
    {
        if (startTimer)
        {
            timer += 1;
        }

        if (timer > 5)
        {
            shop.ResetValue();
            buttonPressCount = 0;
            itemCount = 1;

            timer = 0;
            startTimer = false;
        }

        itemCount = Mathf.Clamp(itemCount, 1, 99);
        
        amountOfItems.text = itemCount.ToString();
    }

    public void IncreaseAmount()
    {
        if (buttonPressCount < 98)
        {
            buttonPressCount += 1;
            itemCount += 1;
            shop.UpdatePositiveValue();
        }

        if (buttonPressCount == 98)
        {
            return;
        }
    }

    public void DecreaseAmount()
    {
        if (buttonPressCount >= 1)
        {
            buttonPressCount -= 1;
            itemCount -= 1;
            shop.UpdateNegativeValue();
        }
        
        if (buttonPressCount == 0)
        {
            return;
        }
    }

    public void ResetAmount()
    {
        startTimer = true;
    }
}
