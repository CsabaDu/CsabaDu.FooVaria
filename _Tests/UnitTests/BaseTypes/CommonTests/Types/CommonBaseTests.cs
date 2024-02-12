namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private FactoryClass factoryObject;
    private CommonBaseChild commonBaseObject;
    #endregion

    #region Test methods
    #region Constructors
    #region CommonBase(IFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IFactory_throws_ArgumentNullException()
    {
        // Arrange
        factoryObject = null;

        // Act
        void attempt() => _ = new CommonBaseChild(factoryObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_creates()
    {
        // Arrange
        factoryObject = new FactoryClass();

        // Act
        var actual = new CommonBaseChild(factoryObject);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion

    #region CommonBase(ICommonBase)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_ICommonBase_throws_ArgumentNullException()
    {
        // Arrange
        commonBaseObject = null;

        // Act
        void attempt() => _ = new CommonBaseChild(commonBaseObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_ICommonBase_creates()
    {
        // Arrange
        factoryObject = new FactoryClass();
        commonBaseObject = new CommonBaseChild(factoryObject);

        // Act
        var actual = new CommonBaseChild(commonBaseObject);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion
    #endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returns_expected()
    {
        // Arrange
        factoryObject = new FactoryClass();
        commonBaseObject = new CommonBaseChild(factoryObject);

        // Act
        var actual = commonBaseObject.GetFactory();

        // Assert
        Assert.AreEqual(factoryObject, actual);
    }
    #endregion
    #endregion
    #endregion
}
