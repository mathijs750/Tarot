using UnityEngine;
using System.Collections;

public enum EnemyType { Bat, Tentale, Blob }

public class Enemymovement : MonoBehaviour
{
    [SerializeField]
    private float BatSpeed, TentacleSpeed, BlobSpeed;
    private CharacterController characterController;
    private Vector3 movementVector;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementVector = Vector3.zero;
    }

    void Update()
    {

    }
}
