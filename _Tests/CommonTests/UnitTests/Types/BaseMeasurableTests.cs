using CsabaDu.FooVaria.Tests.TestSupport.Fakes.Common.Factories;

namespace CsabaDu.FooVaria.Tests.CommonTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurableTests
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
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        factory = new FactoryImplementation();
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);
    }
    #endregion

    #region Private fields
    private IBaseMeasurable baseMeasurable;
    private IFactory factory;
    private Enum measureUnit;
    private MeasureUnitTypeCode measureUnitTypeCode;

    #region Readonly fields
    private readonly BaseMeasurableVariant BaseMeasurableVariant = new();
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurable(IFactory, MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_IFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArg_IFactory_invalidArg_MeasureunitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        factory = new FactoryImplementation();
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_MeasureunitTypeCode_creates()
    {
        // Arrange
        factory = new FactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_IFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArg_IFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        factory = new FactoryImplementation();
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void BaseMeasurable_validArg_IFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        factory = new FactoryImplementation();

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_Enum_creates()
    {
        // Arrange
        factory = new FactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_IFactory_arg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArg_IFactory_nullArg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        factory = new FactoryImplementation();
        baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.commonBase, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_IBaseMeasurable_creates()
    {
        // Arrange
        factory = new FactoryImplementation();
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(baseMeasurable, actual);
    }
    #endregion

    #region BaseMeasurable(IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_BaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArg_IBaseMeasurable_creates()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(baseMeasurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(baseMeasurable, actual);
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object other, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetDefaultMeasureUnit
    #region GetDefaultMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_returns_expected()
    {
        // Arrange
        measureUnit = BaseMeasurableVariant.GetDefaultMeasureUnit(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(measureUnit, actual);
    }
    #endregion
    #endregion

    #region GetDefaultNames
    #region GetDefaultNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultNames_returns_expected()
    {
        // Arrange
        IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetDefaultNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = HashCode.Combine(typeof(IBaseMeasurable), measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitType
    #region GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returns_expected()
    {
        // Arrange
        Type expected = BaseMeasurableVariant.GetMeasureUnitType(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasureUnitType(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = baseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_validArg_MeasureUnitTypeCode_returns_expected()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Type expected = BaseMeasurableVariant.GetMeasureUnitType(measureUnitTypeCode);

        // Act
        var actual = baseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitTypeCode
    #region GetMeasureUnitTypeCode(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCode_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;

        // Act
        void attempt() => _ = baseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void GetMeasureUnitTypeCode_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = baseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCode_validArg_Enum_returns_expected()
    {
        // Arrange
        MeasureUnitTypeCode expected = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(expected);

        // Act
        var actual = baseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region HasMeasureUnitTypeCode
    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitTypeCode_arg_MeasureUnitTypeCode_returns_expected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, IBaseMeasurable baseMeasurable)
    {
        // Arrange
        // Act
        var actual = baseMeasurable.HasMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode, Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitTypeCode_args_MeasureUnitTypeCode_Enum_returns_expected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = baseMeasurable.HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IsDefinedMeasureUnit
    #region IsDefinedMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBoolEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void IsDefinedMeasureUnit_arg_Enum_returns_expected(bool expected, Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = baseMeasurable.IsDefinedMeasureUnit(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Validate
    #region Validate(IFooVariaObject?)
    [TestMethod, TestCategory("UnitTest")]
    public void Validate_nullArg_IFooVariaObject_throws_ArgumentNullException()
    {
        // Arrange
        ICommonBase other = null;

        // Act
        void attempt() => baseMeasurable.Validate(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.fooVariaObject, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableValidateInvalidArgArrayList), DynamicDataSourceType.Method)]
    public void Validate_invalidArg_IFooVariaObject_throws_ArgumentOutOfRangeException(MeasureUnitTypeCode measureUnitTypeCode, ICommonBase other) // TODO MeasureUnitTypeCode
    {
        // Arrange
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        void attempt() => baseMeasurable.Validate(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableValidateValidArgArrayList), DynamicDataSourceType.Method)]
    public void Validate_validArg_IFooVariaObject_returns(MeasureUnitTypeCode measureUnitTypeCode, IFooVariaObject fooVariaObject)
    {
        // Arrange
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            baseMeasurable.Validate(fooVariaObject);
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
        void attempt() => baseMeasurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => baseMeasurable.ValidateMeasureUnit(measureUnit);

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
        baseMeasurable = new BaseMeasurableChild(factory, measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            baseMeasurable.ValidateMeasureUnit(measureUnit);
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

    #region ValidateMeasureUnit(Enum, MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_MeasureUnitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => baseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArgs_Enum_MeasureUnitTypeCode_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitTypeCode measureUnitTypeCode, string paramName)
    {
        // Arrange
        // Act
        void attempt() => baseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_validArgs_Enum_MeasureUnitTypeCode_returns()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            baseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
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
    public void ValidateMeasureUnitTypeCode_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => baseMeasurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitTypeCode_validArg_MeasureUnitTypeCode_returns()
    {
        // Arrange
        measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        bool returned;

        // Act
        try
        {
            baseMeasurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
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

    private static IEnumerable<object[]> GetBaseMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList();

    }

    private static IEnumerable<object[]> GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableHasMeasureUnitTypeCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetBoolEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetBoolEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurableValidateInvalidArgArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableValidateInvalidArgArrayList();

    }

    private static IEnumerable<object[]> GetBaseMeasurableValidateValidArgArrayList()
    {
        return DynamicDataSources.GetBaseMeasurableValidateValidArgArrayList();
    }

    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList();
    }
    #endregion
}
