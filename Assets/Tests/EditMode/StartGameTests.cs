using NUnit.Framework;
using UnityEngine;

public class StartGameTests
{
    private GameObject startGameObject;
    private StartGame startGame;

    [SetUp]
    public void Setup()
    {
        startGameObject = new GameObject();
        startGame = startGameObject.AddComponent<StartGame>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(startGameObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(startGame);
    }

    [Test]
    public void QuitGame()
    {
        Assert.DoesNotThrow(() => startGame.QuitGame());
    }

    [Test]
    public void LoadScene_ThrowsInEditMode()
    {
        Assert.Throws<System.InvalidOperationException>(() => startGame.LoadScene());
    }

    [Test]
    public void Back_ThrowsInEditMode()
    {
        Assert.Throws<System.InvalidOperationException>(() => startGame.Back());
    }
}