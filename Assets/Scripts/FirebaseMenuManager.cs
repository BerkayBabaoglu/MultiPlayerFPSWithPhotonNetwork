using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirebaseMenuManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> menus;

    int currentIndex;

    void Start()
    {
        menus[0].SetActive(true);
        currentIndex = 0;
    }


    void Update()
    {
        
    }

    public void OpenPreviousMenu()
    {
        CloseMenus();

        if (currentIndex == 0)
        {
            menus[menus.Count - 1].SetActive(true);

            currentIndex = menus.Count - 1;
        }
        else if(currentIndex == menus.Count - 1)
        {
            menus[0].SetActive(true);

            currentIndex = 0;
        }
        else
        {
            menus[currentIndex - 1].SetActive(true);
        }
    }

    public void CloseMenus()
    {
        foreach (var menu in menus)
        {
            menu.SetActive(false);
        }
    }

    public void OpenPasswordResetMenu()
    {
        CloseMenus();

        foreach (var menu in menus)
        {
            if (menu.CompareTag("Password Reset"))
            {
                menu.gameObject.SetActive(true);

                currentIndex = menus.IndexOf(menu);
            }

        }
    }

    public void ResetPassword()
    {

    }

    public void Login()
    {

    }

    public void RegisterAndLogin()
    {
        //register iþlemleri

        Login();
    }


}
