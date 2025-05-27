using NUnit.Framework;
using UnityEngine;

public class MenuSelectTests
{
    private GameObject obj;
    private MenuSelect menuSelect;

    [SetUp]
    public void Setup()
    {
        obj = new GameObject();
        menuSelect = obj.AddComponent<MenuSelect>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(obj);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(menuSelect);
    }
}