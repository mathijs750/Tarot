using UnityEngine;
using System.Collections;

public enum EnemyType { Bat, Tentale, Blob }

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private CharacterStats stats;
    private float health;
    private bool IsAlive;

    [SerializeField]
    private EnemyType type;
    private CharacterController characterController;
    private Vector3 movementVector;
    private GameObject target;

    public GameObject Target
    {
        set { if (value.tag == "Player") { target = value; } }
    }

    void OnEnable()
    {
        characterController = GetComponent<CharacterController>();
        GetComponent<CapsuleCollider>().enabled = true;
        movementVector = Vector3.zero;
        health = stats.Health;
        IsAlive = true;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing || !IsAlive) { return; }

        transform.LookAt(target.transform);
        movementVector = transform.forward;
        movementVector *= stats.MovmentSpeed;
        characterController.Move(movementVector * Time.deltaTime);

        if (Vector3.Distance(target.transform.position, transform.position) < 1.3f)
        {
            StartCoroutine(HurtPlayer());
        }
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            IsAlive = false;
            GetComponent<CapsuleCollider>().enabled = false;
            StartCoroutine(DespawnTimer(2));
        }
    }

    IEnumerator DespawnTimer (float time)
    {
        yield return new WaitForSeconds(time);
        gameObject.SetActive(false);
    }

    IEnumerator HurtPlayer()
    {
        yield return new WaitForSeconds(1f);
        target.GetComponent<PlayerMovement>().Hit(stats.AttackDamage);
        Debug.Log(" Give player "  + stats.AttackDamage + "  damage" );

        if (IsAlive && Vector3.Distance(target.transform.position, transform.position) < 1.3f && GameManager.Instance.CurrentState == GameState.Playing)
        {
            StartCoroutine(HurtPlayer());
        }
    }
}
