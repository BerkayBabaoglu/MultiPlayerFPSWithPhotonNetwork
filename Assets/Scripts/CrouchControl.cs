using UnityEngine;
using Photon.Pun;

public class WeaponCameraCrouch : MonoBehaviour
{
    public Transform weaponCamera;  // Player prefab i�indeki kamera
    public float transitionSpeed = 10f;  // Yumu�ak ge�i� h�z�

    // Soldier1 i�in kamera pozisyonlar�
    private float soldier1DefaultLocalY = 0.0181f;
    private float soldier1CrouchLocalY = 0.01431f;
    private float soldier1DefaultLocalZ = 0.003675629f;
    private float soldier1CrouchLocalZ = 0.00551f;

    // Soldier2 i�in kamera pozisyonlar�
    private float soldier2DefaultLocalY = 1.208f;
    private float soldier2CrouchLocalY = 0.316f;
    private float soldier2DefaultLocalZ = 0.898f;
    private float soldier2CrouchLocalZ = 0.00551f;

    private string playerTag;

    void Start()
    {
        // Oyuncunun tag'ini belirle
        playerTag = gameObject.tag;
    }

    void Update()
    {
        if (weaponCamera == null)
        {
            return;
        }

        bool isCrouching = Input.GetKey(KeyCode.LeftShift);
        float targetY, targetZ;

        // E�er Soldier1 ise ona g�re de�erleri kullan
        if (playerTag == "Soldier1")
        {
            targetY = isCrouching ? soldier1CrouchLocalY : soldier1DefaultLocalY;
            targetZ = isCrouching ? soldier1CrouchLocalZ : soldier1DefaultLocalZ;
        }
        // E�er Soldier2 ise ona g�re de�erleri kullan
        else if (playerTag == "Soldier2")
        {
            targetY = isCrouching ? soldier2CrouchLocalY : soldier2DefaultLocalY;
            targetZ = isCrouching ? soldier2CrouchLocalZ : soldier2DefaultLocalZ;
        }
        else
        {
            return; // Tan�ms�z bir tag varsa ��k
        }

        Vector3 newLocalPosition = new Vector3(
            weaponCamera.localPosition.x,
            targetY,
            targetZ
        );

        // Kamera pozisyonunu yumu�ak bir �ekilde de�i�tir
        weaponCamera.localPosition = Vector3.Lerp(
            weaponCamera.localPosition,
            newLocalPosition,
            Time.deltaTime * transitionSpeed
        );
    }
}
