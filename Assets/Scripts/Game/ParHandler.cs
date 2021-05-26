using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Runtime.CompilerServices;

public class ParHandler : MonoBehaviour
{
    public int par;
    public int hole;
    public GameObject player;

    public GameObject parContainer;

    public DragPower dragPower;

    private OutOfBoundsHandler outOfBounds;

    [Header("Spawns")]
    public GameObject hole1Spawn;
    public GameObject hole2Spawn;
    public GameObject hole3Spawn;
    public GameObject hole4Spawn;
    public GameObject hole5Spawn;
    public GameObject hole6Spawn;
    public GameObject hole7Spawn;
    /*
    public GameObject hole8Spawn;
    public GameObject hole9Spawn;
    */

    [Header("Hole Scores")]
    public TMP_Text hole1Score;
    public TMP_Text hole2Score;
    public TMP_Text hole3Score;
    public TMP_Text hole4Score;
    public TMP_Text hole5Score;
    public TMP_Text hole6Score;
    public TMP_Text hole7Score;
    //public TMP_Text hole8Score;
    //public TMP_Text hole9Score;
    public TMP_Text totalScore;

    public TMPro.TMP_Text parNumber;
    private bool startTimer;
    private int timer;

    private int hole1Strokes;
    private int hole2Strokes;
    private int hole3Strokes;
    private int hole4Strokes;
    private int hole5Strokes;
    private int hole6Strokes;
    private int hole7Strokes;

    void Start()
    {
        hole1Score.text = "";
        hole2Score.text = "";
        hole3Score.text = "";
        hole4Score.text = "";
        hole5Score.text = "";
        hole6Score.text = "";
        hole7Score.text = "";
        //hole8Score.text = "";
        //hole9Score.text = "";
        totalScore.text = "0";

        hole = 1;
        parNumber.GetComponent<TMPro.TMP_Text>().text = par.ToString();

        outOfBounds = GetComponent<OutOfBoundsHandler>();
    }

    void Update()
    {
        if (startTimer)
        {
            timer += 1;
        }

        if (timer == 50)
        {
            //reset strokes
            dragPower.strokes = 0;
            dragPower.UpdateScore(dragPower.strokes);
            timer = 0;
            startTimer = false;
        }

        //hole 1, hole 4, hole 6, hole 7
        if (hole == 1 || hole == 4 || hole == 6 || hole == 7)
        {
            par = 2;
            UpdatePar(par);
        }

        //hole 2, hole 3, hole 5
        if (hole == 2 || hole == 3 || hole == 5)
        {
            par = 3;
            UpdatePar(par);
        }  
    }

    public void OnTriggerEnter(Collider col)
    {
        dragPower.ball.velocity = Vector3.zero;

        dragPower.previousScore = dragPower.strokes;
        startTimer = true;
        
        dragPower.score.GetComponent<TMPro.TMP_Text>().text = dragPower.strokes.ToString();
        dragPower.UpdateScore(dragPower.strokes);

        if (col.CompareTag("Hole 1"))
        {
            hole = 2;
            player.transform.position = hole2Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 1 on leaderboard
            hole1Strokes = dragPower.previousScore;
            hole1Score.text = hole1Strokes.ToString();
            totalScore.text = hole1Strokes.ToString();

        }

        if (col.CompareTag("Hole 2"))
        {
            hole = 3;
            player.transform.position = hole3Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 2 on leaderboard
            hole2Strokes = dragPower.previousScore;
            hole2Score.text = hole2Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes).ToString();

        }

        if (col.CompareTag("Hole 3"))
        {
            hole = 4;
            player.transform.position = hole4Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 3 on leaderboard
            hole3Strokes = dragPower.previousScore;
            hole3Score.text = hole3Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes + hole3Strokes).ToString();
        }

        if (col.CompareTag("Hole 4"))
        {
            hole = 5;
            player.transform.position = hole5Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 4 on leaderboard
            hole4Strokes = dragPower.previousScore;
            hole4Score.text = hole4Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes + hole3Strokes + hole4Strokes).ToString();
        }

        if (col.CompareTag("Hole 5"))
        {
            hole = 6;
            player.transform.position = hole6Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 5 on leaderboard
            hole5Strokes = dragPower.previousScore;
            hole5Score.text = hole5Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes + hole3Strokes + hole4Strokes + hole5Strokes).ToString();
        }

        if (col.CompareTag("Hole 6"))
        {
            hole = 7;
            player.transform.position = hole7Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 6 on leaderboard
            hole6Strokes = dragPower.previousScore;
            hole6Score.text = hole6Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes + hole3Strokes + hole4Strokes + hole5Strokes + hole6Strokes).ToString();
        }

        if (col.CompareTag("Hole 7"))
        {
            hole = 8;
            //player.transform.position = hole8Spawn.transform.position;
            transform.position = player.transform.position;

            //update score for hole 7 on leaderboard
            hole7Strokes = dragPower.previousScore;
            hole7Score.text = hole7Strokes.ToString();
            totalScore.text = (hole1Strokes + hole2Strokes + hole3Strokes + hole4Strokes + hole5Strokes + hole6Strokes + hole7Strokes).ToString();
        }
    }

    public void UpdatePar(int par)
    {
        parNumber.GetComponent<TMPro.TMP_Text>().text = par.ToString();
    }

    /*
    public void OnCollisionEnter(Collision col)
    {
        if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
        {
            if (hole == 1)
            {
                if (col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position              
                    player.transform.position = new Vector3(pos.x, pos.y + 0.5f, pos.z);
                }
            }

            if (hole == 2)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 3)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 4)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 5)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 6)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 7)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 8 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 8)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 9 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }

            if (hole == 9)
            {
                if (col.collider.CompareTag("Hole 1 Course") || col.collider.CompareTag("Hole 2 Course") || col.collider.CompareTag("Hole 3 Course") || col.collider.CompareTag("Hole 4 Course") || col.collider.CompareTag("Hole 5 Course") || col.collider.CompareTag("Hole 6 Course") || col.collider.CompareTag("Hole 7 Course") || col.collider.CompareTag("Hole 8 Course"))
                {
                    Vector3 pos = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                    //if touching hole its not currently on, reset back to other position
                    player.transform.position = GameObject.FindGameObjectWithTag("Last Position").transform.position;
                }
            }
        }
    }
    */
}
