namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Private fields
    private readonly Action<IRootObject> createCommonBaseChildAction
        = (IRootObject rootObject) => _ = new CommonBaseChild(rootObject, ParamNames.TestName);
    #endregion

    #region Helper methods
    #region DynamicDataSources
    private static IEnumerable<object[]> GetCommonBaseArgs()
    {
        DynamicDataSource dynamicDataSource = new();

        return dynamicDataSource.GetCommonBaseArgs();
    }
    #endregion

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region CommonBase
    #region CommonBase(IRootObject, string)
    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_nullArg_IRootobject_throws_ArgumentNullException()
    {
        // Arrange
        IRootObject rootObject = null;

        // Act
        void attempt() => createCommonBaseChildAction(rootObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.TestName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CommonBase_invalidArg_IRootobject_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        IRootObject rootObject = new RootObject();

        // Act
        void attempt() => createCommonBaseChildAction(rootObject);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.TestName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetCommonBaseArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = nameof(GetDisplayName))]
    public void CommonBase_validArg_IRootobject_creates(IRootObject rootObject)
    {
        // Arrange
        // Act
        var actual = new CommonBaseChild(rootObject, ParamNames.TestName);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
        Assert.IsNotNull(actual.Factory);
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    #endregion
}
