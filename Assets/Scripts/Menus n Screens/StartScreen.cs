using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour {

    public float fadeSpeed = 0.1F;
    public bool fadeIn = true;
    public Image SplashScreen;


IEnumerator WaitAsec()
    {
        yield return new WaitForSeconds(3);
        SplashScreen.CrossFadeAlpha(0, 0.8f, false);
    }

    void Update()
    {
        float Fade = 0f;
        if (fadeIn)
        {
            StartCoroutine(WaitAsec());
        }

        if (Input.GetKeyDown("joystick button 7"))
        {
            print("nextScene");
        }
	}
}
