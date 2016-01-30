using UnityEngine;
using System;
using System.Collections;

using com.google.zxing.qrcode;

public class WebCam : MonoBehaviour {

    WebCamTexture webcamTexture;
    private Coroutine qrRoutine;

    private Color32[] c;
    private sbyte[] d;
    private int W, H, WxH;
    private int x, y, z;
    
    void Start ()
    {
        webcamTexture = new WebCamTexture();
        Renderer renderer = GetComponent<Renderer>();
        renderer.material.mainTexture = webcamTexture;
        OnEnable();
    }

    void Update()
    {
        c = webcamTexture.GetPixels32();
    }

    void OnEnable()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Play();
            W = webcamTexture.width;
            H = webcamTexture.height;
            WxH = W * H;
        }
    }

    void OnDisable()
    {
        if (webcamTexture != null)
        {
            webcamTexture.Pause();
        }
    }

    void OnDestroy()
    {
        StopCoroutine(qrRoutine);
        webcamTexture.Stop();
    }

    public void StartQR()
    {
        if (qrRoutine == null)
            qrRoutine = StartCoroutine(ReadQR());
    }

    public void EndQR()
    {
        if (qrRoutine != null)
        {
            StopCoroutine(qrRoutine);
            qrRoutine = null;
        }
    }

    IEnumerator ReadQR()
    {
        while (true)
        {
            try
            {
                d = new sbyte[WxH];
                z = 0;
                for (y = H - 1; y >= 0; y--)
                { // flip
                    for (x = 0; x < W; x++)
                    {
                        d[z++] = (sbyte)(((int)c[y * W + x].r) << 16 | ((int)c[y * W + x].g) << 8 | ((int)c[y * W + x].b));
                    }
                }

                SendQR(new QRCodeReader().decode(d, W, H).Text);
            }
            catch {}

            yield return null;
        }
    }

    private void SendQR(string QR)
    {
        int qrParse = int.Parse(QR);

        Debug.Log("SendQR " + qrParse);

        if (qrParse == 13 || qrParse == 14 || qrParse == 15 || qrParse == 16)
            CardManager.Instance.ReceiveWeather((CardManager.WeatherCard)qrParse);
        else if (qrParse == 1 || qrParse == 2 || qrParse == 3 || qrParse == 4 || qrParse == 5 || qrParse == 6)
            CardManager.Instance.ReceiveEvent((CardManager.EventCard)qrParse);
        else if (qrParse == 7 || qrParse == 8 || qrParse == 9 || qrParse == 10 || qrParse == 11 || qrParse == 12)
            CardManager.Instance.ReceiveConsequence((CardManager.ConsequenceCard)qrParse);
    }
}
