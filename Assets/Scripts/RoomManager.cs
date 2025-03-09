using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using System.IO;

public class RoomManager : MonoBehaviourPunCallbacks
{
    public static RoomManager Instance;

    private void Awake()
    {
        if (Instance)
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        PhotonNetwork.AutomaticallySyncScene = false; // Manuel sahne kontrol�
    }

    public override void OnEnable()
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable()
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        if (scene.buildIndex == 2)  // Oyun sahnesi
        {
            // Eski PlayerManager'lar� temizle
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(obj);
            }

            // Yeni oyuncu olu�tur
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject); // Oday� terk edince RoomManager'� sil
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Bir oyuncu ayr�ld�: " + otherPlayer.NickName);

        // Odada eski oyuncu objeleri kald�ysa temizle
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj.GetComponent<PhotonView>().Owner == otherPlayer)
            {
                Destroy(obj);
            }
        }
    }
}