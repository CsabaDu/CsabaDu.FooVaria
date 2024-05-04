namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private CommonBaseChild _commonBase;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion
    #endregion

    #region Test methods
    #region CommonBase
    #region CommonBase(IRootObject)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IRootobject_throws_ArgumentNullException()
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _ = new CommonBaseChild(null, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_validArg_IRootobject_creates()
    {
        // Arrange
        Fields.paramName = null;

        // Act
        var actual = new CommonBaseChild(Fields.RootObject, Fields.paramName);

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
        Fields.paramName = null;
        _commonBase = new(Fields.RootObject, Fields.paramName)
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
