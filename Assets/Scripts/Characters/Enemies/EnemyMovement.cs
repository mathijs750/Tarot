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
    }

    public void Hit(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            IsAlive = false;
        }
    }
}
