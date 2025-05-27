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
        if (Instance) //eger Instance zaten varsa, yeni bir nesne olusturulmaz. Singleton
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);
        Instance = this;

        PhotonNetwork.AutomaticallySyncScene = false; 
    }

    public override void OnEnable() //sahne degisikliklerinde kullanilir.
    {
        base.OnEnable();
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    public override void OnDisable() //sahne degisikliklerinde kullanilir.
    {
        base.OnDisable();
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode) //sahne yuklendiginde cagrilir.
    {

        if (scene.buildIndex == 3)  
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player")) //eger sahnede onceden Player varsa silinir. Bu sayede ağa dogru bir sekilde baglanilir.
            {
                Destroy(obj);
            }
            PhotonNetwork.Instantiate(Path.Combine("PhotonPrefabs", "PlayerManager"), Vector3.zero, Quaternion.identity); //yeni bir PlayerManager prefab'i olusturur ve bunu Photon aginda baslatir.
        }
    }

    public override void OnLeftRoom() //oyuncu odadan ciktiktan sonra o odanin bilgileri oyuncudan silinir.
    {
        Destroy(gameObject); 
    }

    public override void OnPlayerLeftRoom(Photon.Realtime.Player otherPlayer) //
    {
        Debug.Log("Bir oyuncu ayrildi: " + otherPlayer.NickName);

        foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
        {
            if (obj.GetComponent<PhotonView>().Owner == otherPlayer)
            {
                Destroy(obj);
            }
        }
    }
}