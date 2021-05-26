using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject menuCamera;
    public GameObject Player;

    public GameObject playerCamera;

    public MenuController menuController;

    void Update()
    {
    }

    public void EnablePlayerCamera()
    {
        Player.SetActive(true);
        menuCamera.SetActive(false);
    }
}
