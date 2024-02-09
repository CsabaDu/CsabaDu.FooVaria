//namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types;

//[TestClass, TestCategory("UnitTest")]
//public sealed class MeasurableTests
//{
//    #region Initialize
//    [ClassInitialize]
//    public static void InitializeMeasurableTestsClass(TestContext context)
//    {
//        DynamicDataSources = new();
//    }

//    [TestInitialize]
//    public void InitializeMeasurableTests()
//    {
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        factory = new MeasurableFactoryImplementation();
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);
//    }
//    #endregion

//    #region Private fields
//    IBaseMeasurable baseMeasurable;
//    private IMeasurableFactory factory;
//    private IMeasurable measurable;
//    private Enum measureUnit;
//    private MeasureUnitTypeCode measureUnitTypeCode;
//    private IFooVariaObject fooVariaObject;

//    #region Readonly fields
//    private readonly RandomParams RandomParams = new();
//    #endregion

//    #region Static fields
//    private static DynamicDataSources DynamicDataSources;
//    #endregion
//    #endregion

//    #region Test methods
//    #region Constructors
//    #region Measurable(IMeasurableFactory, MeasureUnitTypeCode)
//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_nullArg_IMeasurableFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
//    {
//        // Arrange
//        factory = null;
//        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.factory, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_invalidArg_IMeasurableFactory_invalidArg_MeasureunitTypeCode_throws_InvalidEnumArgumentException()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArgs_IMeasurableFactory_MeasureunitTypeCode_creates()
//    {
//        // Arrange
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

//        // Act
//        var actual = new MeasurableChild(factory, measureUnitTypeCode);

//        // Assert
//        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
//        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
//    }
//    #endregion

//    #region Measurable(IMeasurableFactory, Enum)
//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_nullArg_IMeasurableFactory_arg_Enum_throws_ArgumentNullException()
//    {
//        // Arrange
//        factory = null;
//        measureUnit = null;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.factory, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArg_IMeasurableFactory_nullArg_Enum_throws_ArgumentNullException()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        measureUnit = null;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
//    public void Measurable_validArg_IMeasurableFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArgs_IMeasurableFactory_Enum_creates()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);

//        // Act
//        var actual = new MeasurableChild(factory, measureUnit);

//        // Assert
//        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
//        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
//    }
//    #endregion

//    #region Measurable(IMeasurableFactory, IBaseMeasurable)
//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_nullArg_IMeasurableFactory_arg_IBaseMeasurable_throws_ArgumentNullException()
//    {
//        // Arrange
//        factory = null;
//        baseMeasurable = null;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.factory, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArg_IMeasurableFactory_nullArg_IBaseMeasurable_throws_ArgumentNullException()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        baseMeasurable = null;

//        // Act
//        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.commonBase, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArgs_IMeasurableFactory_IBaseMeasurable_creates()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

//        // Act
//        var actual = new MeasurableChild(factory, baseMeasurable);

//        // Assert
//        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
//        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
//    }
//    #endregion

//    #region Measurable(IMeasurable)
//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_nullArg_Measurable_throws_ArgumentNullException()
//    {
//        // Arrange
//        measurable = null;

//        // Act
//        void attempt() => _ = new MeasurableChild(measurable);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.other, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void Measurable_validArg_IMeasurable_creates()
//    {
//        // Arrange
//        factory = new MeasurableFactoryImplementation();
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);

//        // Act
//        var actual = new MeasurableChild(measurable);

//        // Assert
//        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
//        Assert.AreEqual(measurable, actual);
//    }
//    #endregion
//    #endregion

//    #region Equals
//    #region Equals(object?)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
//    public void Equals_arg_object_returns_expected(bool expected, object other, MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        // Arrange
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);

//        // Act
//        var actual = measurable.Equals(other);

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetFactory
//    #region GetFactory()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetFactory_returns_expected()
//    {
//        // Arrange
//        // Act
//        var actual = measurable.GetFactory();

//        // Assert
//        Assert.IsNotNull(actual);
//        Assert.IsInstanceOfType(measurable.GetFactory(), typeof(IMeasurableFactory));
//    }
//    #endregion
//    #endregion

//    #region GetHashCode
//    #region GetHashCode()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetHashCode_returns_expected()
//    {
//        // Arrange
//        int expected = HashCode.Combine(typeof(IMeasurable), measureUnitTypeCode);

//        // Act
//        var actual = measurable.GetHashCode();

//        // Assert
//        Assert.AreEqual(expected, actual);
//    }
//    #endregion
//    #endregion

