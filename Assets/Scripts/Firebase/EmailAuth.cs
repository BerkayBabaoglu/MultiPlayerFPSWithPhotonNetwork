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

    private string idToken;
    private float tokenExpirationTime;
    private FirebaseUser user;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
           
            DontDestroyOnLoad(gameObject); // Sahne geçiþlerinde yok olma
        }
        else
        {
            Destroy(gameObject); // Birden fazla örnek oluþmasýný engelle
        }
        tokenExpirationTime = Time.time + 60;
    }

    void Start()
    {
        auth = FirebaseAuth.DefaultInstance;
        panelAnimator = passwordPopUpPanel.GetComponent<PanelAnimator>();
        isFaultLogin = false;
    }

    void Update()
    {
        // Token süresini kontrol et
        Debug.Log(Time.time); // oyun ilk baþlatýldýðý andan itibaren geçen süre
        Debug.Log("token ex : " + tokenExpirationTime); // giriþ yapýldýktan sonra ile baþlangýçtan itibaren geçen sürenin toplamý
        if (user != null && Time.time >= tokenExpirationTime)
        {
            Debug.Log("Token expired, logging out...");
            Debug.Log("user : " + user);
            Debug.Log(tokenExpirationTime);
            Logout();
        }
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

    public async void Login()
    {
        try
        {
            // Firebase giriþ iþlemini baþlat ve bekleyin
            var task = await auth.SignInWithEmailAndPasswordAsync(email.text, password.text);

            if (task != null)
            {
                Firebase.Auth.AuthResult result = task;
                Debug.LogFormat("User signed in successfully: {0} ({1})",
                    result.User.DisplayName, result.User.UserId);

                tokenExpirationTime = Time.time + 60;

                // Kullanýcý oturumunu kaydet
                user = result.User;

                // Token al ve süresini ayarla
                await GetIdToken();

                if (!isFaultLogin)
                {
                    SceneManager.LoadScene("Lobby");
                }
            }
        }
        catch (System.Exception ex)
        {
            panelAnimator.AnimatePanel();
            Debug.Log("Login failed: " + ex.Message);
            isFaultLogin = true;
        }
    }

    private async Task GetIdToken()
    {
        if (user != null)
        {
            try
            {
                // Token al ve süresini ayarla
                var tokenTask = await user.TokenAsync(true);
                idToken = tokenTask;
                tokenExpirationTime = Time.time + 60; // 1 dakika sonra token süresi dolacak
                Debug.Log("Token received: " + idToken);
            }
            catch (System.Exception ex)
            {
                Debug.LogError("Token retrieval failed: " + ex.Message);
            }
        }
        else
        {
            Debug.LogError("No user is currently signed in.");
        }
    }

    public void Logout()
    {
        auth.SignOut();
        user = null;
        idToken = null;
        Debug.Log("User logged out.");
        SceneManager.LoadScene("PlayerLogin");
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
        ChangePasswordAsync(changeemail.text, newpassword.text, currentPassword.text).ContinueWith(task => {
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
        AuthResult authResult = await auth.SignInWithEmailAndPasswordAsync(emailAddress, currentPassword);
        FirebaseUser user = authResult.User;

        if (user != null)
        {
            await user.UpdatePasswordAsync(newPassword);
        }
        else
        {
            Debug.LogError("No user is currently signed in.");
        }
    }
}