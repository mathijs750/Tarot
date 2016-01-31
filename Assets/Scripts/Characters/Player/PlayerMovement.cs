using UnityEngine;
using System.Collections;



public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private AnimationCurve SpeedCurve;
    [SerializeField]
    private CharacterStats stats;
    private float health;
    private int money;

    private Vector3 movementDirection;
    private CharacterController characterController;
    private PlayerSpiteController spritecontroller;
    private RaycastHit slashHit;
    #endregion

    public int Money
    {
        set { money += value; }
        get { return money; }
    }

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementDirection = Vector3.zero;
        spritecontroller = GetComponentInChildren<PlayerSpiteController>();
        health = stats.Health;
    }

    void Update()
    {
        if (GameManager.Instance.CurrentState != GameState.Playing) { return; }

        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * movementDirection;
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);

        PlayerSpiteController.Speed = movementDirection.magnitude;
        if (movementDirection.magnitude > 0.1f)
        {
            PlayerSpiteController.YRotation = Quaternion.LookRotation(-movementDirection, transform.up).eulerAngles.y - 45f;
        }

        if (Input.GetButtonDown("Fire1"))
        {
            spritecontroller.Slash();

            float arcAngle = 90f;
            int numLines= 20;
            Quaternion rot = Quaternion.Euler(0, PlayerSpiteController.YRotation, 0);
            for (int  i = 0; i < numLines; i++)
            {
                Vector3 shootVec = rot  * Quaternion.AngleAxis(arcAngle * (i / numLines) - arcAngle / 2, Vector3.up) * Vector3.forward;
                Debug.DrawRay(transform.position, shootVec, Color.magenta,30f);
                if (Physics.Raycast(transform.position, shootVec, out slashHit, 10.0f))
                {
                    Debug.DrawLine(transform.position, slashHit.point, Color.green);
                    if (slashHit.transform.tag == "Enemy")
                    {
                        slashHit.transform.GetComponent<EnemyMovement>().Hit(stats.AttackDamage);
                    }
                }
            }

        }

        movementDirection *= SpeedCurve.Evaluate(movementDirection.magnitude) * stats.MovmentSpeed;
        characterController.Move(movementDirection * Time.deltaTime);
    }


    public void Hit(float damage)
    {
        health -= damage;
    }
}
