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
    private int preSize = 8;
    [SerializeField]
    private Monster[] Monsters;
    [SerializeField]
    private Vector3 BoxExtent;
    [SerializeField]
    private GameObject player;

    private bool isSpawining;
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
        isSpawining = true;
        StartCoroutine(SpawnTimer(1.5f, 3));
    }

    private IEnumerator SpawnTimer(float minWait, float randomFactor)
    {
        yield return new WaitForSeconds(Random.Range(minWait, minWait * randomFactor));
        var mon = GetMonster();
        mon.GetComponent<EnemyMovement>().Target = player;
        mon.SetActive(true);

        if (isSpawining)
        {
            StartCoroutine(SpawnTimer(minWait, randomFactor));
        }
    }
}
