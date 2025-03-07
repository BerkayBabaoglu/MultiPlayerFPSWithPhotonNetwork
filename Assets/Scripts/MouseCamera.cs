using Photon.Pun;
using UnityEngine;

public class MouseCamera : MonoBehaviourPunCallbacks
{
    public float mouseSensitivity = 100f;
    public Transform playerBody;

    private float xRotation = 0f;

    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (!PV.IsMine)
        {
            GetComponent<Camera>().enabled = false; // Kameray� kapat
            GetComponent<AudioListener>().enabled = false; // Ses dinleyicisini de kapat
            Destroy(this);
            return;
        }

        Cursor.lockState = CursorLockMode.Locked; // �mleci ekranda gizler ve kilitler
        Cursor.visible = false;
    }

    void Update()
    {
        if (PV.IsMine) // Sadece kendi karakterinin kameras�n� kontrol et
        {
            CameraControl();
        }
    }

    void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukar�-a�a�� d�n�� s�n�r�

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kameray� yukar�-a�a�� d�nd�r
        playerBody.Rotate(Vector3.up * mouseX); // Oyuncuyu sola-sa�a d�nd�r
    }
}