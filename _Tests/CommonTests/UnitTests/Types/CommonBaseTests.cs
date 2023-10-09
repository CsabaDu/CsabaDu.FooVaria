namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class CommonBaseTests
{
    #region Private fields
    private IFactory factory;
    private ICommonBase commonBase;
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
        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IFactory_createsInstance()
    {
        // Arrange
        factory = new FactoryImplementation();

        // Act
        var actual = new CommonBaseChild(factory);

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
        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_ICommonBase_createsInstance()
    {
        // Arrange
        factory = new FactoryImplementation();
        commonBase = new CommonBaseChild(factory);

        // Act
        var actual = new CommonBaseChild(commonBase);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion
    #endregion
    #endregion
}
