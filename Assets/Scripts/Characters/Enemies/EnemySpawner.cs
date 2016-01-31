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
    [SerializeField]
    private int preSize = 8;
    [SerializeField]
    private Monster[] Monsters;
    [SerializeField]
    private Vector3 BoxExtent;
    [SerializeField]
    private PlayerMovement player;

    private CardManager.EventCard MainEvent;
    private CardManager.ConsequenceCard Concequence;

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
        var mon = GetMonster();
        mon.GetComponent<EnemyMovement>().Target = player.gameObject;
        mon.SetActive(true);

        if (isSpawining)
        {
            StartCoroutine(SpawnTimer(minWait, randomFactor));
        }
    }

    void Initialize(CardManager.EventCard MainEvent, CardManager.ConsequenceCard Concequence)
    {
        this.Concequence = Concequence;
        this.MainEvent = MainEvent;


        if (MainEvent == CardManager.EventCard.RampSpoed || MainEvent == CardManager.EventCard.tRampSpoed )//|| GameManager.currentEvent == 3)
        {
            isSpawining = true;
        }

        switch (Concequence)
        {
            case CardManager.ConsequenceCard.Armoede:

                break;
            case CardManager.ConsequenceCard.Rijkdom:
                player.Money += 10000;
                break;
            case CardManager.ConsequenceCard.tRijkdom:
                player.Money += 10000;
                break;
            case CardManager.ConsequenceCard.PowerDown:
                player.stats.AttackDamage -= 4;
                player.stats.MovmentSpeed -= 2;
                break;
            case CardManager.ConsequenceCard.PowerUp:
                player.stats.AttackDamage += 4;
                player.stats.MovmentSpeed += 2;
                break;
        }
    }
}
