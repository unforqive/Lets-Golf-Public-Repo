using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public GameObject menuCamera;
    public CursorLock cursorLock;

    public GameObject player;

    public bool pause;
    public MenuController menuController;
    public bool startPausedTimer;
    public int pausedTimer;

    public GameObject returnToMenuButton;
    public GameObject pauseCloseButton;
    public GameObject normalCloseButton;

    public bool startAnimationTimer;
    public int animationTimer;

    public bool startCloseTimer;
    public int closeTimer;

    public bool pausePressed;

    public Timer timer;
    public GameObject timerObject;

    public LoadName loadName;

    void Start()
    {
        returnToMenuButton.SetActive(false);
        normalCloseButton.SetActive(true);
        pauseCloseButton.SetActive(false);
        pause = false;
        pausePressed = false;
    }

    void Update()
    {
        if (startAnimationTimer)
        {
            animationTimer += 1;
        }

        if (animationTimer > 10)
        {
            menuController.SettingsMenu.SetActive(false);
            startAnimationTimer = false;
            animationTimer = 0;
        }

        if (startPausedTimer)
        {
            pausedTimer += 1;
        }

        if (pausedTimer > 50)
        {
            Time.timeScale = 0;
            startPausedTimer = false;
            pausedTimer = 0;
        }

        if (startCloseTimer)
        {
            closeTimer += 1;
        }

        if (closeTimer == 10)
        {
            pause = false;
            closeTimer = 0;
            startCloseTimer = false;
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !pausePressed && menuController.canPause && animationTimer == 0)
        {
            pausePressed = true;
            PauseButtonPressed();

            pause = true;
            menuController.SettingsMenu.SetActive(true);
            menuController.audioHandler.sfx.PlayOneShot(menuController.audioHandler.longSwooshSFX);
        }
    }

    public void PauseButtonPressed()
    {
        startPausedTimer = true;
        returnToMenuButton.SetActive(true);
        normalCloseButton.SetActive(false);
        pauseCloseButton.SetActive(true);
    }

    public void ClosePauseMenu()
    {
        menuController.canPause = true;

        startCloseTimer = true;
        Time.timeScale = 1;

        pausePressed = false;

        menuController.SettingsMenuAnimation.SetBool("Settings Appear", false);
        menuController.SettingsMenuAnimation.SetBool("Settings Disappear", true);
        menuController.returnToMenu = true;

        menuController.audioHandler.sfx.PlayOneShot(menuController.audioHandler.swooshSFX);
        startAnimationTimer = true;
        returnToMenuButton.SetActive(false);
        normalCloseButton.SetActive(true);
        pauseCloseButton.SetActive(false);
    }

    public void ReturnToMenu()
    {
        loadName.displayName.SetActive(true);

        timer.End();
        timerObject.SetActive(false);
        pausePressed = false;

        //return to main menu
        menuCamera.SetActive(true);
        menuController.returnToMenu = true;

        returnToMenuButton.SetActive(false);
        normalCloseButton.SetActive(true);
        pauseCloseButton.SetActive(false);

        Time.timeScale = 1;
        player.SetActive(false);
        menuController.audioHandler.soundtrack.Play();

        menuController.audioHandler.sfx.PlayOneShot(menuController.audioHandler.longSwooshSFX);

        menuController.nextMenu = "Start Menu";

        cursorLock.showCursor();
        menuController.QuitGame();
    }
}
