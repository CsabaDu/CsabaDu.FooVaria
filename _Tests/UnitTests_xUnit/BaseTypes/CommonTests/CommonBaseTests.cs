namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class CommonBaseTests
{
    #region Private fields
    private readonly DataFields _fields = new();
    #endregion

    #region Test methods
    #region CommonBase
    #region CommonBase(IRootObject)
    [Fact]
    public void CommonBase_nullArg_IRootobject_throws_ArgumentNullException()
    {
        // Arrange
        _fields.paramName = ParamNames.rootObject;

        // Act
        void attempt() => _ = new CommonBaseChild(null, _fields.paramName);

        // Assert
        Assert.Throws<ArgumentNullException>(_fields.paramName, attempt);
    }

    [Fact]
    public void CommonBase_validArg_IRootobject_doesNotThrow()
    {
        // Arrange
        // Act
        var actual = new CommonBaseChild(_fields.RootObject, null);

        // Assert
        Assert.IsAssignableFrom<ICommonBase>(actual);
    }
    #endregion
    #endregion

    #region IFactory GetFactory
    #region abstract ICommonBase.GetFactory()
    [Fact]
    public void GetFactory_returns_expected()
    {
        // Arrange
        _fields.paramName = null;
        CommonBaseChild commonBaseChild = new(_fields.RootObject, _fields.paramName)
        {
            ReturnValues = new()
            {
                GetFactoryReturnValue = new FactoryObject(),
            }
        };

        // Act
        var actual = commonBaseChild.GetFactory();

        // Assert
        Assert.IsAssignableFrom<IFactory>(actual);
    }
    #endregion
    #endregion
    #endregion
}
