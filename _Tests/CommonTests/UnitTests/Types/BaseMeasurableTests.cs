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
        RandomParams = new();
        BaseMeasurableVariant = new(RandomParams);
        MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Factory = new FactoryImplementation();
        BaseMeasurable = new BaseMeasurableChild(Factory, MeasureUnitTypeCode);
    }
    #endregion

    #region Private fields
    private IBaseMeasurable BaseMeasurable;
    private BaseMeasurableVariant BaseMeasurableVariant;
    private IFactory Factory;
    private MeasureUnitTypeCode MeasureUnitTypeCode;
    private RandomParams RandomParams;

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
        IFactory factory = null;
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

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
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_MeasureunitTypeCode_createsInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();

        // Act
        var actual = new BaseMeasurableChild(factory, expected_MeasureUnitTypeCode);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_IFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        IFactory factory = null;
        Enum measureUnit = null;

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
        IFactory factory = new FactoryImplementation();
        Enum measureUnit = null;

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
        IFactory factory = new FactoryImplementation();

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_Enum_createsInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode expected_MeasureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(expected_MeasureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected_MeasureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion

    #region BaseMeasurable(IFactory, IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_IFactory_arg_IBaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        IFactory factory = null;
        IBaseMeasurable baseMeasurable = null;

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
        IFactory factory = new FactoryImplementation();
        IBaseMeasurable baseMeasurable = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(factory, baseMeasurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.baseMeasurable, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArgs_IFactory_IBaseMeasurable_createsInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable expected = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(factory, expected);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region BaseMeasurable(IBaseMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_nullArg_BaseMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        IBaseMeasurable other = null;

        // Act
        void attempt() => _ = new BaseMeasurableChild(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurable_validArg_IBaseMeasurable_createsInstance()
    {
        // Arrange
        IFactory factory = new FactoryImplementation();
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        IBaseMeasurable expected = new BaseMeasurableChild(factory, measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurableChild(expected);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurable));
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returnsExpected(bool expected, object other, MeasureUnitTypeCode measureUnitTypeCode)
    {
        // Arrange
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(Factory, measureUnitTypeCode);

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
    public void GetDefaultMeasureUnit_returnsExpected()
    {
        // Arrange
        Enum expected = BaseMeasurableVariant.GetDefaultMeasureUnit(MeasureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetDefaultNames
    #region GetDefaultNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultNames_returnsExpected()
    {
        // Arrange
        IEnumerable<string> expected = BaseMeasurableVariant.GetDefaultNames(MeasureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetDefaultNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returnsExpected()
    {
        // Arrange
        int expected = HashCode.Combine(typeof(IBaseMeasurable), MeasureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitType
    #region GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returnsExpected()
    {
        // Arrange
        Type expected = BaseMeasurableVariant.GetMeasureUnitType(MeasureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region GetMeasureUnitType(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_invalidArg_MeasureUnitTypeCode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => _ = BaseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_validArg_MeasureUnitTypeCode_returnsExpected()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Type expected = BaseMeasurableVariant.GetMeasureUnitType(measureUnitTypeCode);

        // Act
        var actual = BaseMeasurable.GetMeasureUnitType(measureUnitTypeCode);

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
        Enum measureUnit = null;

        // Act
        void attempt() => _ = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

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
        void attempt() => _ = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitTypeCode_validArg_Enum_returnsExpected()
    {
        // Arrange
        MeasureUnitTypeCode expected = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(expected);

        // Act
        var actual = BaseMeasurable.GetMeasureUnitTypeCode(measureUnit);

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
        IEnumerable<MeasureUnitTypeCode> expected = BaseMeasurableVariant.GetMeasureUnitTypeCodes();

        // Act
        var actual = BaseMeasurable.GetMeasureUnitTypeCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region HasMeasureUnitTypeCode
    #region HasMeasureUnitTypeCode(MeasureUnitTypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurableHasMeasureUnitTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitTypeCode_arg_MeasureUnitTypeCode_returnsExpected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, IBaseMeasurable baseMeasurable)
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
    public void HasMeasureUnitTypeCode_args_MeasureUnitTypeCode_Enum_returnsExpected(bool expected, MeasureUnitTypeCode measureUnitTypeCode, Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = BaseMeasurable.HasMeasureUnitTypeCode(measureUnitTypeCode, measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IsDefinedMeasureUnit
    #region IsDefinedMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBoolEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void IsDefinedMeasureUnit_arg_Enum_returnsExpected(bool expected, Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = BaseMeasurable.IsDefinedMeasureUnit(measureUnit);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValidateMeasureUnit
    #region ValidateMeasureUnit(Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit);

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
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_validArg_Enum_returns()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        IBaseMeasurable baseMeasurable = new BaseMeasurableChild(Factory, measureUnitTypeCode);
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
        Enum measureUnit = null;
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

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
        void attempt() => BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_validArgs_Enum_MeasureUnitTypeCode_returns()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        Enum measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);
        bool returned;

        // Act
        try
        {
            BaseMeasurable.ValidateMeasureUnit(measureUnit, measureUnitTypeCode);
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
        MeasureUnitTypeCode measureUnitTypeCode = SampleParams.NotDefinedMeasureUnitTypeCode;

        // Act
        void attempt() => BaseMeasurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnitTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitTypeCode_validArg_MeasureUnitTypeCode_returns()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        bool returned;

        // Act
        try
        {
            BaseMeasurable.ValidateMeasureUnitTypeCode(measureUnitTypeCode);
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

    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitMeasureUnitTypeCodeArgsArrayList();
    }

    #endregion
}
