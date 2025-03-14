using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class TeamSelectionUI : MonoBehaviour
{
    public Button redTeamButton;
    public Button blueTeamButton;
    private PlayerManager playerManager;

    private void Start()
    {
        // Sahnedeki tüm PlayerManager nesnelerini bul
        PlayerManager[] allPlayerManagers = FindObjectsOfType<PlayerManager>();

        foreach (var pm in allPlayerManagers)
        {
            // Eðer bu PlayerManager bana aitse, onu kullan
            if (pm.GetComponent<PhotonView>().IsMine)
            {
                playerManager = pm;
                break;
            }
        }

        if (playerManager != null)
        {
            // Butonlarý kendi PlayerManager'ýmýz ile baðla
            redTeamButton.onClick.AddListener(playerManager.SelectRedTeam);
            blueTeamButton.onClick.AddListener(playerManager.SelectBlueTeam);
        }
        else
        {
            Debug.LogError("Kendi PlayerManager nesnem bulunamadý!");
        }
    }
}