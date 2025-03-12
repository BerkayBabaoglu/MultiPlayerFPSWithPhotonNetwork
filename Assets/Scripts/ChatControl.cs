using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatControl : MonoBehaviour
{
    public bool lockCursor = true;
    public Chat chat;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;

        if (Input.GetKeyDown(KeyCode.T)) // �rne�in, 'T' tu�u ile chat canvas'�n� a��p kapat
        {
            chat.ToggleChatCanvas();
        }
    }


}
