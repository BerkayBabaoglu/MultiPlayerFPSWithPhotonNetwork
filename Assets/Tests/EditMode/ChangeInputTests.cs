using NUnit.Framework;
using UnityEngine;

public class ChangeInputTests
{
    private GameObject changeInputObject;
    private ChangeInput changeInput;

    [SetUp]
    public void Setup()
    {
        changeInputObject = new GameObject();
        changeInput = changeInputObject.AddComponent<ChangeInput>();
    }

    [TearDown]
    public void TearDown()
    {
        Object.DestroyImmediate(changeInputObject);
    }

    [Test]
    public void NullKontrol()
    {
        Assert.IsNotNull(changeInput);
    }
} 