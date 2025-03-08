using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatCanvasActive : MonoBehaviour
{
    [SerializeField] GameObject chatCanvas;
    public void setCanvasActive()
    {
        chatCanvas.SetActive(true);
    }


}
