using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Chat : MonoBehaviour
{
    public TMP_InputField InputField;
    public GameObject Message;
    public GameObject content;
    private List<string> messageHistory = new List<string>();

    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, InputField.text);
    }

    [PunRPC]
    public void GetMessage(string receiveMessage)
    {
        messageHistory.Add(receiveMessage);
        UpdateChatCanvas();
    }

    public void UpdateChatCanvas()
    {
        // Önceki mesajlarý temizle
        foreach (Transform child in content.transform)
        {
            Destroy(child.gameObject);
        }

        // Tüm mesajlarý yeniden oluþtur
        foreach (string message in messageHistory)
        {
            GameObject M = Instantiate(Message, Vector3.zero, Quaternion.identity, content.transform);
            M.GetComponent<ChatMessage>().text.text = message;
        }
    }

    public void ToggleChatCanvas()
    {
        content.SetActive(!content.activeSelf);
        if (content.activeSelf)
        {
            UpdateChatCanvas();
        }
    }
}