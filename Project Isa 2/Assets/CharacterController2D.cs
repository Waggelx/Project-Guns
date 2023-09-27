using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float speed = 2f;
    Vector2 movement;
    Animator animator;
    
    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent <Animator>();
    }

    private void Update()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
            );
        animator.SetFloat("horizontal", Input.GetAxisRaw("Horizontal"));
        animator.SetFloat("vertical", Input.GetAxisRaw("Vertical"));

        if (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 || Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1 || (Mathf.Abs(Input.GetAxisRaw("Horizontal")) == 1 && Mathf.Abs(Input.GetAxisRaw("Vertical")) == 1))
        {
            animator.SetFloat("LastMoveX", Input.GetAxisRaw("Horizontal"));
            animator.SetFloat("LastMoveY", Input.GetAxisRaw("Vertical"));
            animator.SetFloat("speed", movement.sqrMagnitude);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        rigidbody2d.velocity = movement * speed;
    }
}