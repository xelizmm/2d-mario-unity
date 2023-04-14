using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterController : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpForce = 300f;

    private Rigidbody2D rigidbody2D;
    private Animator animator;

    private bool grounded;
    private bool started;
    private bool jumping;

    private void Awake()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        grounded = true;
    }

    private void Update()
    {
        if (Input.GetKeyDown("space"))
        {
            if (started && grounded)
            {
                animator.SetTrigger("Jump");
                grounded = false;
                jumping = true;
            }
            else
            {
                animator.SetBool("GameStarted", true);
                started = true;
            }
        }

        animator.SetBool("Grounded",grounded);
    }

    private void FixedUpdate()
    {
        if (started)
        {
            rigidbody2D.velocity = (new Vector2(speed, rigidbody2D.velocity.y));
        }
        
        if(jumping)
        {
            rigidbody2D.AddForce(new Vector2(0f, jumpForce));
            jumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("ground"))
        {
            grounded = true;
        }
    }
}
