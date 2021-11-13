using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D rb2D;
    private Animator animator;

    private bool facingRight = true;

    public float speed = 2.0f;
    public float horizMovement;

    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    
    void Update()
    {
        // check if the player has inpirt movement
        horizMovement = Input.GetAxis("Horizontal");
    }

    private void FixedUpdate()
    {
        // move the charachyer left and right
        rb2D.velocity = new Vector2(horizMovement*speed, rb2D.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(horizMovement));
        Flip(horizMovement);
    }

    private void Flip(float horizontal)
    {
        if (horizontal < 0 && facingRight || horizontal > 0 && !facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }
    }

}
