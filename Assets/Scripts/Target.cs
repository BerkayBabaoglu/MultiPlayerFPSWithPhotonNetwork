using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPunCallbacks
{
    public float health = 100f;

    PlayerManager playerManager;
    PhotonView PV;

    private void Awake()
    {
        PV = GetComponent<PhotonView>();
        playerManager = PhotonView.Find((int)PV.InstantiationData[0]).GetComponent<PlayerManager>();
    }

    public void TakeDamage(float amount)
    {
        if (PV.IsMine)
        {
            health -= amount;
            if (health <= 0f)
            {
                Debug.Log("vuruldu: " + this.gameObject.name);
                Die();
            }
        }
        
    }

    void Die()
    {
        playerManager.Die();
    }
}
