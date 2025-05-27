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
        // Sahnedeki t�m PlayerManager nesnelerini bul
        PlayerManager[] allPlayerManagers = FindObjectsOfType<PlayerManager>();

        foreach (var pm in allPlayerManagers)
        {
            // E�er bu PlayerManager bana aitse, onu kullan
            if (pm.GetComponent<PhotonView>().IsMine)
            {
                playerManager = pm;
                break;
            }
        }

        if (playerManager != null)
        {
            // Butonlar� kendi PlayerManager'�m�z ile ba�la
            redTeamButton.onClick.AddListener(playerManager.SelectRedTeam);
            blueTeamButton.onClick.AddListener(playerManager.SelectBlueTeam);
        }
        else
        {
            Debug.LogError("Kendi PlayerManager nesnem bulunamad�!");
        }
    }
}