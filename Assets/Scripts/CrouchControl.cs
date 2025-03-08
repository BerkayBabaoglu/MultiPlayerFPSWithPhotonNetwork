using UnityEngine;
using Photon.Pun;

public class WeaponCameraCrouch : MonoBehaviour
{
    public Transform weaponCamera;  // Player prefab içindeki kamera
    private float defaultLocalY = 0.0181f;  // Normal duruþ yüksekliði
    private float crouchLocalY = 0.01431f;  // Eðilme yüksekliði
    private float defaultLocalZ = 0.003675629f;  // Normal duruþ Z konumu
    private float crouchLocalZ = 0.00551f;  // Eðilme Z konumu
    public float transitionSpeed = 10f;  // Yumuþak geçiþ hýzý


    void Update()
    {
        if (weaponCamera == null)
        {
            Debug.LogError("WeaponCamera atanmadý! Inspector'dan ekleyin.");
            return;
        }

        bool isCrouching = Input.GetKey(KeyCode.LeftShift);
        float targetY = isCrouching ? crouchLocalY : defaultLocalY;
        float targetZ = isCrouching ? crouchLocalZ : defaultLocalZ;

        // Debug ile deðiþimin olup olmadýðýný kontrol et
        Debug.Log($"isCrouching: {isCrouching}, TargetY: {targetY}, TargetZ: {targetZ}, CurrentY: {weaponCamera.localPosition.y}, CurrentZ: {weaponCamera.localPosition.z}");

        Vector3 newLocalPosition = new Vector3(
            weaponCamera.localPosition.x,
            targetY,
            targetZ
        );

        // Yumuþak geçiþ için Lerp
        weaponCamera.localPosition = Vector3.Lerp(
            weaponCamera.localPosition,
            newLocalPosition,
            Time.deltaTime * transitionSpeed
        );


    }
}