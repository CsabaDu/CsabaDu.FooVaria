namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private ICommonBase _commonBase;
    private string _paramName;
    private IRootObject _rootObject;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region CommonBase(IRootObject)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IRootObject_throws_ArgumentNullException()
    {
        // Arrange
        _rootObject = null;
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _ = new CommonBaseChild(_rootObject, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IRootObject_creates()
    {
        // Arrange
        _rootObject = SampleParams.rootObject;
        _paramName = string.Empty;

        // Act
        var actual = new CommonBaseChild(_rootObject, _paramName);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
    }
    #endregion
    #endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returns_expected()
    {
        // Arrange
        _commonBase = new CommonBaseChild(SampleParams.rootObject, string.Empty);

        // Act
        var actual = _commonBase.GetFactory();

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IFactory));
    }
    #endregion
    #endregion
    #endregion
}
