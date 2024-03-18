namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        _measurable = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.measureUnit,
            }
        };

        Fields.measureUnitType = Fields.measureUnit.GetType();
    }

    [TestCleanup]
    public void CleanupMeasurableTests()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Private fields
    private MeasurableChild _measurable;
    //private Enum measureUnit;
    //private MeasureUnitCode measureUnitCode;
    //private Type measureUnitType;
    //private string paramName;

    #region Readonly fields
    private readonly DataFields Fields = new();
    //private readonly RandomParams RandomParams = new();
    //private readonly RootObject RootObject = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region Tested in parent classes' tests

    // Measurable(IRootObject rootObject, string paramName)
    // IFactory ICommonBase.GetFactory()

    #endregion

    #region bool Equals
    #region Measurable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };

        // Act
        var actual = _measurable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Enum GetBaseMeasureUnit
    #region IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = Fields.RandomParams.GetRandomMeasureUnit();
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = expected,
        };

        // Act
        var actual = _measurable.GetBaseMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Enum GetDefaultMeasureUnit
    #region IDefaultMeasureUnit.GetDefaultMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = (Enum)Enum.ToObject(Fields.measureUnitType, 0);

        // Act
        var actual = _measurable.GetDefaultMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IEnumerable<string> GetDefaultMeasureUnitNames
    #region IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultMeasureUnitNames_returns_expected()
    {
        // Arrange
        string measureUnitCodeName = Enum.GetName(Fields.measureUnitCode);
        string getDefaultMeasureUnitName(string measureUnitName) => measureUnitName + measureUnitCodeName;
        IEnumerable<string> expected = Enum.GetNames(Fields.measureUnitType).Select(getDefaultMeasureUnitName);

        // Act
        var actual = _measurable.GetDefaultMeasureUnitNames();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region Measurable.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        int expected = Fields.measureUnitCode.GetHashCode();

        // Act
        var actual = _measurable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region MeasureUnitCode GetMeasureUnitCode
    #region IMeasureUnitCode.GetMeasureUnitCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCode_returns_expected()
    {
        // Arrange
        MeasureUnitCode expected = Fields.measureUnitCode;

        // Act
        var actual = _measurable.GetMeasureUnitCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Type GetMeasureUnitType
    #region IMeasureUnit.GetMeasureUnitType()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitType_returns_expected()
    {
        // Arrange
        Type expected = Fields.measureUnit.GetType();

        // Act
        var actual = _measurable.GetMeasureUnitType();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TypeCode GetQuantityTypeCode
    #region IQuantityType.GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        TypeCode expected = Fields.measureUnitCode.GetQuantityTypeCode();

        // Act
        var actual = _measurable.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool HasMeasureUnitCode
    #region IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgsArrayList), DynamicDataSourceType.Method)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected)
    {
        // Arrange
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };

        // Act
        var actual = _measurable.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnit
    #region IDefaultMeasureUnit.ValidateMeasureUnit(Enum?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(null, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
        };
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(measureUnit, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidMeasureUnitArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = measureUnitCode.GetDefaultMeasureUnit(),
        };
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void validator() => _measurable.ValidateMeasureUnit(measureUnit, Fields.paramName);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnitCode
    #region IMeasurable.ValidateMeasureUnitCode(IMeasurable?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_nullArg_IMeasurable_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(null, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_invalidArg_IMeasurable_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        MeasurableChild measurable = new(Fields.RootObject, Fields.paramName);
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        measurable.Return = new()
        {
            GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
        };
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measurable, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_IMeasurable_arg_string_returns()
    {
        // Arrange
        MeasurableChild measurable = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
            }
        };
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void validator() => _measurable.ValidateMeasureUnitCode(measurable, Fields.paramName);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion

    #region IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitCodeInvalidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        _measurable.Return = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measureUnitCode, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_MeasureUnitCode_arg_string_returns()
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void validator() => _measurable.ValidateMeasureUnitCode(Fields.measureUnitCode, Fields.paramName);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion
    #endregion

    //#region Static methods

    //#endregion
    #endregion

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgsArrayList()
    {
        return DynamicDataSource.GetEqualsArgsArrayList();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitInvalidArgsArrayList()
    {
        return DynamicDataSource.GetValidateMeasureUnitInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgsArrayList()
    {
        return DynamicDataSource.GetValidateMeasureUnitCodeInvalidArgsArrayList();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgsArrayList()
    {
        return DynamicDataSource.GetHasMeasureUnitCodeArgsArrayList();
    }

    private static IEnumerable<object[]> GetValidMeasureUnitArgsArrayList()
    {
        return DynamicDataSource.GetValidMeasureUnitArgsArrayList();
    }
    #endregion
}
