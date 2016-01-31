using UnityEngine;
using System.Collections;

public class PlayerSpiteController : MonoBehaviour
{
    [SerializeField]
    private Transform SlashPivot;
    private Animator Anim;
    private Animator SlashAnim;
    private SpriteRenderer Sr;
    private static float rotationAxis, movementSpeed;

    public static float YRotation
    {
        set { if (value > 0) { rotationAxis = value; } }
        get { return rotationAxis; }
    }

    public static float Speed
    {
        set {  movementSpeed = value;  }
    }

    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
        Anim = GetComponent<Animator>();
        SlashAnim = SlashPivot.GetComponentInChildren<Animator>();
    }

    public void Slash()
    {
        SlashAnim.SetTrigger("fire");
        Anim.SetTrigger("Attack");
    }

    public void Death()
    {
        Anim.SetTrigger("Dead");
    }

    public void Hurt()
    {
        Anim.SetTrigger("Hurt");
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



        Vector3 rot = new Vector3(0, rotationAxis, 0);
        SlashPivot.rotation = Quaternion.Euler(rot);

    }
}
