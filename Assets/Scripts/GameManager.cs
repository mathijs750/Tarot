using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public enum GameState { Init, Playing, Dead, Paused }

public class GameManager : MonoBehaviour
{

    private static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("GameManager").AddComponent<GameManager>();
            }

            return instance;
        }
    }
    
    private static GameState _state, _pervState;
    public GameState CurrentState
    {
        get { return _state; }
        set
        {
            _pervState = _state;
            _state = value;
        }
    }

    private bool startupComplete = false;
    private bool gameStarted = false;

    private int currentEvent = 1;

    void Awake()
    {
        _state = GameState.Paused;

        StartCoroutine(GameStartup());
    }


    private GameManager() { instance = this; }


    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (startupComplete && !gameStarted)
                StartCoroutine(StartGame());
            else if (gameStarted)
                NextEvent();
        }
    }

    public void ReceiveCards(CardManager.WeatherCard weatherCard)
    {
        switch (weatherCard)
        {
            case CardManager.WeatherCard.Dag:
                GameObject.Find("Darkness").GetComponent<Image>().color = new Color(0, 0, 0, .1f);
                break;
            case CardManager.WeatherCard.Nacht:
                GameObject.Find("Darkness").GetComponent<Image>().color = new Color(0, 0, 0, .8f);
                break;
            case CardManager.WeatherCard.Mooi:
                GameObject.Find("Darkness").GetComponent<Image>().color = new Color(0, 0, 0, 0);
                break;
            case CardManager.WeatherCard.Slecht:
                GameObject.Find("Darkness").GetComponent<Image>().color = new Color(0, 0, 0, .4f);
                break;
        }

        currentEvent += 1;
    }

    public void NextEvent()
    {
        CardManager.Instance.ReadCards();
    }

    public void SendCameraToArea()
    {
        GameObject gameCam = GameObject.Find("GameCamera");
        gameCam.transform.position = new Vector3(-500 + (500 * currentEvent), 150, -10);
    }

    private IEnumerator GameStartup()
    {
        Camera globeCamera = GameObject.Find("UICamera").GetComponent<Camera>();
        globeCamera.depth = -11;

        GameObject webcam = GameObject.Find("Webcam");
        webcam.SetActive(false);

        yield return new WaitForSeconds(1);

        float counter = 1;
        Image splashImage = GameObject.Find("SplashScreen").GetComponent<Image>();
        while (counter > 0)
        {
            splashImage.color = new Color(1, 1, 1, counter);
            counter -= Time.deltaTime;
            yield return null;
        }
        splashImage.gameObject.SetActive(false);

        globeCamera.depth = 1;
        webcam.SetActive(true);

        startupComplete = true;
    }

    private IEnumerator StartGame()
    {
        yield return new WaitForSeconds(0.5f);

        float counter = 0;
        Camera globeCamera = GameObject.Find("UICamera").GetComponent<Camera>();

        while (counter < 1)
        {
            globeCamera.fieldOfView = Mathf.Lerp(80, 60, counter);
            counter += Time.deltaTime;
            yield return null;
        }

        gameStarted = true;

        yield return new WaitForSeconds(5);

        //GameObject.Find()
    }
}
