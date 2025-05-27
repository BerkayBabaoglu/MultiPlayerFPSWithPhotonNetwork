using NUnit.Framework;
using UnityEngine;

public class MenuManagerTests
{
    private GameObject menuManagerObject;
    private MenuManager menuManager;
    private Menu menu1;
    private Menu menu2;

    [SetUp]
    public void Setup()
    {
        menuManagerObject = new GameObject();
        menuManager = menuManagerObject.AddComponent<MenuManager>();

        // Menüleri oluştur ve diziye ekle
        menu1 = new GameObject("Menu1").AddComponent<Menu>();
        menu1.menuName = "menu1";
        menu2 = new GameObject("Menu2").AddComponent<Menu>();
        menu2.menuName = "menu2";

        // MenuManager'ın menus dizisini ayarla
        typeof(MenuManager)
            .GetField("menus", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
            .SetValue(menuManager, new Menu[] { menu1, menu2 });
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(menuManagerObject);
        Object.DestroyImmediate(menu1.gameObject);
        Object.DestroyImmediate(menu2.gameObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(menuManager);
    }

    [Test]
    public void OpenMenu_AcilanMenuAktifMi()
    {
        menuManager.OpenMenu("menu1");
        Assert.IsTrue(menu1.open);
        Assert.IsTrue(menu1.gameObject.activeSelf);
        Assert.IsFalse(menu2.open);
        Assert.IsFalse(menu2.gameObject.activeSelf);

        menuManager.OpenMenu("menu2");
        Assert.IsTrue(menu2.open);
        Assert.IsTrue(menu2.gameObject.activeSelf);
        Assert.IsFalse(menu1.open);
        Assert.IsFalse(menu1.gameObject.activeSelf);
    }

    [Test]
    public void CloseMenu_KapananMenuPasifMi()
    {
        menuManager.OpenMenu("menu1");
        menuManager.CloseMenu(menu1);

        Assert.IsFalse(menu1.open);
        Assert.IsFalse(menu1.gameObject.activeSelf);
    }
} 