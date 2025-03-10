using Photon.Pun;
using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;

    GameObject player;

    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine)
        {
            CreateController();
        }
    }

    void CreateController()
    {

        player = PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "Player2"), Vector3.zero, Quaternion.identity,0,new object[] {PV.ViewID});
    }

    public void Die()
    {
        if (PV.IsMine) {
            PhotonNetwork.Destroy(player);
            CreateController();
        }
        
    }  
}