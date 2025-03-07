using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    public AudioSource walkingSound;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        walkingSound = GetComponent<AudioSource>();
        // Rigidbody'nin dönmesini engelle (FPS karakterleri için önemli)
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);

            // Ses sadece çalmýyorsa çal
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }

            // Karakterin baktýðý yöne doðru hareket etmesi
            Vector3 moveDirection = transform.forward * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            animator.SetBool("isRunning", false);

            // Hareketi durdur (sadece X-Z ekseninde)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // Yürüyüþ sesini durdur
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }

    void Update()
    {
        if (PV.IsMine) return;
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            Jump();
            walkingSound.Stop();
        }
    }

    void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        animator.SetBool("isJumping",true);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Yere temas edince isGrounded'ý true yap
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}