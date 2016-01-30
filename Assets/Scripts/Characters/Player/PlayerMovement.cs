using UnityEngine;
using System.Collections;

public class PlayerMovement : MonoBehaviour
{
    #region Variables
    [SerializeField]
    private AnimationCurve SpeedCurve;
    [SerializeField]
    private float SpeedMultiplier;
    private bool canRun = true;
    private Vector3 movementDirection;
    private CharacterController characterController;
    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementDirection = Vector3.zero;
    }

    void Update()
    {
        movementDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        movementDirection = transform.TransformDirection(movementDirection);
        movementDirection = Quaternion.Euler(0, Camera.main.transform.rotation.eulerAngles.y, 0) * movementDirection;
        movementDirection = Vector3.ClampMagnitude(movementDirection, 1f);

        if (Input.GetButton("Fire1"))
        {
            movementDirection *= SpeedCurve.Evaluate(movementDirection.magnitude) * SpeedMultiplier;
        }
        else
        {
            movementDirection = Vector3.ClampMagnitude(movementDirection, .5f);
            movementDirection *= SpeedCurve.Evaluate(movementDirection.magnitude) * SpeedMultiplier;
        }

        characterController.Move(movementDirection * Time.deltaTime);

        PlayerSpiteController.Speed = movementDirection.magnitude;
        Vector2 secondStick = new Vector2(Input.GetAxis("Horizontal2"), Input.GetAxis("Vertical2"));

        if (secondStick.x != 0f || secondStick.y !=0f)
        {
            float angle = Mathf.Atan2(secondStick.x, secondStick.y) * Mathf.Rad2Deg;
            PlayerSpiteController.YRotation = angle+180;
        }
    }
}
