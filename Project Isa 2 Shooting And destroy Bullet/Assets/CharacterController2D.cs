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
    public Transform weapon;
    public float offset;

    public Transform shotPoint;
    public GameObject projectile;

    public float timeBetweenShot;
    float nextShotTime;
    
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
        // Weapon rotation
        Vector3 displacement = weapon.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
        float angle = Mathf.Atan2(displacement.y, displacement.x) * Mathf.Rad2Deg;
        weapon.rotation = Quaternion.Euler(0f, 0f, angle + offset);

        // Shooting
        if (Input.GetMouseButtonDown(0))
        {
            if (Time.time > nextShotTime)
            {
                nextShotTime = Time.time + timeBetweenShot;
                Instantiate(projectile, shotPoint.position, shotPoint.rotation);
            }
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