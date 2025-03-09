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

        PhotonNetwork.AutomaticallySyncScene = false; // Manuel sahne kontrolü
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
            // Eski PlayerManager'larý temizle
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(obj);
            }

            // Yeni oyuncu oluþtur
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity);
        }
    }

    public override void OnLeftRoom()
    {
        Destroy(gameObject); // Odayý terk edince RoomManager'ý sil
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer)
    {
        Debug.Log("Bir oyuncu ayrýldý: " + otherPlayer.NickName);

        // Odada eski oyuncu objeleri kaldýysa temizle
        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj.GetComponent<PhotonView>().Owner == otherPlayer)
            {
                Destroy(obj);
            }
        }
    }
}