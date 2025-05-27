using NUnit.Framework;
using UnityEngine;

public class AudioManagerTests
{
    private GameObject audioManagerObject;
    private AudioManager audioManager;

    [SetUp]
    public void Setup()
    {
        audioManagerObject = new GameObject();
        audioManager = audioManagerObject.AddComponent<AudioManager>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(audioManagerObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(audioManager);
    }
} 