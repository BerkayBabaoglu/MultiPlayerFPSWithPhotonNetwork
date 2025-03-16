using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Target : MonoBehaviourPunCallbacks
{
    public float health = 100f;

    public PhotonView PV; // public yapildi

    PlayerManager playerManager;

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
                Debug.Log("Vuruldu: " + this.gameObject.name);
                Die();
            }
        }
    }

    void Die()
    {
        playerManager.Die();
    }
}
