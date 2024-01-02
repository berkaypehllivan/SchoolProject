using UnityEngine;

public class FadeInOut : MonoBehaviour
{
    public CanvasGroup canvas;
    public bool fadein = false;
    public bool fadeout = false;
    public float timeToFade;

    private void Update()
    {
        if (fadein)
        {
            if (canvas.alpha < 1)
            {
                canvas.alpha += Time.deltaTime / timeToFade;
                if (canvas.alpha >= 1)
                {
                    fadein = false;
                }
            }
        }
        if (fadeout)
        {
            if (canvas.alpha > 0)
            {
                canvas.alpha -= Time.deltaTime / timeToFade;
                if (canvas.alpha <= 0)
                {
                    fadeout = false;
                }
            }
        }
    }

    public void FadeIn()
    {
        fadein = true;
    }

    public void FadeOut()
    {
        fadeout = true;
    }
}