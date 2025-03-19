using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using TMPro;

public class Target : MonoBehaviourPunCallbacks
{
    public float health = 100f;
    public PhotonView PV;

    PlayerManager playerManager;
    private TextMeshProUGUI healthText; 
    InGameUIManager gameUIManager;


    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
        healthText = InGameUIManager.Instance.healthText;
    }

    private void Start()
    {
        if (PV.IsMine && healthText != null)
        {
            UpdateHealthUI();
        }
    }

    public void TakeDamage(float amount)
    {
        if (PV.IsMine)
        {
            health -= amount;
            UpdateHealthUI();

            if (health <= 0f)
            {
                Debug.Log("Vuruldu: " + this.gameObject.name);
                string team = playerManager.GetTeam();
                PV.RPC("UpdateScore_RPC", RpcTarget.AllBuffered, team);
                Debug.LogWarning($"Puan RPC çaðrýlýyor, takim: {team}");
                Die();
                
                
                
            }
        }
    }

    void UpdateHealthUI()
    {
        if (healthText != null)
        {
            healthText.text =  health.ToString();
        }
    }

    void Die()
    {
        playerManager.Die();
         
    }

    [PunRPC]
    void UpdateScore_RPC(string team)
    {
        Debug.LogWarning($"UpdateScore_RPC çaðrýldý! takim: {team}");
        InGameUIManager.Instance.UpdateScore(team);
    }
}
