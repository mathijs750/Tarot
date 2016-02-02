using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemySpawner : MonoBehaviour
{
    [System.Serializable]
    private struct Monster
    {
        public GameObject Prefab;
        public EnemyType Type;
    }

    [SerializeField]
    private GameObject Event;


    [SerializeField]
    private int MostersToSpawn, Difficulty;
    private int counter = 0;
    [SerializeField]
    private int preSize = 8;
    [SerializeField]
    private Monster[] Monsters;
    [SerializeField]
    private Vector3 BoxExtent;
    [SerializeField]
    private PlayerMovement player;

    private bool isTriggered = false;

    private CardManager.EventCard MainEvent;
    private CardManager.ConsequenceCard Concequence1;
    private CardManager.ConsequenceCard Concequence2;

    private bool isSpawining = false;
    private List<GameObject> pool;

    public GameObject SpawnMonster(int type)
    {
        var clone = Instantiate<GameObject>(Monsters[type].Prefab);
        pool.Add(clone);
        return clone;
    }

    public void PrePolpulate()
    {
        for (var i = 0; i < preSize; i++)
        {
            var clone = SpawnMonster(Random.Range(0, 2));
            clone.transform.parent = transform;
            clone.transform.position = transform.position + new Vector3(Random.Range(-(BoxExtent.x / 2), BoxExtent.x / 2), 1, Random.Range(-(BoxExtent.z / 2), BoxExtent.z / 2));
            clone.SetActive(false);
        }
    }

    public GameObject GetMonster()
    {
        foreach (var instance in pool)
        {
            if (instance.activeSelf != true)
            {
                return instance;
            }
        }

        var clone = SpawnMonster(Random.Range(0, 2));
        clone.transform.parent = transform;
        clone.transform.position = transform.position + new Vector3(Random.Range(-(BoxExtent.x / 2), BoxExtent.x / 2), 1, Random.Range(-(BoxExtent.z / 2), BoxExtent.z / 2));
        clone.SetActive(false);
        return clone;
    }

    public void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireCube(transform.position, BoxExtent);
    }

    void Awake()
    {
        pool = new List<GameObject>(preSize);
        PrePolpulate();
    }

    private IEnumerator SpawnTimer(float minWait, float randomFactor)
    {
        yield return new WaitForSeconds(Random.Range(minWait, minWait * randomFactor));
        counter++;
        var mon = GetMonster();
        mon.GetComponent<EnemyMovement>().Target = player.gameObject;
        mon.SetActive(true);

        if (counter >= MostersToSpawn)
        {
            isSpawining = false;
            GameManager.Instance.NextEvent();
        }

        if (isSpawining)
        {
            StartCoroutine(SpawnTimer(minWait, randomFactor));
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        Debug.Log("Enter Trigger " + GameManager.Instance.CurrentEvent);

        if (isTriggered) return;

        if (GameManager.Instance.CurrentEvent == 1)
        {

            FirstEvent eventG = Event.GetComponent<FirstEvent>();

            switch (this.Concequence1)
            {
                case CardManager.ConsequenceCard.Armoede:

                    eventG.ShowText("You are poor");

                    break;
                case CardManager.ConsequenceCard.Rijkdom:
                    eventG.ShowText("You are rich");
                    player.Money += 10000;

                    break;
                case CardManager.ConsequenceCard.tRijkdom:
                    eventG.ShowText("You are rich");
                    player.Money += 10000;

                    break;
                case CardManager.ConsequenceCard.PowerDown:
                    eventG.ShowText("You have bad luck");
                    player.stats.AttackDamage -= 4;
                    player.stats.MovmentSpeed -= 2;

                    break;
                case CardManager.ConsequenceCard.PowerUp:
                    eventG.ShowText("You have good luck");

                    player.stats.AttackDamage += 4;
                    player.stats.MovmentSpeed += 2;

                    break;
            }
            switch (this.Concequence2)
            {
                case CardManager.ConsequenceCard.Armoede:

                    eventG.ShowText("You are poor");

                    break;
                case CardManager.ConsequenceCard.Rijkdom:
                    eventG.ShowText("You are rich");
                    player.Money += 10000;

                    break;
                case CardManager.ConsequenceCard.tRijkdom:
                    eventG.ShowText("You are rich");
                    player.Money += 10000;

                    break;
                case CardManager.ConsequenceCard.PowerDown:
                    eventG.ShowText("You have bad luck");
                    player.stats.AttackDamage -= 4;
                    player.stats.MovmentSpeed -= 2;

                    break;
                case CardManager.ConsequenceCard.PowerUp:
                    eventG.ShowText("You have good luck");

                    player.stats.AttackDamage += 4;
                    player.stats.MovmentSpeed += 2;

                    break;
            }
        }

        isTriggered = true;
    }

    IEnumerator WaitTimer()
    {
        yield return new WaitForSeconds(15);
        GameManager.Instance.NextEvent();
    }


    public void Initialize(CardManager.EventCard MainEvent, CardManager.ConsequenceCard Concequence1, CardManager.ConsequenceCard Concequence2)
    {
        this.Concequence1 = Concequence1;
        this.Concequence2 = Concequence2;
        this.MainEvent = MainEvent;


        if (MainEvent == CardManager.EventCard.RampSpoed || MainEvent == CardManager.EventCard.tRampSpoed || GameManager.Instance.CurrentEvent == 3)
        {
            isSpawining = true;
        }
    }
}
