
using Photon.Pun;
using Photon.Realtime;
using ExitGames.Client.Photon; // Custom properties için gerekli

using System.IO;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    PhotonView PV;
    GameObject player;


    private string selectedTeam = "";


    void Awake()
    {
        PV = GetComponent<PhotonView>();
    }

    void Start()
    {
        if (PV.IsMine && GameManager.Instance.teamSelectionPanel != null)
        {
            Debug.Log("Paneli actim");
            GameManager.Instance.teamSelectionPanel.SetActive(true);
        }
    }

    public void SelectTeam(string team)
    {
        if (PV.IsMine)
        {
            selectedTeam = team;


            Hashtable hash = new Hashtable();// oyuncunun takim bilgisi CustomProperties ile saklandi, ise yaradi sonunda
            hash["Team"] = team;
            PhotonNetwork.LocalPlayer.SetCustomProperties(hash);

            Debug.Log("Takım seçildi: " + team);
            GameManager.Instance.teamSelectionPanel.SetActive(false);
            CreateController();
        }
    }

    public void CreateController()
    {
        if (string.IsNullOrEmpty(selectedTeam))
        {
            Debug.LogError("Takım seçilmedi!");
            return;
        }

        Transform spawnPoint = GetSpawnPoint();
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn noktası bulunamadı!");
            return;
        }

        string prefabName = selectedTeam == "Red" ? "Player" : "Player2";
        player = PhotonNetwork.Instantiate(
            Path.Combine("PhotonPrefabs", prefabName),
            spawnPoint.position,
            spawnPoint.rotation,
            0,
            new object[] { PV.ViewID }
        );
    }

    public void Die()

    {
        if (PV.IsMine)  
        {
            PhotonNetwork.Destroy(player);
            CreateController();
            
        }
        
    }

    Transform GetSpawnPoint()
    {
        if (selectedTeam == "Red")
        {
            return GameManager.Instance.redTeamSpawns[Random.Range(0, GameManager.Instance.redTeamSpawns.Length)];
        }
        else
        {
            return GameManager.Instance.blueTeamSpawns[Random.Range(0, GameManager.Instance.blueTeamSpawns.Length)];
        }
    }

    public void SelectRedTeam()
    {
        SelectTeam("Red");
    }

    public void SelectBlueTeam()
    {
        SelectTeam("Blue");
    }

    public string GetTeam()
    {
        return selectedTeam;

    }
}