using UnityEngine;
using System.Collections;

public class PlayerSpiteController : MonoBehaviour
{

    private Animator Anim;
    private SpriteRenderer Sr;
    private static float rotationAxis, movementSpeed;

    public static float YRotation
    {
        set { if (value > 0) { rotationAxis = value; } }
    }

    public static float Speed
    {
        set { if (value > 0) { movementSpeed = value; } }
    }

    // Use this for initialization
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
    }

    void Update()
    {
        Anim.SetFloat("Speed", movementSpeed);
        Anim.SetFloat("Rotation", rotationAxis);

        if (rotationAxis > 180f)
        {
            Sr.flipX = true;
        }
        else if (rotationAxis < 180f)
        {
            Sr.flipX = false;
        }
    }
}