//    #region GetMeasureUnitTypeCodes
//    #region GetMeasureUnitTypeCodes()
//    [TestMethod, TestCategory("UnitTest")]
//    public void GetMeasureUnitTypeCodes_returns_expected()
//    {
//        // Arrange
//        IEnumerable<MeasureUnitTypeCode> expected = Enum.GetValues<MeasureUnitTypeCode>();

//        // Act
//        var actual = measurable.GetMeasureUnitTypeCodes();

//        // Assert
//        Assert.IsTrue(expected.SequenceEqual(actual));
//    }
//    #endregion
//    #endregion

//    #region IsValidMeasureUnitTypeCode
//    #region IsValidMeasureUnitTypeCode(MeasureUnitTypeCode)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetBoolMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
//    public void IsValidMeasureUnitTypeCode_arg_MeasureUnitTypeCode_returns_expected(bool expected, MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        // Arrange
//        // Act
//        var actual = measurable.IsValidMeasureUnitTypeCode(measureUnitTypeCode);

//        // Assert
//        Assert.AreEqual(expected, actual);
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
//        void attempt() => measurable.Validate(fooVariaObject);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.fooVariaObject, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetMeasurableValidateInvalidArgArrayList), DynamicDataSourceType.Method)]
//    public void Validate_invalidArg_IFooVariaObject_throws_ArgumentOutOfRangeException(IFooVariaObject fooVariaObject, string name, MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        // Arrange
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);

//        // Act
//        void attempt() => measurable.Validate(fooVariaObject);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
//        Assert.AreEqual(name, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetMeasurableValidateValidArgArrayList), DynamicDataSourceType.Method)]
//    public void Validate_validArg_IFooVariaObject_returns(MeasureUnitTypeCode measureUnitTypeCode, IFooVariaObject fooVariaObject)
//    {
//        // Arrange
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);
//        bool returned;

//        // Act
//        try
//        {
//            measurable.Validate(fooVariaObject);
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

//    #region ValidateMeasureUnit
//    #region ValidateMeasureUnit(Enum)
//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnit_nullArg_Enum_throws_ArgumentNullException()
//    {
//        // Arrange
//        measureUnit = null;

//        // Act
//        void attempt() => measurable.ValidateMeasureUnit(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetMeasurableValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnit_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
//    {
//        // Arrange
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);

//        // Act
//        void attempt() => measurable.ValidateMeasureUnit(measureUnit);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnit_validArg_Enum_returns()
//    {
//        // Arrange
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);
//        bool returned;

//        // Act
//        try
//        {
//            measurable.ValidateMeasureUnit(measureUnit);
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

//    #region ValidateMeasureUnitTypeCode
//    #region ValidateMeasureUnitTypeCode(MeasureUnitTypeCode)
//    [TestMethod, TestCategory("UnitTest")]
//    [DynamicData(nameof(GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
//    public void ValidateMeasureUnitTypeCode_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode, IMeasurable measurable)
//    {
//        // Arrange
//        // Act
//        void attempt() => measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);

//        // Assert
//        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
//        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
//    }

//    [TestMethod, TestCategory("UnitTest")]
//    public void ValidateMeasureUnitTypeCode_validArg_MeasureUnitTypeCode_returns()
//    {
//        // Arrange
//        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
//        measurable = new MeasurableChild(factory, measureUnitTypeCode);
//        bool returned;

//        // Act
//        try
//        {
//            measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
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
//    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
//    {
//        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
//    }

//    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
//    {
//        return DynamicDataSources.GetMeasurableEqualsArgsArrayList();
//    }

//    private static IEnumerable<object[]> GetBoolMeasureUnitTypeCodeArgsArrayList()
//    {
//        return DynamicDataSources.GetBoolMeasureUnitTypeCodeArgsArrayList();
//    }

//    private static IEnumerable<object[]> GetMeasurableValidateInvalidArgArrayList()
//    {
//        return DynamicDataSources.GetMeasurableValidateInvalidArgArrayList();
//    }

//    private static IEnumerable<object[]> GetMeasurableValidateValidArgArrayList()
//    {
//        return DynamicDataSources.GetMeasurableValidateValidArgArrayList();
//    }

//    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
//    {
//        return DynamicDataSources.GetMeasurableValidateMeasureUnitInvalidArgsArrayList();
//    }

//    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList()
//    {
//        return DynamicDataSources.GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList();
//    }
//    #endregion
//}