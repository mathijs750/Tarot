using UnityEngine;
using System.Collections;

public class CardManager : MonoBehaviour {

    private static CardManager instance;
    public static CardManager Instance {
        get
        {
            if (instance == null)
            {
                instance = new GameObject("CardManager").AddComponent<CardManager>();

            }

            return instance;
        }
    }
    
    public enum WeatherCard
    {
        Dag = 13,
        Nacht = 14,
        Mooi = 15, 
        Slecht = 16
    }

    public enum EventCard
    {
        RampSpoed = 1,
        tRampSpoed = 2,
        Geluk = 3,
        tGeluk = 4,
        Pech = 5,
        tPech = 6
    }

    public enum ConsequenceCard
    {
        Armoede = 7,
        tArmoede = 8,
        Rijkdom = 9,
        tRijkdom = 10,
        PowerUp = 11,
        PowerDown = 12
    }

    private WeatherCard weather;
    public WeatherCard SetWeather { get { return weather; } }

    private EventCard eventCard;
    public EventCard SetEventCard { get { return eventCard; } }

    private ConsequenceCard[] consequences = new ConsequenceCard[2];
    public ConsequenceCard[] SetConsequences { get { return consequences; } }

    private GameObject objectWeather;
    private bool waitingWeather = false;
    private bool receiveWeather = false;

    private GameObject objectEvent;
    private bool waitingEvent = false;
    private bool receiveEvent = false;

    private GameObject objectConsequenceOne;
    private bool waitingConsequenceOne = false;
    private bool receiveConsequenceOne = false;
    private GameObject objectConsequenceTwo;
    private bool waitingConsequenceTwo = false;
    private bool receiveConsequenceTwo = false;

    public void ReadCards()
    {
        StartCoroutine(ReadCardEvent());
    }

    public void ReceiveWeather(WeatherCard weatherCard)
    {
        if (!waitingWeather) return;

        weather = weatherCard;

        objectWeather = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_" + (int)weatherCard));
        CardAnimation anim = objectWeather.AddComponent<CardAnimation>();
        anim.StartTransition(0);

        receiveWeather = true;
    }

    public void ReceiveEvent(EventCard eventCard)
    {
        if (!waitingEvent) return;

        this.eventCard = eventCard;

        objectEvent = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_" + (int)eventCard));
        CardAnimation anim = objectEvent.AddComponent<CardAnimation>();
        anim.StartTransition(1);

        receiveEvent = true;
    }

    public void ReceiveConsequence(ConsequenceCard consequence)
    {
        if (waitingConsequenceOne)
        {
            consequences[0] = consequence;

            objectConsequenceOne = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_" + (int)consequence));
            CardAnimation anim = objectConsequenceOne.AddComponent<CardAnimation>();
            anim.StartTransition(2);

            receiveConsequenceOne = true;
        }
        else if (waitingConsequenceTwo)
        {
            consequences[1] = consequence;

            objectConsequenceTwo = Instantiate(Resources.Load<GameObject>("Prefabs/Cards/Card_" + (int)consequence));
            CardAnimation anim = objectConsequenceTwo.AddComponent<CardAnimation>();
            anim.StartTransition(3);

            receiveConsequenceTwo = true;
        }
    }

    private IEnumerator ReadCardEvent()
    {
        TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GameToGlobe);

        yield return new WaitForSeconds(2.5f);

        TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeGameToCam);

        yield return new WaitForSeconds(2.5f);

        GameObject.FindObjectOfType<WebCam>().StartQR();

        Debug.Log("Waiting For Weather");

        waitingWeather = true;
        while (!receiveWeather)
            yield return null;
        waitingWeather = false;

        Debug.Log("End Weather");

        GameObject.FindObjectOfType<WebCam>().EndQR();

        yield return new WaitForSeconds(2.5f);

        GameObject.FindObjectOfType<WebCam>().StartQR();

        Debug.Log("Waiting For Event");

        waitingEvent = true;
        while (!receiveEvent)
            yield return null;
        waitingEvent = false;

        Debug.Log("End Event");

        GameObject.FindObjectOfType<WebCam>().EndQR();

        yield return new WaitForSeconds(2.5f);

        GameObject.FindObjectOfType<WebCam>().StartQR();

        Debug.Log("Waiting For Consequence 1");

        waitingConsequenceOne = true;
        while (!receiveConsequenceOne)
            yield return null;
        waitingConsequenceOne = false;

        Debug.Log("End Consequence 1");

        GameObject.FindObjectOfType<WebCam>().EndQR();

        yield return new WaitForSeconds(2.5f);

        GameObject.FindObjectOfType<WebCam>().StartQR();

        Debug.Log("Waiting For Consequence 2");

        waitingConsequenceTwo = true;
        while (!receiveConsequenceTwo)
            yield return null;
        waitingConsequenceTwo = false;

        Debug.Log("End Consequence 2");

        GameObject.FindObjectOfType<WebCam>().EndQR();

        yield return new WaitForSeconds(2.5f);

        objectWeather.GetComponent<CardAnimation>().StartSpiral();
        objectWeather = null;

        yield return new WaitForSeconds(1);

        objectEvent.GetComponent<CardAnimation>().StartSpiral();
        objectEvent = null;

        yield return new WaitForSeconds(1);

        objectConsequenceOne.GetComponent<CardAnimation>().StartSpiral();
        objectConsequenceOne = null;

        yield return new WaitForSeconds(1);

        objectConsequenceTwo.GetComponent<CardAnimation>().StartSpiral();
        objectConsequenceTwo = null;

        GameManager.Instance.ReceiveCards(weather, eventCard, consequences[1], consequences[2]);

        yield return new WaitForSeconds(4);

        TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeCamToGame);

        yield return new WaitForSeconds(2.5f);

        TransitionManager.Instance.StartTransition(TransitionManager.TransitionType.GlobeToGame);

        yield return new WaitForSeconds(2.5f);

        receiveWeather = false;
        receiveEvent = false;
        receiveConsequenceOne = false;
        receiveConsequenceTwo = false;
    }
}
