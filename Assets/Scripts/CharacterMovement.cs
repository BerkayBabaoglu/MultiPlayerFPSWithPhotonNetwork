using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float speed = 3f;
    public float jumpForce = 5f;
    public Animator animator;
    private Rigidbody rb;
    private bool isGrounded;
    public AudioSource walkingSound;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
        walkingSound = GetComponent<AudioSource>();
        // Rigidbody'nin d�nmesini engelle (FPS karakterleri i�in �nemli)
        rb.freezeRotation = true;
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.W))
        {
            animator.SetBool("isRunning", true);

            // Ses sadece �alm�yorsa �al
            if (!walkingSound.isPlaying)
            {
                walkingSound.Play();
            }

            // Karakterin bakt��� y�ne do�ru hareket etmesi
            Vector3 moveDirection = transform.forward * speed;
            rb.velocity = new Vector3(moveDirection.x, rb.velocity.y, moveDirection.z);
        }
        else
        {
            animator.SetBool("isRunning", false);

            // Hareketi durdur (sadece X-Z ekseninde)
            rb.velocity = new Vector3(0, rb.velocity.y, 0);

            // Y�r�y�� sesini durdur
            if (walkingSound.isPlaying)
            {
                walkingSound.Stop();
            }
        }
    }

    void Update()
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
        animator.SetBool("isJumping",true);
        isGrounded = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        // Yere temas edince isGrounded'� true yap
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            animator.SetBool("isJumping", false);
        }
    }
}