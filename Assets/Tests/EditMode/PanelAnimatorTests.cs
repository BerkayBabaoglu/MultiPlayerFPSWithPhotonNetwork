using NUnit.Framework;
using UnityEngine;

public class PanelAnimatorTests
{
    private GameObject panelAnimatorObject;
    private PanelAnimator panelAnimator;

    [SetUp]
    public void Setup()
    {
        panelAnimatorObject = new GameObject();
        panelAnimator = panelAnimatorObject.AddComponent<PanelAnimator>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(panelAnimatorObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(panelAnimator);
    }
} 