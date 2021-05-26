using System.Collections;
using System.Collections.Generic;
using UnityEditor.UIElements;
using UnityEngine;

public class OutOfBoundsHandler : MonoBehaviour
{
    public ParHandler parHandler;
    public DragPower dragPower;

    public Collision player;

    public bool resetPosition;

    public bool checkForMovement;

    public bool startTimer;
    public int timer;
    private void Start()
    {
        resetPosition = false;
        checkForMovement = false;
    }

    public void Update()
    {
        if (startTimer)
        {
            timer += 1;
        }

        if (timer == 100)
        {
            dragPower.ball.sleepThreshold = 0.005f; //default is 0.005f;
        }

        if (timer == 150)
        {
            resetPosition = true;

            checkForMovement = false;

            timer = 0;
            startTimer = false;

            dragPower.ball.sleepThreshold = 0.5f; //default is 0.005f;
        }

        if (checkForMovement)
        {
            startTimer = true;
        }

        if (resetPosition)
        {
            //move back to last position
            player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
            resetPosition = false;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        checkForMovement = true;

        player = collision;
    }
}
