using UnityEngine;

public class MouseCamera : MonoBehaviour
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked; // Ýmleci ekranda gizler ve kilitler
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarý-aþaðý dönüþ sýnýrý

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamerayý yukarý-aþaðý döndür
        playerBody.Rotate(Vector3.up * mouseX); // Oyuncuyu sola-saða döndür
    }
}
