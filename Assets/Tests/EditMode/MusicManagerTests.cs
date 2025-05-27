using NUnit.Framework;
using UnityEngine;

public class MusicManagerTests
{
    private GameObject musicManagerObject;
    private MusicManager musicManager;

    [SetUp]
    public void Setup()
    {
        musicManagerObject = new GameObject();
        musicManager = musicManagerObject.AddComponent<MusicManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(musicManagerObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(musicManager);
    }
} 