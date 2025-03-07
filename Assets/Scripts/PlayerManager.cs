using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)  // E�er bu oyuncu bu istemciye aitse
        {
            CreateController();
        }
    }

    void CreateController()
    {
        // Bu istemci i�in kendi player'�n� instantiate et
        GameObject player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player"), Vector3.zero, Quaternion.identity);

        // Sadece bu istemci kendi karakterini kontrol edebilir
        if (player.GetComponent<PhotonView>().IsMine)
        {
            Debug.Log("Player instantiated successfully for this client.");
        }
    }
}