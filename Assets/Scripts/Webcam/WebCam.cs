using UnityEngine;
using System.Collections;
using System.Threading;

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

                Debug.Log(new QRCodeReader().decode(d, W, H).Text);
            }
            catch {}

            yield return null;
        }
    }
}
