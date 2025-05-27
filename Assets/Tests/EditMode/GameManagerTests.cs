using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

public class GameManagerTests
{
    private GameObject gameManagerObject;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        GameManager.Instance = null;
        gameManagerObject = new GameObject();
        gameManager = gameManagerObject.AddComponent<GameManager>();
        gameManager.teamSelectionPanel = new GameObject();
        gameManager.winPanel = new GameObject();
        gameManager.losePanel = new GameObject();
        gameManager.redTeamSpawns = new Transform[1] { new GameObject().transform };
        gameManager.blueTeamSpawns = new Transform[1] { new GameObject().transform };
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(gameManagerObject);
        GameManager.Instance = null;
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(gameManager);
    }

    [Test]
    public void SingletonUygulandiMi()
    {
        gameManager.Awake();
        Assert.AreSame(gameManager, GameManager.Instance);
    }

    [Test]
    public void GameManager_Skorlar_DefaultZero()
    {
        Assert.AreEqual(0, gameManager.redScore);
        Assert.AreEqual(0, gameManager.blueScore);
    }

    [Test]
    public void GameManager_WinLosePanels()
    {
        LogAssert.Expect(LogType.Error, "InGameUIManager bulunamadı! Skorlar sıfır olabilir.");
        gameManager.winPanel.SetActive(true);
        gameManager.losePanel.SetActive(true);
        gameManager.Start();
        Assert.IsFalse(gameManager.winPanel.activeSelf);
        Assert.IsFalse(gameManager.losePanel.activeSelf);
    }
} 