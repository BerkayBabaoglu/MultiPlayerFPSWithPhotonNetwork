using NUnit.Framework;
using UnityEngine;

public class MenuTests
{
    private GameObject menuObject;
    private Menu menu;

    [SetUp]
    public void Setup()
    {
        menuObject = new GameObject();
        menu = menuObject.AddComponent<Menu>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(menuObject);
    }

    [Test]
    public void SetActiveTrue() //menu aciliyor mu
    {
        menu.open = false;
        menuObject.SetActive(false);

        menu.Open();

        Assert.IsTrue(menu.open);
        Assert.IsTrue(menuObject.activeSelf);
    }

    [Test]
    public void SetActiveFalse() //menu kapaniyor mu
    {
        menu.open = true;
        menuObject.SetActive(true);

        menu.Close();

        Assert.IsFalse(menu.open);
        Assert.IsFalse(menuObject.activeSelf);
    }
}