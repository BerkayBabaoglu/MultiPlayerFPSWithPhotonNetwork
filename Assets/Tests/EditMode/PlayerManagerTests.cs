using NUnit.Framework;
using UnityEngine;
using Photon.Pun;

public class PlayerManagerTests
{
    private GameObject playerManagerObject;
    private PlayerManager playerManager;
    private PhotonView photonView;

    [SetUp]
    public void Setup()
    {
        playerManagerObject = new GameObject();
        playerManager = playerManagerObject.AddComponent<PlayerManager>();
        photonView = playerManagerObject.AddComponent<PhotonView>();
        typeof(PlayerManager)
            .GetField("PV", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(playerManager, photonView);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(playerManagerObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(playerManager);
    }

    [Test]
    public void PlayerManager_GetTeam_BaslangictaBos()
    {
        Assert.AreEqual("", playerManager.GetTeam());
    }

    [Test]
    public void RedTeam()
    {
        playerManager.SelectRedTeam();
        Assert.AreEqual("Red", playerManager.GetTeam());
    }

    [Test]
    public void BlueTeam()
    {
        playerManager.SelectBlueTeam();
        Assert.AreEqual("Blue", playerManager.GetTeam());
    }
}