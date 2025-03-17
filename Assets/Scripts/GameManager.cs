using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;
using Photon.Realtime;
using ExitGames.Client.Photon;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;

    public GameObject teamSelectionPanel;
    public Transform[] redTeamSpawns;
    public Transform[] blueTeamSpawns;

    public GameObject winPanel;
    public GameObject losePanel;

    public int redScore = 0;
    public int blueScore = 0;

    private const byte ShowResultsEventCode = 1;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        if (InGameUIManager.Instance != null)
        {
            redScore = InGameUIManager.Instance.GetRedScore();
            blueScore = InGameUIManager.Instance.GetBlueScore();
        }
        else
        {
            Debug.LogError("InGameUIManager bulunamadı! Skorlar sıfır olabilir.");
        }

        if (winPanel != null) winPanel.SetActive(false);
        if (losePanel != null) losePanel.SetActive(false);

    }

    public void EndGame()
    {
        if (!PhotonNetwork.IsMasterClient) return; // sonucu master client'a gore belirleme

        redScore = InGameUIManager.Instance != null ? InGameUIManager.Instance.GetRedScore() : redScore;
        blueScore = InGameUIManager.Instance != null ? InGameUIManager.Instance.GetBlueScore() : blueScore;

        string winnerTeam = redScore > blueScore ? "Red" : "Blue";

        ExitGames.Client.Photon.Hashtable roomProperties = new ExitGames.Client.Photon.Hashtable();
        roomProperties["WinnerTeam"] = winnerTeam;
        PhotonNetwork.CurrentRoom.SetCustomProperties(roomProperties);

        object[] content = new object[] { winnerTeam };
        RaiseEventOptions raiseEventOptions = new RaiseEventOptions { Receivers = ReceiverGroup.All };
        SendOptions sendOptions = new SendOptions { Reliability = true };

        PhotonNetwork.RaiseEvent(ShowResultsEventCode, content, raiseEventOptions, sendOptions);
    }

    [PunRPC]
    private void ShowResultsRPC(string winnerTeam)
    {
        string playerTeam = "Unknown";
        if (PhotonNetwork.LocalPlayer.CustomProperties.ContainsKey("Team"))
        {
            playerTeam = (string)PhotonNetwork.LocalPlayer.CustomProperties["Team"];
        }

        Debug.Log($"Oyuncu: {PhotonNetwork.LocalPlayer.NickName}, Takimi: {playerTeam}, Kazanan Takim: {winnerTeam}");

        if (playerTeam == winnerTeam)
        {
            winPanel.SetActive(true);
        }
        else
        {
            losePanel.SetActive(true);
        }

        StartCoroutine(ReturnToLobby());
    }

    private IEnumerator ReturnToLobby()
    {
        yield return new WaitForSeconds(5f);

        if (PhotonNetwork.IsMasterClient)
        {
            PhotonNetwork.CurrentRoom.IsOpen = false;
            PhotonNetwork.CurrentRoom.IsVisible = false;
            PhotonNetwork.CurrentRoom.EmptyRoomTtl = 0;
        }

        PhotonNetwork.LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        PhotonNetwork.LoadLevel(1);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public override void OnEnable()
    {
        base.OnEnable();
        PhotonNetwork.NetworkingClient.EventReceived += OnEvent;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        PhotonNetwork.NetworkingClient.EventReceived -= OnEvent;
    }

    private void OnEvent(EventData photonEvent)
    {
        if (photonEvent.Code == ShowResultsEventCode)
        {
            object[] data = (object[])photonEvent.CustomData;
            string winnerTeam = (string)data[0];
            ShowResultsRPC(winnerTeam);
        }
    }
}
