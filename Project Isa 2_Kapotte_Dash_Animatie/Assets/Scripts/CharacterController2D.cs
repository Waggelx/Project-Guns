using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterController2D : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    [SerializeField] float baseSpeed = 2f;
    float speed; // Huidige snelheid inclusief versnelling
    Vector2 movement;
    Vector2 accelerationDirection; // Richting tijdens versnelling
    Animator animator;
    public Transform weapon;
    public float offset;

    public Transform shotPoint;
    public GameObject projectile;

    public float timeBetweenShot;
    float nextShotTime;

    public float accelerationMultiplier = 2f;
    public float accelerationDuration = 2f;
    private bool isAccelerating = false;
    private float accelerationTimer;

    void Awake()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        speed = baseSpeed;
    }

    void Update()
    {
        movement = new Vector2(
            Input.GetAxisRaw("Horizontal"),
            Input.GetAxisRaw("Vertical")
        );

        animator.SetFloat("horizontal", movement.x);
        animator.SetFloat("vertical", movement.y);

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

        // Dash
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isAccelerating)
            {
                StartAcceleration();
            }
        }

        // Herstel de snelheid als de versnellingstimer afloopt
        if (isAccelerating && accelerationTimer <= 0)
        {
            StopAcceleration();
        }
    }

    void FixedUpdate()
    {
        Move();

        // Verminder de versnellingstimer
        if (isAccelerating)
        {
            accelerationTimer -= Time.fixedDeltaTime;
        }
    }

    void Move()
    {
        rigidbody2d.velocity = movement.normalized * speed;
    }

    void StartAcceleration()
    {
        isAccelerating = true;
        accelerationTimer = accelerationDuration;
        accelerationDirection = movement.normalized;
        speed = baseSpeed * accelerationMultiplier;

        //animator.SetBool("IsAccelerating", true);
    }

    void StopAcceleration()
    {
        isAccelerating = false;
        speed = baseSpeed;

        //animator.SetBool(IsAccelerating, false);
    }
}