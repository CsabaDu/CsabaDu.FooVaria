
namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class CommonBaseTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeBaseMeasurableTests()
    {
        factory = new FactoryClass();
        commonBase = new CommonBaseChild(factory);
    }
    #endregion

    #region Private fields
    private ICommonBase commonBase;
    private IFactory factory;
    private IFooVariaObject fooVariaObject;

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    //    #region Test methods
    //    #region Constructors
    //    #region CommonBase(IFactory)
    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_nullArg_IFactory_throws_ArgumentNullException()
    //    {
    //        // Arrange
    //        factory = null;

    //        // Act
    //        void attempt() => _ = new CommonBaseChild(factory);

    //        // Assert
    //        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
    //        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_validArg_IFactory_creates()
    //    {
    //        // Arrange
    //        factory = new FactoryImplementation();

    //        // Act
    //        var actual = new CommonBaseChild(factory);

    //        // Assert
    //        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
    //        Assert.IsNotNull(actual.Factory);
    //    }
    //    #endregion

    //    #region CommonBase(IFactory, ICommonBase)
    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_nullArg_IFactory_arg_ICommonBase_throws_ArgumentNullException()
    //    {
    //        // Arrange
    //        factory = null;
    //        commonBase = null;

    //        // Act
    //        void attempt() => _ = new CommonBaseChild(factory, commonBase);

    //        // Assert
    //        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
    //        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_validArg_IFactory_nullArg_ICommonBase_throws_ArgumentNullException()
    //    {
    //        // Arrange
    //        factory = new FactoryImplementation();
    //        commonBase = null;

    //        // Act
    //        void attempt() => _ = new CommonBaseChild(factory, commonBase);

    //        // Assert
    //        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
    //        Assert.AreEqual(ParamNames.commonBase, ex.ParamName);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_validArgs_IFactory_ICommonBase_creates()
    //    {
    //        // Arrange
    //        factory = new FactoryImplementation();
    //        commonBase = new CommonBaseChild(factory);

    //        // Act
    //        var actual = new CommonBaseChild(factory, commonBase);

    //        // Assert
    //        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
    //        Assert.IsNotNull(actual.Factory);
    //    }
    //    #endregion

    //    #region CommonBase(ICommonBase)
    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_nullArg_ICommonBase_throws_ArgumentNullException()
    //    {
    //        // Arrange
    //        commonBase = null;

    //        // Act
    //        void attempt() => _ = new CommonBaseChild(commonBase);

    //        // Assert
    //        var ex = Assert.ThrowsException<ArgumentNullException>(() => attempt());
    //        Assert.AreEqual(ParamNames.other, ex.ParamName);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    public void CommonBase_validArg_ICommonBase_creates()
    //    {
    //        // Arrange
    //        factory = new FactoryImplementation();
    //        commonBase = new CommonBaseChild(factory);

    //        // Act
    //        var actual = new CommonBaseChild(commonBase);

    //        // Assert
    //        Assert.IsInstanceOfType(actual, typeof(ICommonBase));
    //        Assert.IsNotNull(actual.Factory);
    //    }
    //    #endregion
    //    #endregion

    //    #region GetFactory
    //    #region GetFactory()
    //    [TestMethod, TestCategory("UnitTest")]
    //    public void GetFactory_returns_expected()
    //    {
    //        // Arrange
    //        factory = new BaseSpreadFactoryImplementation();
    //        commonBase = new CommonBaseChild(factory);

    //        // Act
    //        var actual = commonBase.GetFactory();

    //        // Assert
    //        Assert.AreEqual(factory, actual);
    //    }
    //    #endregion
    //    #endregion

    //    #region Validate
    //    #region Validate(IFooVariaObject?)
    //    [TestMethod, TestCategory("UnitTest")]
    //    public void Validate_nullArg_IFooVariaObject_throws_ArgumentNullException()
    //    {
    //        // Arrange
    //        fooVariaObject = null;

    //        // Act
    //        void attempt() => commonBase.Validate(fooVariaObject);

    //        // Assert
    //        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
    //        Assert.AreEqual(ParamNames.fooVariaObject, ex.ParamName);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    public void Validate_invalidArg_IFooVariaObject_throws_InvalidOperationException()
    //    {
    //        // Arrange
    //        fooVariaObject = new FooVariaObjectImplementation();

    //        // Act
    //        void attempt() => commonBase.Validate(fooVariaObject);

    //        // Assert
    //        _ = Assert.ThrowsException<InvalidOperationException>(attempt);
    //    }

    //    [TestMethod, TestCategory("UnitTest")]
    //    [DynamicData(nameof(GetCommonBaseValidateArgArrayList), DynamicDataSourceType.Method)]
    //    public void Validate_validArg_IFooVariaObject_returns(IFooVariaObject fooVariaObject)
    //    {
    //        // Arrange
    //        bool returned;

    //        // Act
    //        try
    //        {
    //            commonBase.Validate(fooVariaObject);
    //            returned = true;
    //        }
    //        catch
    //        {
    //            returned = false;
    //        }

    //        // Assert
    //        Assert.IsTrue(returned);
    //    }
    //    #endregion
    //    #endregion
    //    #endregion

    //    #region ArrayList methods
    //    private static IEnumerable<object[]> GetCommonBaseValidateArgArrayList()
    //    {
    //        return DynamicDataSources.GetCommonBaseValidateArgArrayList();
    //    }
    //    #endregion
}
