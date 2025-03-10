using UnityEngine;
using Photon.Pun;

public class WeaponCameraCrouch : MonoBehaviour
{
    public Transform weaponCamera;  // Player prefab içindeki kamera
    public float transitionSpeed = 10f;  // Yumuþak geçiþ hýzý

    // Soldier1 için kamera pozisyonlarý
    private float soldier1DefaultLocalY = 0.0181f;
    private float soldier1CrouchLocalY = 0.01431f;
    private float soldier1DefaultLocalZ = 0.003675629f;
    private float soldier1CrouchLocalZ = 0.00551f;

    // Soldier2 için kamera pozisyonlarý
    private float soldier2DefaultLocalY = 1.208f;
    private float soldier2CrouchLocalY = 0.898f;
    private float soldier2DefaultLocalZ = 0.316f;
    private float soldier2CrouchLocalZ = 0.00551f;


    void Start()
    {
        
    }

    void Update()
    {
        if (weaponCamera == null)
        {
            return;
        }

        bool isCrouching = Input.GetKey(KeyCode.LeftShift);
        float targetY, targetZ;


        Debug.Log("Layer: " + this.gameObject.layer);

        if (this.gameObject.layer == 7)
        {
            Debug.Log("girdim");
            targetY = isCrouching ? soldier1CrouchLocalY : soldier1DefaultLocalY;
            targetZ = isCrouching ? soldier1CrouchLocalZ : soldier1DefaultLocalZ;
        }
        else if (this.gameObject.layer == 8)
        {
            targetY = isCrouching ? soldier2CrouchLocalY : soldier2DefaultLocalY;
            targetZ = isCrouching ? soldier2CrouchLocalZ : soldier2DefaultLocalZ;
        }
        else
        {
            return; 
        }

        Vector3 newLocalPosition = new Vector3(
            weaponCamera.localPosition.x,
            targetY,
            targetZ
        );


        weaponCamera.localPosition = Vector3.Lerp(
            weaponCamera.localPosition,
            newLocalPosition,
            Time.deltaTime * transitionSpeed
        );
    }
}
