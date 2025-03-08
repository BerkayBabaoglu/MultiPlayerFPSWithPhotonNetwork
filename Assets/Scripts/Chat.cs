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
    public void SendMessage()
    {
        GetComponent<PhotonView>().RPC("GetMessage", RpcTarget.All, InputField.text);
    }

    [PunRPC]    
    public void GetMessage(string receiveMessage)
    {
        GameObject M = Instantiate(Message,Vector3.zero, Quaternion.identity,content.transform);
        M.GetComponent<ChatMessage>().text.text = receiveMessage;
    }

}
