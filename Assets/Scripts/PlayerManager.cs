using Photon.Pun;
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
        Debug.Log("geldim anam geldim");
        if (PV.IsMine)
        {
            Debug.Log("Takým seçildi: " + team);
            selectedTeam = team;
            GameManager.Instance.teamSelectionPanel.SetActive(false);

            CreateController();
        }
    }

    public void CreateController()
    {
        if (string.IsNullOrEmpty(selectedTeam))
        {
            Debug.LogError("Takým seçilmedi!");
            return;
        }

        Transform spawnPoint = GetSpawnPoint();
        if (spawnPoint == null)
        {
            Debug.LogError("Spawn noktasý bulunamadý!");
            return;
        }

        string prefabName = selectedTeam == "Red" ? "Player" : "Player2";
        player = PhotonNetwork.Instantiate(
        Path.Combine("PhotonPrefabs", prefabName),
        spawnPoint.position,
        spawnPoint.rotation,
        0,
        new object[] { PV.ViewID }  // PlayerManager'in ViewID'sini gönderiyoruz
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
}
