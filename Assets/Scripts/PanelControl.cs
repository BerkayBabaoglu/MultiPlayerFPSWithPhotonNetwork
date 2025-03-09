using Photon.Pun;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelControl : MonoBehaviourPunCallbacks
{
    public GameObject panel;

    private void Start()
    {
        if (panel == null)
        {
            panel = GameObject.Find("Panel"); // Panel GameObject'ini bul
            if (panel == null)
            {
                Debug.LogError("Panel GameObject'ini bulamadým!");
                return;
            }
        }

        panel.SetActive(false); // Baþlangýçta panel gizli olsun
        SetCursorState(false);
    }

    private void Update()
    {
        if (panel == null)
        {
            Debug.LogError("Panel GameObject'i referans olarak verilmemiþ!");
            return;
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            bool isActive = !panel.activeSelf;
            
            panel.SetActive(isActive); // panel acik mi degil mi tersine cevir
            SetCursorState(isActive);
            Debug.Log("Panel aktif mi? " + panel.activeSelf);
        }
    }

    private void SetCursorState(bool isPanelOpen)
    {
        if (isPanelOpen)
        {
            Cursor.lockState = CursorLockMode.None; //cursor serbest
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked; //cursor kilit
            Cursor.visible = false;
        }
    }

    public void LeaveRoom()
    {
        if (PhotonNetwork.InRoom)
        {
            foreach (var obj in GameObject.FindGameObjectsWithTag("Player"))
            {
                Destroy(obj);
            }
            PhotonNetwork.LeaveRoom();
        }
    }

    public override void OnLeftRoom()
    {
        SceneManager.LoadScene(1);
    }
}
