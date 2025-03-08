using Photon.Pun;
using UnityEngine;

public class CharacterMovement : MonoBehaviourPunCallbacks
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public float crouchSpeed = 2f;
    public Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    public AudioSource walkingSound;
    private bool isCrouching = false;

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
        float moveX = Input.GetAxis("Horizontal"); // A (-1) ve D (1)
        float moveZ = Input.GetAxis("Vertical");   // W (1) ve S (-1)
        bool isMoving = moveX != 0 || moveZ != 0;
        bool isShift = Input.GetKey(KeyCode.LeftShift);

        Vector3 moveDirection = transform.right * moveX + transform.forward * moveZ;
        float currentSpeed = isShift ? crouchSpeed : speed;
        rb.velocity = new Vector3(moveDirection.x * currentSpeed, rb.velocity.y, moveDirection.z * currentSpeed);

        if (isMoving && !walkingSound.isPlaying)
        {
            walkingSound.Play();
        }
        else if (!isMoving && walkingSound.isPlaying)
        {
            walkingSound.Stop();
        }

        HandleAnimations(moveX, moveZ, isShift);
    }

    void HandleAnimations(float moveX, float moveZ, bool isShift)
    {
        string animationState = "Idle";

        if (isShift)
        {
            animationState = moveZ > 0 ? "CrouchForward" : moveZ < 0 ? "CrouchBackward" :
                             moveX > 0 ? "CrouchRight" : moveX < 0 ? "CrouchLeft" : "CrouchIdle";
        }
        else
        {
            animationState = moveZ > 0 ? "RunForward" : moveZ < 0 ? "WalkBackward" :
                             moveX > 0 ? "WalkRight" : moveX < 0 ? "WalkLeft" : "Idle";
        }

        animator.Play(animationState);
        PV.RPC("SyncAnimation", RpcTarget.Others, animationState);
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
        animator.Play("Jump");
        isGrounded = false;
        PV.RPC("SyncJumpAnimation", RpcTarget.Others, true);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.Play("Idle");
            PV.RPC("SyncJumpAnimation", RpcTarget.Others, false);
        }
    }

    [PunRPC]
    void SyncAnimation(string animationState)
    {
        animator.Play(animationState);
    }

    [PunRPC]
    void SyncJumpAnimation(bool isJumping)
    {
        if (isJumping)
            animator.Play("Jump");
        else
            animator.Play("Idle");
    }
}
