using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPunCallbacks
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    public AudioSource walkingSound;

    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
        rb = GetComponent<Rigidbody>();

        if (!PV.IsMine)
        {
            
            GetComponent<CharacterMovement>().enabled = false;
            rb.isKinematic = true; 
        }
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        walkingSound = GetComponent<AudioSource>();
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (PV.IsMine)
        {
            Movement();
        }
    }

    void Movement()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
            Vector3 moveDirection = transform.forward * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            animator.SetBool("isRunning", false);
            rb.velocity = new Vector3(0, rb.velocity.y, 0);
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }

    void Update()
    {
        if (PV.IsMine)
        {
            JumpControl();
        }
    }

    void JumpControl()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            walkingSound.Stop();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJumping", true);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}
