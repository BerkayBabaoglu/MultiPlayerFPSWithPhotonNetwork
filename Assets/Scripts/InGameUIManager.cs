using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class InGameUIManager : MonoBehaviour
{
    public static InGameUIManager Instance;

    //public TextMeshProUGUI redScoreText;
    //public TextMeshProUGUI blueScoreText;
    //public TextMeshProUGUI timerText;
    public TextMeshProUGUI healthText;

    public TextMeshProUGUI redScoreText;
    public TextMeshProUGUI blueScoreText;
    public TextMeshProUGUI timerText;

    private int redScore = 0;
    private int blueScore = 0;
    private float gameTime = 600f; // 10 dakika (600 saniye)
    private float playerHealth = 100f; 

    private PhotonView PV;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        redScoreText = GameObject.Find("RedScoreText").GetComponent<TextMeshProUGUI>();
        blueScoreText = GameObject.Find("BlueScoreText").GetComponent<TextMeshProUGUI>();
        timerText = GameObject.Find("TimerText").GetComponent<TextMeshProUGUI>();

        StartCoroutine(StartCountdown(timerText));
    }

    public void UpdateScore(string team)
    {
        Debug.LogWarning($"UpdateScore çaðrýldý! Takým: {team}");

        if (team == "Red")
        {
            blueScore++; // Red oyuncu ölünce Blue puan alýr
            Debug.LogWarning($"Blue Takým Puaný: {blueScore}");
        }
        else if (team == "Blue")
        {
            redScore++;
            Debug.LogWarning($"Red Takým Puaný: {redScore}");
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        Debug.LogWarning($"UI Güncelleniyor... Red: {redScore}, Blue: {blueScore}");

        redScoreText.text = redScore.ToString();
        blueScoreText.text = blueScore.ToString();
    }

    public IEnumerator StartCountdown(TextMeshProUGUI timerText)
    {
        while (gameTime > 0)
        {
            int minutes = Mathf.FloorToInt(gameTime / 60);
            int seconds = Mathf.FloorToInt(gameTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            yield return new WaitForSeconds(1f);
            gameTime--;
        }

        GameManager.Instance.EndGame();
    }


    public int GetBlueScore() { return blueScore; }
    public int GetRedScore() { return redScore; }
}
