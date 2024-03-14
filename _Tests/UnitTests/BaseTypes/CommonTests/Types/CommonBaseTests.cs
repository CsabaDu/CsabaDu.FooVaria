namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private CommonBaseChild _commonBase;
    private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly IRootObject RootObject;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region CommonBase(IRootObject)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IRootObject_throws_ArgumentNullException()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _ = new CommonBaseChild(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IRootObject_creates()
    {
        // Arrange
        _paramName = null;

        // Act
        var actual = new CommonBaseChild(RootObject, _paramName);

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
        _paramName = null;
        _commonBase = new(RootObject, _paramName)
        {
            Returns = new()
            {
                GetFactory = new FactoryObject(),
            }
        };

        // Act
        var actual = _commonBase.GetFactory();

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IFactory));
    }
    #endregion
    #endregion
    #endregion
}
