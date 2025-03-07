using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // �mleci ekranda gizler ve kilitler
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukar�-a�a�� d�n�� s�n�r�

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kameray� yukar�-a�a�� d�nd�r
        playerBody.Rotate(Vector3.up * mouseX); // Oyuncuyu sola-sa�a d�nd�r
    }
}
