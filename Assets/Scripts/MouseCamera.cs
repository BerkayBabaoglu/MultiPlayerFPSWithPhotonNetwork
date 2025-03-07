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
            GetComponent<Camera>().enabled = false; // Kamerayý kapat
            GetComponent<AudioListener>().enabled = false; // Ses dinleyicisini de kapat
            Destroy(this);
            return;
        }

        Cursor.lockState = CursorLockMode.Locked; // Ýmleci ekranda gizler ve kilitler
        Cursor.visible = false;
    }

    void Update()
    {
        if (PV.IsMine) // Sadece kendi karakterinin kamerasýný kontrol et
        {
            CameraControl();
        }
    }

    void CameraControl()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f); // Yukarý-aþaðý dönüþ sýnýrý

        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f); // Kamerayý yukarý-aþaðý döndür
        playerBody.Rotate(Vector3.up * mouseX); // Oyuncuyu sola-saða döndür
    }
}