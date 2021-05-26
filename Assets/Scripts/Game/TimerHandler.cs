using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimerHandler : MonoBehaviour
{
    [SerializeField] Timer timer1;
    public int time;
    public GameObject timerObject;
    public void Start()
    {
        timerObject.SetActive(false);
    }

    public void StartTime()
    {
        time *= 60;
        timer1.SetDuration(time).Begin();
    }
}
