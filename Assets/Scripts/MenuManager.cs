using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    public static MenuManager Instance;

    [SerializeField] Menu[] menus;
    public GameObject panel;
    private bool isPaused = false;

    private void Awake()
    {
        Instance = this;
    }


    public void OpenMenu(string menuName)
    {
        for (int i = 0; i < menus.Length; i++)
        {
            Debug.Log("for'a girdim");
            if (menus[i].menuName == menuName)
            {
                Debug.Log("girdim" + menus[i].name);
                menus[i].Open();
                Debug.Log("girdi" + menus[i].name);
            }
            else if (menus[i].open)
            {
                Debug.Log("gird" + menus[i].name);
                CloseMenu(menus[i]);
                Debug.Log("girdimmm" + menus[i].name);
            }
        }
    }

    public void OpenMenu(Menu menu) {

        for (int i = 0; i < menus.Length; i++)
        {
            Debug.Log("girdi5" + menus[i].name);
            if (menus[i].open)
            {
                Debug.Log("girdim2" + menus[i].name);
                CloseMenu(menus[i]);
            }
        }
        Debug.Log("girdim3" + menu.name);
        menu.Open();
    }

    public void CloseMenu(Menu menu)
    {
        menu.Close();
    }

}