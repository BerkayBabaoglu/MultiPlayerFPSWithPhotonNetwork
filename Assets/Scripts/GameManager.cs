using UnityEngine;
using Photon.Pun;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviourPunCallbacks
{
    public static GameManager Instance;
    
    public GameObject teamSelectionPanel;
    public Transform[] redTeamSpawns;
    public Transform[] blueTeamSpawns;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    
}
