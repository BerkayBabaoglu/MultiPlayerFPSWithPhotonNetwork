using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Firebase.Auth;
using TMPro;
using Firebase;
using System.Net.Mail;
using System.Threading.Tasks;
using UnityEngine.SceneManagement;

public class EmailAuth : MonoBehaviour
{
    FirebaseAuth auth;

     public TMP_InputField email;
    public TMP_InputField changeemail;
    public TMP_InputField password;
    public TMP_InputField newpassword;
    public TMP_InputField currentPassword;

    public static EmailAuth Instance;
    bool isFaultLogin;

    [SerializeField] GameObject passwordPopUpPanel;
    PanelAnimator panelAnimator;
    private void Awake()
    {
        Instance = this;
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        panelAnimator = passwordPopUpPanel.GetComponent<PanelAnimator>();
        isFaultLogin = false;
    }

    public void SignUp()
    {
        auth.CreateUserWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("CreateUserWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            // Firebase user has been created.
            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("Firebase user created successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);
        });
    }

    public void Login()
    {
        auth.SignInWithEmailAndPasswordAsync(email.text, password.text).ContinueWith(task =>
        {
            if (task.IsCanceled)
            {
                Debug.LogError("SignInWithEmailAndPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                //Debug.LogError("SignInWithEmailAndPasswordAsync encountered an error: " + task.Exception);
                Debug.Log("hata");
                Debug.Log(email.text);
                isFaultLogin = true;
                return;
            }

            Firebase.Auth.AuthResult result = task.Result;
            Debug.LogFormat("User signed in successfully: {0} ({1})",
                result.User.DisplayName, result.User.UserId);

        });
        if(isFaultLogin)
        {
            SceneManager.LoadScene("Lobby");
        }
    }

    public void ResetPassword()
    {
        ResetPasswordAsync(email.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("ResetPasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("ResetPasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password reset email sent successfully.");
        });
    }

    private async Task ResetPasswordAsync(string emailAddress)
    {
        await auth.SendPasswordResetEmailAsync(emailAddress);
    }


    public void ChangeUserPassword()
    {
        ChangePasswordAsync(changeemail.text,newpassword.text, currentPassword.text).ContinueWith(task => {
            if (task.IsCanceled)
            {
                Debug.LogError("ChangePasswordAsync was canceled.");
                return;
            }
            if (task.IsFaulted)
            {
                Debug.LogError("ChangePasswordAsync encountered an error: " + task.Exception);
                return;
            }

            Debug.Log("Password updated successfully.");
        });
    }

    private async Task ChangePasswordAsync(string emailAddress, string newPassword, string currentPassword)
    {
        // Mevcut kullanýcýyý al
        //FirebaseUser user = await auth.SignInWithEmailAndPasswordAsync(emailAddress, currentPassword);
        AuthResult authResult = await auth.SignInWithEmailAndPasswordAsync(emailAddress, currentPassword);

        // AuthResult içindeki FirebaseUser nesnesine eriþ
        FirebaseUser user = authResult.User;

        if (user != null)
        {
            // Þifreyi güncelle
            await user.UpdatePasswordAsync(newPassword);
        }
        else
        {
            Debug.LogError("No user is currently signed in.");
        }
    }

}
