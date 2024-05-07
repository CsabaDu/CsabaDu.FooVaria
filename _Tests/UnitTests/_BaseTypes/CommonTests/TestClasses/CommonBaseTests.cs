using CsabaDu.FooVaria.Tests.TestHelpers.Params;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private CommonBaseChild _commonBase;
    private DataFields _fields;

    #region Readonly fields
    //private readonly DataFields Fields = new();
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _fields = new();
    }
    #endregion

    #region Test methods
    #region CommonBase
    #region CommonBase(IRootObject)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IRootobject_throws_ArgumentNullException()
    {
        // Arrange
        _fields.paramName = _fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _ = new CommonBaseChild(null, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IRootobject_creates()
    {
        // Arrange
        _fields.paramName = null;

        // Act
        var actual = new CommonBaseChild(_fields.RootObject, _fields.paramName);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
    }
    #endregion
    #endregion

    #region IFactory GetFactory
    #region abstract ICommonBase.GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returns_expected()
    {
        // Arrange
        _fields.paramName = null;
        _commonBase = new(_fields.RootObject, _fields.paramName)
        {
            Return = new()
            {
                GetFactoryValue = new FactoryObject(),
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
