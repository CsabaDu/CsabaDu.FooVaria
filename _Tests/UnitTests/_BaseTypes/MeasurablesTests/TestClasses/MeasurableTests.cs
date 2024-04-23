namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.MeasurablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class MeasurableTests
{
    #region Tested in parent classes' tests

    // Measurable(IRootObject rootObject, string paramName)
    // IFactory ICommonBase.GetFactory()

    #endregion

    #region Private fields
    private MeasurableChild _measurable;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    private const string DisplayName = nameof(GetDisplayName);
    #endregion
    #endregion

    #region Initialize
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        Fields.SetMeasureUnit(Fields.RandomParams.GetRandomMeasureUnit());
        SetMeasurableChild();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region bool Equals
    #region override Measurable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_object_returns_expected(string testCase, bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        SetMeasurableChild(measureUnit);

        // Act
        var actual = _measurable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region Enum GetBaseMeasureUnit
    #region abstract IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        Enum expected = Fields.RandomParams.GetRandomMeasureUnit();

        SetMeasurableChild(expected);

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
    #region override Measurable.GetHashCode()
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
    #region virtual IMeasureUnitCode.GetMeasureUnitCode()
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
    #region virtual IQuantityType.GetQuantityTypeCode()
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
    #region virtual IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetHasMeasureUnitCodeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void HasMeasureUnitCode_arg_MeasureUnitCode_returns_expected(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode, bool expected)
    {
        // Arrange
        SetMeasurableChild(measureUnit);

        // Act
        var actual = _measurable.HasMeasureUnitCode(measureUnitCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnit
    #region virtual IDefaultMeasureUnit.ValidateMeasureUnit(Enum?, string)
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
    [DynamicData(nameof(GetValidateMeasureUnitInvalidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        Fields.measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        SetMeasurableChild();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(measureUnit, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitValidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        Fields.measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        SetMeasurableChild();

        // Act
        void attempt() => _measurable.ValidateMeasureUnit(measureUnit, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
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
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();
        IMeasurable measurable = GetMeasurableChild(Fields.measureUnit);

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
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        MeasurableChild measurable = GetMeasurableChild(Fields.measureUnit);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _measurable.ValidateMeasureUnitCode(measurable, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion

    #region virtual IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitCodeInvalidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        SetMeasurableChild(measureUnit);

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
        void attempt() => _measurable.ValidateMeasureUnitCode(Fields.measureUnitCode, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion

    //#region Static methods

    //#endregion
    #endregion

    #region Private methods
    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitInvalidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitInvalidArgs();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitCodeInvalidArgs();
    }

    private static IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    {
        return DynamicDataSource.GetHasMeasureUnitCodeArgs();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitValidArgs();
    }
    #endregion

    private void SetMeasurableChild(Enum measureUnit, IBaseMeasurementFactory factory = null)
    {
        _measurable = GetMeasurableChild(measureUnit, factory);
    }

    private void SetMeasurableChild()
    {
        SetMeasurableChild(Fields.measureUnit);
    }
    #endregion
}
