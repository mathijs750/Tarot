using UnityEngine;
using System.Collections;

public class WebCamTest : MonoBehaviour {

    WebCamTexture webcamTexture;
    Renderer renderer;

    // Use this for initialization
    void Start () {
        webcamTexture = new WebCamTexture();
        renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        webcamTexture.Play();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(KeyCode.Space))
            Debug.Log(ReadColor());
	}

    // Read Webcam Color
    private Color ReadColor()
    {
        Color[] Pixels = webcamTexture.GetPixels(webcamTexture.width / 2 - 5, webcamTexture.height / 2 - 5, 10, 10);
        float r = 0,g = 0,b = 0;

        foreach(Color color in Pixels)
        {
            r += color.r;
            g += color.g;
            b += color.b;
        }

        r = r / Pixels.Length;
        g = g / Pixels.Length;
        b = b / Pixels.Length;

        return new Color(r, g, b);
    }
}
