using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private Rigidbody rb;
    [SerializeField] private GroundCheck canJump;
    [SerializeField] private float speed = 3f;
    [SerializeField] private float jumpHeight = 2f;
    [SerializeField] private int maxJumps = 2;
    [SerializeField] private float SpeedRotation = 720f;
    [SerializeField] private float speedMultiplier = 2f;
    private float horizontal, vertical;
    private Vector3 dir;
    private int jumpCounter;
    private float multipliedSpeed;
    private float initialSpeed;


    private void Awake()
    {
        if (rb == null) rb = GetComponent<Rigidbody>();
        if (canJump == null) canJump = GetComponent<GroundCheck>();
    }

    private void Start()
    {
        initialSpeed = speed;
        multipliedSpeed = speed * speedMultiplier;
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");

        dir = new Vector3(horizontal, 0f, vertical).normalized;

        if (canJump.CheckIsGrounded() == true)
        {
            jumpCounter = 0;
        }

        PerformJump();

        if (Input.GetButton("Fire3"))
        {
            speed = multipliedSpeed;
        }
        if (Input.GetButtonUp("Fire3"))
        {
            speed = initialSpeed;
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + dir * speed * Time.fixedDeltaTime);

        if (dir != Vector3.zero)           //se c'e movimento
        {
            Quaternion targetRotation = Quaternion.LookRotation(dir);       //calcola il target della rotazione

            rb.MoveRotation(Quaternion.RotateTowards(rb.rotation, targetRotation, SpeedRotation * Time.fixedDeltaTime));    //rotazione attuale, target rotazione, velocita rotazione
        }
    }

    private void PerformJump()
    {
        if (jumpCounter < maxJumps - 1)
        {
            if (Input.GetButtonDown("Jump"))
            {
                rb.AddForce(Vector3.up * jumpHeight, ForceMode.Impulse);
                jumpCounter++;
            }
        }
        return;
    }
}