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
            int numLines= 10;
            Quaternion rot = Quaternion.Euler(0, PlayerSpiteController.YRotation-135, 0);
            for (int  i = 1; i < numLines; i++)
            {
                float floatCounter = ((float)i / (float)numLines);
                Vector3 shootVec = rot  * Quaternion.AngleAxis(arcAngle * floatCounter - arcAngle / 2, Vector3.up) * Vector3.forward;
                if (Physics.Raycast(transform.position, shootVec, out slashHit, 3.0f))
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
        spritecontroller.Hurt();
        health -= damage;
        if (health <=0)
        {
            Debug.LogWarning("Speler is dood");
            GameManager.Instance.CurrentState = GameState.Dead;
            spritecontroller.Death();
        }
    }
}
