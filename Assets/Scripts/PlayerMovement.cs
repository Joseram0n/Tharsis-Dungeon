using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    private Vector2 AttackDir;
    private Animator animator;

    private Rigidbody2D rb;

    void Start()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    public void Move(Vector2 direction)
    {
        rb.velocity = new Vector2(direction.x * speed, direction.y * speed);

        if (direction.x != 0 || direction.y != 0) // se mueve
        {
            SetAnimatorMovement(direction);
            AttackDir = direction;
        }
        else
            SetAnimatorMovement(AttackDir);
    }
    private void SetAnimatorMovement(Vector2 direction)
    {

        animator.SetFloat("xDir", direction.x);
        animator.SetFloat("yDir", direction.y);
    }
}
