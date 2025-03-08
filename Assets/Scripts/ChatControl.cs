using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChatControl : MonoBehaviour
{
    public bool lockCursor = true;


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            lockCursor = !lockCursor;
        }
        Cursor.lockState = lockCursor ? CursorLockMode.Locked : CursorLockMode.None;
        Cursor.visible = !lockCursor;
    }


}
