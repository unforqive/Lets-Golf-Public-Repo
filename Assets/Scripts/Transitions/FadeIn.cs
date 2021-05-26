using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeIn : MonoBehaviour
{
    public GameObject fade;

    void Start()
    {
        fade.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
        fade.SetActive(false);
    }

    public void fadeIn()
    {
        fade.SetActive(true);
        fade.GetComponent<Image>().CrossFadeAlpha(1, 2, false);
        fade.GetComponent<Image>().canvasRenderer.SetAlpha(0.0f);
    }

    public void fadeOut()
    {
        fade.SetActive(true);
        fade.GetComponent<Image>().CrossFadeAlpha(0, 2, false);
        fade.GetComponent<Image>().canvasRenderer.SetAlpha(1f);
    }
}
