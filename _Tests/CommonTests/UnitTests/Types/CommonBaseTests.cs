namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private IFactory factory;
    private ICommonBase commonBase;
    private ICommonBase[] commonBases;
    #endregion

    #region Test methods
    #region Constructors
    #region CommonBase(IFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IFactory_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;

        // Act
        void attempt() => _ = new CommonBaseChild(factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_creates()
    {
        // Arrange
        factory = new FactoryClass();

        // Act
        var actual = new CommonBaseChild(factory);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion

    #region CommonBase(IFactory, params ICommonBase[])
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IFactory_arg_ICommonBase_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        commonBases = null;

        // Act
        void attempt() => _ = new CommonBaseChild(factory, commonBases);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_nullArg_ICommonBase_throws_ArgumentNullException()
    {
        // Arrange
        factory = new FactoryClass();
        commonBases = null;

        // Act
        void attempt() => _ = new CommonBaseChild(factory, commonBases);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.commonBases, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_nullElementArrayArg_ICommonBase_throws_ArgumentNullException()
    {
        // Arrange
        factory = new FactoryClass();
        commonBases = [ null ];

        // Act
        void attempt() => _ = new CommonBaseChild(factory, commonBases);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.commonBases, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArgs_IFactory_ICommonBase_creates()
    {
        // Arrange
        factory = new FactoryClass();
        commonBase = new CommonBaseChild(factory);
        commonBases = [ commonBase ];

        // Act
        var actual = new CommonBaseChild(factory, commonBases);

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
        commonBase = null;

        // Act
        void attempt() => _ = new CommonBaseChild(commonBase);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_ICommonBase_creates()
    {
        // Arrange
        factory = new FactoryClass();
        commonBase = new CommonBaseChild(factory);

        // Act
        var actual = new CommonBaseChild(commonBase);

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
        factory = new FactoryClass();
        commonBase = new CommonBaseChild(factory);

        // Act
        var actual = commonBase.GetFactory();

        // Assert
        Assert.AreEqual(factory, actual);
    }
    #endregion
    #endregion
    #endregion
}
