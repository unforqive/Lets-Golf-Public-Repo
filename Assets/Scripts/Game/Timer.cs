using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public TMP_Text textDisplay;

    public int Duration { get; private set; }
    private int remainingDuration;

    public void Awake()
    {
        ResetTimer();
    }


    private void ResetTimer()
    {
        textDisplay.text = "00:00";

        Duration = remainingDuration = 0;
    }

    public Timer SetDuration (int seconds)
    {
        Duration = remainingDuration = seconds;
        return this ;
    }

    public void Begin()
    {
        StopAllCoroutines();
        StartCoroutine(UpdateTimer());
    }

    private IEnumerator UpdateTimer()
    {
        while (remainingDuration > 0)
        {
            UpdateUI(remainingDuration);
            remainingDuration--;
            yield return new WaitForSeconds(1f);

        }
        End();
    }

    private void UpdateUI(int seconds)
    {
        textDisplay.text = string.Format("{0:D2}:{1:D2}", seconds / 60, seconds % 60);
    }    

    public void End()
    {
        ResetTimer();
    }
    
}
