namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.CommonTests;

internal sealed class DynamicDataSource : CommonDynamicDataSource
{
    #region Methods
    internal IEnumerable<object[]> GetCommonBaseArgs()
    {
        IFactory factory = new FactoryObject();

        testCase = "IFactory";
        IRootObject rootObject = factory;
        yield return argsToObjectArray();

        testCase = "CommonBase";
        rootObject = new CommonBaseChild(factory, null);
        yield return argsToObjectArray();

        #region argsToObjectArray method
        object[] argsToObjectArray()
        {
            TestCase_IRootObject args = new(testCase, rootObject);

            return args.ToObjectArray();
        }
        #endregion
    }
    #endregion
}
