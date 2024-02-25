namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private FactoryClass _factory;
    private CommonBaseChild _commonBase;
    #endregion

    #region Test methods
    #region Constructors
    #region CommonBase(IFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IFactory_throws_ArgumentNullException()
    {
        // Arrange
        _factory = null;

        // Act
        void attempt() => _ = new CommonBaseChild(_factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_creates()
    {
        // Arrange
        _factory = new FactoryClass();

        // Act
        var actual = new CommonBaseChild(_factory);

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
        _commonBase = null;

        // Act
        void attempt() => _ = new CommonBaseChild(_commonBase);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_ICommonBase_creates()
    {
        // Arrange
        _factory = new FactoryClass();
        _commonBase = new CommonBaseChild(_factory);

        // Act
        var actual = new CommonBaseChild(_commonBase);

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
        _factory = new FactoryClass();
        _commonBase = new CommonBaseChild(_factory);

        // Act
        var actual = _commonBase.GetFactory();

        // Assert
        Assert.AreEqual(_factory, actual);
    }
    #endregion
    #endregion
    #endregion
}
