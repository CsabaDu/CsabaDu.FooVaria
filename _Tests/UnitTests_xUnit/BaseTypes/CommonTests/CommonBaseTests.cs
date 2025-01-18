namespace CsabaDu.FooVaria.Test.UnitTests_xUnit.BaseTypes.CommonTests;

public sealed class CommonBaseTests
{
    #region Private fields
    private readonly IRootObject _rootObject = new RootObject();
    #endregion

    #region Test methods
    #region CommonBase tests
    #region CommonBase(IRootObject, string)
    [Fact]
    public void CommonBase_nullArg_IRootobject_throwsArgumentNullException()
    {
        // Arrange
        string paramName = ParamNames.rootObject;

        // Act
        void attempt() => _ = new TestCommonBase(null, paramName, null);

        // Assert
        Assert.Throws<ArgumentNullException>(paramName, attempt);
    }

    [Fact]
    public void CommonBase_validArg_IRootobject_doesNotThrow()
    {
        // Arrange
        // Act
        var actual = new TestCommonBase(_rootObject, null, null);

        // Assert
        Assert.NotNull(actual);
        Assert.IsAssignableFrom<ICommonBase>(actual);
    }
    #endregion
    #endregion

    #region GetFactory tests
    #region abstract IFactory GetFactory()
    [Fact]
    public void GetFactory_returnsExpected()
    {
        // Arrange
        TestCommonBase testCommonBase = new(_rootObject, null, new FactoryObject());

        // Act
        var actual = testCommonBase.GetFactory();

        // Assert
        Assert.NotNull(actual);
        Assert.IsAssignableFrom<IFactory>(actual);
    }
    #endregion
    #endregion
    #endregion
}
