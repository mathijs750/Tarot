using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class TransitionManager : MonoBehaviour
{

    private static TransitionManager instance;
    public static TransitionManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("TransitionManager").AddComponent<TransitionManager>();
                instance.Initialize();
            }

            return instance;
        }
    }

    public enum TransitionType
    {
        GameToGlobe,
        GlobeToGame,
        GlobeGameToCam,
        GlobeCamToGame
    }

    private bool transitionActive = false;

    private float transitionDuration = 2;
    private float fadeDuration = 1;

    private Camera globeCamera;
    private Camera gameCamera;
    private Camera camCamera;

    private Image screenFade;

    private void Initialize()
    {
        globeCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        camCamera = GameObject.Find("WebcamCamera").GetComponent<Camera>();
        gameCamera = GameObject.Find("GameCamera").GetComponent<Camera>();

        screenFade = GameObject.Find("ScreenFade").GetComponent<Image>();
    }

    public void StartTransition(TransitionType transition)
    {
        if (transitionActive) return;

        switch(transition)
        {
            case TransitionType.GameToGlobe:
                StartCoroutine(GameToGlobeTransition());
                break;
            case TransitionType.GlobeToGame:
                StartCoroutine(GlobeToGameTransition());
                break;
            case TransitionType.GlobeGameToCam:
                StartCoroutine(GlobeGameToCam());
                break;
            case TransitionType.GlobeCamToGame:
                StartCoroutine(GlobeCamToGame());
                break;
        }
    }

    private IEnumerator GameToGlobeTransition()
    {
        float counter = 0;
        transitionActive = true;

        while (counter < 1)
        {
            globeCamera.fieldOfView = Mathf.Lerp(25, 60, counter);

            float x = Mathf.Lerp(0, .2f, counter);
            float y = Mathf.Lerp(0, .05f, counter);
            float width = Mathf.Lerp(1, .6f, counter);
            float height = Mathf.Lerp(1, .95f, counter);

            gameCamera.rect = new Rect(x, y, width, height);

            counter += Time.deltaTime / transitionDuration;
            yield return null;
        }

        transitionActive = false;
    }

    private IEnumerator GlobeToGameTransition()
    {
        float counter = 0;
        transitionActive = true;

        while (counter < 1)
        {
            globeCamera.fieldOfView = Mathf.Lerp(60, 25, counter);

            float x = Mathf.Lerp(.2f, 0, counter);
            float y = Mathf.Lerp(.05f, 0, counter);
            float width = Mathf.Lerp(.6f, 1, counter);
            float height = Mathf.Lerp(.95f, 1, counter);

            gameCamera.rect = new Rect(x, y, width, height);

            counter += Time.deltaTime / transitionDuration;
            yield return null;
        }

        transitionActive = false;
    }

    private IEnumerator GlobeGameToCam()
    {
        float counter = 0;
        transitionActive = true;
        Color fromColor = screenFade.color;
        Color toColor = new Color(0, 0, 0, 1);

        while (counter < 1)
        {
            screenFade.color = Color.Lerp(fromColor, toColor, counter);

            counter += Time.deltaTime / fadeDuration * 2;
            yield return null;
        }

        gameCamera.depth = -10;
        camCamera.depth = 0;

        fromColor = toColor;
        toColor = new Color(0, 0, 0, 0);
        counter = 0;

        while (counter < 1)
        {
            screenFade.color = Color.Lerp(fromColor, toColor, counter);

            counter += Time.deltaTime / fadeDuration * 2;
            yield return null;
        }

        GameObject.FindObjectOfType<WebCam>().StartQR();

        transitionActive = false;
    }



    private IEnumerator GlobeCamToGame()
    {
        float counter = 0;
        transitionActive = true;
        Color fromColor = screenFade.color;
        Color toColor = new Color(0, 0, 0, 1);

        GameObject.FindObjectOfType<WebCam>().EndQR();

        while (counter < 1)
        {
            screenFade.color = Color.Lerp(fromColor, toColor, counter);

            counter += Time.deltaTime / fadeDuration * 2;
            yield return null;
        }

        gameCamera.depth = 0;
        camCamera.depth = -10;

        fromColor = toColor;
        toColor = new Color(0, 0, 0, 0);
        counter = 0;

        while (counter < 1)
        {
            screenFade.color = Color.Lerp(fromColor, toColor, counter);

            counter += Time.deltaTime / fadeDuration * 2;
            yield return null;
        }

        transitionActive = false;
    }
}
