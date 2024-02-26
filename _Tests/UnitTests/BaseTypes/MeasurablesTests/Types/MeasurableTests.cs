namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurableTests
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
        _factory = new();
        _measurable = new(_factory);

        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _measurable.GetMeasureUnit_returns = _measureUnit;

        _measureUnitType = _measureUnit.GetType();
        _measureUnitCode = GetMeasureUnitCode(_measureUnitType);
    }
    #endregion

    #region Private fields
    private MeasureUnitCode _measureUnitCode;
    private MeasurableChild _measurable;
    private MeasurableFactoryClass _factory;
    private Enum _measureUnit;
    Type _measureUnitType;

    private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region Measurable(IMeasurableFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurableFactory_arg_MeasureunitTypeCode_throws_ArgumentNullException()
    {
        // Arrange
        _factory = null;

        // Act
        void attempt() => _ = new MeasurableChild(_factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArgs_IMeasurableFactory_MeasureunitTypeCode_creates()
    {
        // Arrange
        _factory = new MeasurableFactoryClass();

        // Act
        var actual = new MeasurableChild(_factory);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
    }
    #endregion

    #region Measurable(IMeasurable)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_nullArg_IMeasurable_throws_ArgumentNullException()
    {
        // Arrange
        _measurable = null;

        // Act
        void attempt() => _ = new MeasurableChild(_measurable);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurable_validArg_IMeasurable_creates()
    {
        // Arrange
        _measurable = new MeasurableChild(_factory);

        // Act
        var actual = new MeasurableChild(_measurable);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurable));
    }
    #endregion
    #endregion

    #region Equals
    #region Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        _measurable.GetMeasureUnit_returns = measureUnit;

        // Act
        var actual = _measurable.Equals(obj);

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
        Enum expected = (Enum)Enum.ToObject(_measureUnitType, 0);

        // Act
        var actual = _measurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetDefaultMeasureUnitNames
    #region GetDefaultMeasureUnitNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnitNames_returns_expected()
    {
        // Arrange
        string measureUnitCodeName = Enum.GetName(_measureUnitCode);
        IEnumerable<string> expected = Enum.GetNames(_measureUnitType).Select(x => x + measureUnitCodeName);

        // Act
        var actual = _measurable.GetDefaultMeasureUnitNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetFactory
    #region GetFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetFactory_returns_expected()
    {
        // Arrange
        // Act
        var actual = _measurable.GetFactory();

        // Assert
        Assert.IsNotNull(actual);
        Assert.IsInstanceOfType(_measurable.GetFactory(), typeof(IMeasurableFactory));
    }
    #endregion
    #endregion

    #region GetHashCode
    #region GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = _measureUnitCode.GetHashCode();

        // Act
        var actual = _measurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnit
    #region GetMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = RandomParams.GetRandomMeasureUnit();
        _measurable.GetMeasureUnit_returns = expected;

        // Act
        var actual = _measurable.GetMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetMeasureUnitCode
    #region GetMeasureUnitCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCode_returns_expected()
    {
        // Arrange
        MeasureUnitCode expected = _measureUnitCode;

        // Act
        var actual = _measurable.GetMeasureUnitCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    //#region GetMeasureUnitCodes
    //#region GetMeasureUnitCodes()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetMeasureUnitCodes_returns_expected()
    //{
    //    // Arrange
    //    IEnumerable<MeasureUnitCode> expected()
    //    {
    //        yield return _measureUnitCode;
    //    };

    //    // Act
    //    var actual = _measurable.GetMeasureUnitCodes();

    //    // Assert
    //    Assert.IsTrue(expected().SequenceEqual(actual));
    //}
    //#endregion
    //#endregion

    #region GetMeasureUnitType
    #region GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returns_expected()
    {
        // Arrange
        Type expected = MeasureUnitTypeSet.First(x => x.Name == Enum.GetName(_measureUnitCode));

        // Act
        var actual = _measurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region GetQuantityTypeCode
    #region GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        TypeCode expected = _measureUnitCode.GetQuantityTypeCode();

        // Act
        var actual = _measurable.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region HasMeasureUnitCode
    #region HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected)
    {
        // Arrange
        _measurable.GetMeasureUnit_returns = measureUnit;

        // Act
        var actual = _measurable.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    //#region IsValidMeasureUnitCode
    //#region IsValidMeasureUnitCode(MeasureUnitCode)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetMeasurableIsValidMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    //public void IsValidMeasureUnitCode_arg_MeasureUnitCode_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected)
    //{
    //    // Arrange
    //    _measurable.GetMeasureUnit_returns = measureUnit;

    //    // Act
    //    var actual = _measurable.IsValidMeasureUnitCode(measureUnitCode);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    #region ValidateMeasureUnit
    #region ValidateMeasureUnit(Enum, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.GetMeasureUnit_returns = measureUnitCode.GetDefaultMeasureUnit();
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(measureUnit, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.GetMeasureUnit_returns = measureUnitCode.GetDefaultMeasureUnit();
        _paramName = RandomParams.GetRandomParamName();
        bool returned = true;

        // Act
        try
        {
            _measurable.ValidateMeasureUnit(measureUnit, _paramName);
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

    #region ValidateMeasureUnitCode
    #region ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.GetMeasureUnit_returns = measureUnit;
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measureUnitCode, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_MeasureUnitCode_arg_string_returns()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();
        bool returned = true;

        // Act
        try
        {
            _measurable.ValidateMeasureUnitCode(_measureUnitCode, _paramName);
        }
        catch
        {
            returned = false;
        }

        // Assert
        Assert.IsTrue(returned);
    }
    #endregion

    #region ValidateMeasureUnitCode(IMeasurable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_nullArg_IMeasurable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_invalidArg_IMeasurable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        MeasurableChild measurable = new(_factory);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        measurable.GetMeasureUnit_returns = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measurable, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_IMeasurable_arg_string_returns()
    {
        // Arrange
        MeasurableChild measurable = new(_factory);
        measurable.GetMeasureUnit_returns = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        _paramName = RandomParams.GetRandomParamName();

        bool returned = true;

        // Act
        try
        {
            _measurable.ValidateMeasureUnitCode(_measurable, _paramName);
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

    //#region Static methods

    //#endregion
    #endregion

    #region DynamicDataSources
    private static IEnumerable<object[]> GetMeasurableEqualsArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitInvalidArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList()
    {
        return DynamicDataSources.GetMeasurableValidateMeasureUnitCodeInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSources.GetHasMeasureUnitCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
    {
        return DynamicDataSources.GetValidMeasureUnitArgsArrayList();
    }
    #endregion
}