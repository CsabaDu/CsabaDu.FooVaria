namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseQuantifiableTests
{
    #region Tested in parent classes' tests

    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseQuantifiableTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeBaseQuantifiableTests()
    {
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit();
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        Fields.measureUnitType = Fields.measureUnit.GetType();
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
    }

    [TestCleanup]
    public void CleanupBaseQuantifiableTests()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Private fields
    private BaseQuantifiableChild _baseQuantifiable;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region bool Equals
    #region BaseQuantifiable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit, decimal defaultQuantity)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);

        // Act
        var actual = _baseQuantifiable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, null, Fields.defaultQuantity);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
        LimiterBaseQuantifiableOblect limiter = new(Fields.RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
                GetDefaultQuantity = otherQuantity,
            },
            LimitMode = Fields.limitMode,
        };
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #endregion

    #region decimal GetDefaultQuantity
    #region IDefaultQuantity.GetDefaultQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultQuantity_returns_expected()
    {
        // Arrange
        decimal expected = Fields.defaultQuantity;
        SetBaseQuantifiableChild(null, null, expected);

        // Act
        var actual = _baseQuantifiable.GetDefaultQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region Measurable.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        var expected = HashCode.Combine(Fields.measureUnitCode, Fields.defaultQuantity);

        // Act
        var actual = _baseQuantifiable.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateQuantity
    #region IBaseQuantifiable.ValidateQuantity(ValueType?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateQuantity_nullArg_ValueType_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetBaseQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();
        ValueType quantity = null;

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateQuantityInvalidQuantityTypeCodeArg), DynamicDataSourceType.Method)]
    public void ValidateQuantity_invalidArg_ValueType_arg_string_throws_ArgumentOutOfRangeException(TypeCode typeCode)
    {
        // Arrange
        SetBaseQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        ValueType quantity = Fields.RandomParams.GetRandomValueType(typeCode);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateQuantityValidQuantityTypeCodeArg), DynamicDataSourceType.Method)]
    public void ValidateQuantity_validArg_ValueType_arg_string_returns_expected(TypeCode typeCode)
    {
        // Arrange
        SetBaseQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        ValueType quantity = Fields.RandomParams.GetRandomValueType(typeCode);

        // Act
        void attempt() => _baseQuantifiable.ValidateQuantity(quantity, null);

        // Assert
        Assert.IsTrue(Returned(attempt));
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    private void SetBaseQuantifiableChild(Enum measureUnit, IBaseQuantifiableFactory factory, decimal defaultQuantity)
    {
        _baseQuantifiable = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetDefaultQuantity = defaultQuantity,
            }
        };
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable<object[]> GetFitsInArgs()
    {
        return DynamicDataSource.GetFitsInArgs();
    }

    private static IEnumerable<object[]> GetValidateQuantityInvalidQuantityTypeCodeArg()
    {
        return DynamicDataSource.GetValidateQuantityInvalidQuantityTypeCodeArg();
    }

    private static IEnumerable<object[]> GetValidateQuantityValidQuantityTypeCodeArg()
    {
        return DynamicDataSource.GetValidateQuantityValidQuantityTypeCodeArg();
    }
    #endregion
    #endregion
}
