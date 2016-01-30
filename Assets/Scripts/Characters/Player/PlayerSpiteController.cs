using UnityEngine;
using System.Collections;

public class PlayerSpiteController : MonoBehaviour
{

    private Animator Anim;
    private SpriteRenderer Sr;
    private static float rotationAxis;

    public static float YRotation
    {
        set { if (value > 0) { rotationAxis = value; } }
    }

    // Use this for initialization
    void Start()
    {
        Sr = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
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
