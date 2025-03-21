using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ChangeInput : MonoBehaviour
{
    EventSystem system;
    public Selectable firstInput;
    public Button submitButton;

    // Start is called before the first frame update
    void Start()
    {
        system = EventSystem.current;
        firstInput.Select();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab) && Input.GetKey(KeyCode.LeftShift)) //Shitf + Tab'a bas�ld���nda bir alttaki ui �gesine ge�ecek
        {
            Selectable previous = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnUp();
            if (previous != null)
            {
                previous.Select();
            }
        }
        else if (Input.GetKeyDown(KeyCode.Tab)) //Tab'a bas�ld���nda bir �stteki ui �gesine ge�ecek
        {
            Selectable next = system.currentSelectedGameObject.GetComponent<Selectable>().FindSelectableOnDown();
            if (next != null)
            {
                next.Select();
            }
        }
        else if(Input.GetKeyDown(KeyCode.Return)) //enter'a bas�l�nca login ediyor
        {
            submitButton.onClick.Invoke();
            Debug.Log("Button pressed");

            //Firebase Login veya register i�lemleri
        }
    }
}
