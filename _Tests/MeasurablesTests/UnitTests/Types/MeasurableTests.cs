namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class MeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        factory = new MeasurableFactoryImplementation();
        measurable = new MeasurableChild(factory, measureUnitTypeCode);
    }
    #endregion

    #region Private fields
    IBaseMeasurable baseMeasurable;
    private IMeasurableFactory factory;
    private IMeasurable measurable;
    private Enum measureUnit;
    private MeasureUnitTypeCode measureUnitTypeCode;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region Measurable(IMeasurableFactory, MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_invalidArg_IMeasurableFactory_invalidArg_MeasureunitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_MeasureunitTypeCode_createsInstance()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new MeasurableChild(factory, measureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurableFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        measureUnit = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurableFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        measureUnit = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void Measurable_validArg_IMeasurableFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();

        // Act
        void attempt() => _ = new MeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_Enum_createsInstance()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurableFactory, IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        baseMeasurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurableFactory_nullArg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        baseMeasurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.commonBase, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_IBaseMeasurable_createsInstance()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(factory, baseMeasurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region Measurable(IMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_Measurable_throws_ArgumentNullException()
    {
        // Arrange
        measurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(measurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurable_createsInstance()
    {
        // Arrange
        factory = new MeasurableFactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measurable = new MeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new MeasurableChild(measurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
        Assert.AreEqual(measurable, actual);
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returnsExpected(bool expected, object other, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        measurable = new MeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = measurable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returnsExpected()
    {
        // Arrange
        // Act
        var actual = measurable.GetFactory();

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(measurable.GetFactory(), typeof(IMeasurableFactory));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returnsExpected()
    {
        // Arrange
        int expected = HashCode.Combine(typeof(IMeasurable), measureUnitTypeCode);

        // Act
        var actual = measurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitTypeCodes
    #region GetMeasureUnitTypeCodes()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCodes_returnsExpected()
    {
        // Arrange
        IEnumerable<MeasureUnitTypeCode> expected = Enum.GetValues<MeasureUnitTypeCode>();

        // Act
        var actual = measurable.GetMeasureUnitTypeCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region IsValidMeasureUnitTypeCode
    #region IsValidMeasureUnitTypeCode(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBoolMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void IsValidMeasureUnitTypeCode_arg_MeasureUnitTypeCode_returnsExpected(bool expected, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        // Act
        var actual = measurable.IsValidMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Validate
    #region Validate(ICommonBase?)
    [TestMethod, TestCategory("UnitTest")]
    public void Validate_nullArg_ICommonBase_throws_ArgumentNullException()
    {
        // Arrange
        ICommonBase other = null;

        // Act
        void attempt() => measurable.Validate(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateArgArrayList), DynamicDataSourceType.Method)]
    public void Validate_invalidArg_ICommonBase_throws_ArgumentOutOfRangeException(MeasureUnitTypeCode measureUnitTypeCode, ICommonBase other)
    {
        // Arrange
        measurable = new MeasurableChild(factory, measureUnitTypeCode);

        // Act
        void attempt() => measurable.Validate(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Validate_validArg_ICommonBase_returns()
    {
        // Arrange
        IMeasurable other = new MeasurableChild(factory, measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            measurable.Validate(other);
            returned = true;
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion

    #region Validate(IFactory?)
    [TestMethod, TestCategory("UnitTest")]
    public void Validate_nullArg_IFactory_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;

        // Act
        void attempt() => measurable.Validate(factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Validate_validArg_IFactory_returns()
    {
        // Arrange
        bool returned;

        // Act
        try
        {
            measurable.Validate(factory);
            returned = true;
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnit
    #region ValidateMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;

        // Act
        void attempt() => measurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        measurable = new MeasurableChild(factory, measureUnitTypeCode);

        // Act
        void attempt() => measurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_validArg_Enum_returns()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        measurable = new MeasurableChild(factory, measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            measurable.ValidateMeasureUnit(measureUnit);
            returned = true;
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnitTypeCode
    #region ValidateMeasureUnitTypeCode(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitTypeCode_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException(MeasureUnitTypeCode measureUnitTypeCode, IMeasurable measurable)
    {
        // Arrange
        // Act
        void attempt() => measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitTypeCode_validArg_MeasureUnitTypeCode_returns()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measurable = new MeasurableChild(factory, measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            measurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
            returned = true;
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion
    #endregion
    #endregion

    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetBoolMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetBoolMeasureUnitTypeCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateArgArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateArgArrayList();
    }
    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitTypeCodeArgsArrayList();
    }
    #endregion
}