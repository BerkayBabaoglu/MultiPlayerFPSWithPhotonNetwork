using NUnit.Framework;
using UnityEngine;

public class PanelControlTests
{
    private GameObject panelControlObject;
    private PanelControl panelControl;

    [SetUp]
    public void Setup()
    {
        panelControlObject = new GameObject();
        panelControl = panelControlObject.AddComponent<PanelControl>();
        panelControl.panel = new GameObject("Panel");
        panelControl.panel.SetActive(false);
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(panelControlObject);
    }

    [Test]
    public void PanelControl_Panel_BaslangictaKapali()
    {
        Assert.IsFalse(panelControl.panel.activeSelf);
    }

    [Test]
    public void PanelControl_Panel_AktifEdilebiliyor()
    {
        panelControl.panel.SetActive(true);
        Assert.IsTrue(panelControl.panel.activeSelf);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(panelControl);
    }
} 