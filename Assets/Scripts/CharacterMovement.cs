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
        bool isMoving = Input.GetKey(KeyCode.W);

        if (isMoving)
        {
            Vector3 moveDirection = transform.forward * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);

            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }
        }
        else
        {
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }

        if (PV.IsMine)
        {
            PV.RPC("SyncAnimation", RpcTarget.Others, isMoving);
        }

        animator.SetBool("isRunning", isMoving);
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
        PV.RPC("SyncJumpAnimation", RpcTarget.Others, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
            PV.RPC("SyncJumpAnimation", RpcTarget.Others, false);
        }
    }

    [PunRPC]
    void SyncAnimation(bool isRunning) //animasyonlarý senkronize ederiz
    {
        animator.SetBool("isRunning", isRunning);
    }

    [PunRPC]
    void SyncJumpAnimation(bool isJumping)
    {
        animator.SetBool("isJumping", isJumping);
    }
}
