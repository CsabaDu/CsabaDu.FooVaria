using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Types;
using CsabaDu.FooVaria.Tests.TestSupport.Params;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class CommonBaseTests
{
    #region Test methods
    #region Constructors
    #region CommonBase(IFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_NullArg_IFactory_ThrowsArgumentNullException()
    {
        // Arrange
        IFactory factory = null;

        // Act
        void attempt() => _ = new CommonBaseChild(factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_ValidArg_IFactory_CreatesInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();

        // Act
        var actual = new CommonBaseChild(factory);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion

    #region CommonBase(ICommonBase)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_NullArg_ICommonBase_ThrowsArgumentNullException()
    {
        // Arrange
        ICommonBase other = null;

        // Act
        void attempt() => _ = new CommonBaseChild(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_ValidArg_ICommonBase_CreatesInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        ICommonBase other = new CommonBaseChild(factory);

        // Act
        var actual = new CommonBaseChild(other);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion
    #endregion
    #endregion
}
