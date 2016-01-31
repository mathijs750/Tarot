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
    public int CurrentEvent {  get { return currentEvent; } }

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

    public void ReceiveCards(CardManager.WeatherCard weatherCard, CardManager.EventCard eventCard, CardManager.ConsequenceCard consequenceOne, CardManager.ConsequenceCard consequenceTwo)
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

        // Send Cards to Event or sumthin

        currentEvent += 1;
    }

    public void NextEvent()
    {
        CardManager.Instance.ReadCards();
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
        GameObject fakeGlobe = GameObject.Find("FakeGlobe");
        Destroy(GameObject.Find("StartText"));

        while (counter < 1)
        {
            globeCamera.fieldOfView = Mathf.Lerp(80, 25, counter);
            counter += Time.deltaTime;
            fakeGlobe.GetComponent<Renderer>().material.color = new Color(1, 1, 1, Mathf.Lerp(1, 0, counter));
            yield return null;
        }

        gameStarted = true;

        Destroy(GameObject.Find("FakeGlobe"));

        yield return new WaitForSeconds(5);
    }
}
