﻿using UnityEngine;
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
    private PlayerSpiteController spritecontroller;
    #endregion

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movementDirection = Vector3.zero;
        spritecontroller = GetComponentInChildren<PlayerSpiteController>();
    }

    void Update()
    {
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
        }

        movementDirection *= SpeedCurve.Evaluate(movementDirection.magnitude) * SpeedMultiplier;
        characterController.Move(movementDirection * Time.deltaTime);
    }
}