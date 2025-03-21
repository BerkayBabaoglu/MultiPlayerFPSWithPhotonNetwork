using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class FirebaseMenuManager : MonoBehaviour
{
    [SerializeField]
    List<GameObject> menus;

    [SerializeField] GameObject mailpasswordpanel;
    int currentIndex;

    PanelAnimator panelAnimator;
    private void Awake()
    {
        panelAnimator = mailpasswordpanel.GetComponent<PanelAnimator>();
    }
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
        EmailAuth.Instance.Login();
    }

    public void RegisterAndLogin()
    {
        
        EmailAuth.Instance.SignUp();

        EmailAuth.Instance.email.text = string.Empty;
        EmailAuth.Instance.password.text = string.Empty;

        panelAnimator.AnimatePanel();
 
    }


}
