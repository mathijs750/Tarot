using UnityEngine;
using System.Collections;

public class PlayerSpiteController : MonoBehaviour
{
    [SerializeField]
    private Transform SlashPivot;
    private static Transform _slashPivot;

    private Animator Anim;
    private static Animator SlashAnim;
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
        _slashPivot = SlashPivot;
        Sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        SlashAnim = _slashPivot.GetComponentInChildren<Animator>();
    }

    public static void Slash()
    {
        SlashAnim.SetBool("fire",true);
        Vector3 rot = new Vector3(0, rotationAxis, 0);
        _slashPivot.Rotate(rot); 

        SlashAnim.SetBool("fire", false);
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
