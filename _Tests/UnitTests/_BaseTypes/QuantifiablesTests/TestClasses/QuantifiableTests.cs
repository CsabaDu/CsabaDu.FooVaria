namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class QuantifiableTests
{
    #region Tested in parent classes' tests

    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

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
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit();
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Private fields
    private QuantifiableChild _quantifiable;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region int CompareTo
    #region virtual IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity);

        IQuantifiable other = null;
        const int expected = 1;

        // Act
        var actual = _quantifiable.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);
        int expected = _quantifiable.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _quantifiable.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region virtual IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_arg_IQuantifiable_returns_expected(Enum measureUnit, decimal defaultQuantity, bool expected, IQuantifiable other)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region override ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        ILimiter limiter = null;

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        ILimiter limiter = GetLimiterQuantifiableObject(Fields.limitMode, Fields.measureUnit, otherQuantity);
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region virtual IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity);

        ILimiter limiter = null;

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIQuantifiableLimitModeArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArgs_IQuantifiable_LimitMode_returns_null(Enum measureUnit, LimitMode? limitMode, IQuantifiable other)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        IQuantifiable quantifiable = GetQuantifiableChild(otherQuantity, Fields.measureUnit);
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(quantifiable, Fields.limitMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValueType GetBaseQuantity
    #region IQuantity.GetBaseQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseQuantity_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        TypeCode typeCode = Fields.measureUnitCode.GetQuantityTypeCode();
        ValueType expected = (ValueType)Fields.defaultQuantity.ToQuantity(typeCode);

        // Act
        var actual = _quantifiable.GetBaseQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal GetDecimalQuantity
    #region IDecimalQuantity.GetDecimalQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDecimalQuantity_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        ValueType quantity = _quantifiable.GetBaseQuantity();
        decimal expected = (decimal)quantity.ToQuantity(TypeCode.Decimal);

        // Act
        var actual = _quantifiable.GetDecimalQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region MeasureUnitCode GetMeasureUnitCode
    #region override sealed IMeasureUnitCode.GetMeasureUnitCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();
        MeasureUnitCode expected = Fields.measureUnitCode;

        // Act
        var actual = _quantifiable.GetMeasureUnitCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable GetQuantifiable
    #region IQuantifiable.GetQuantifiable(MeasureUnitCode, decimal)
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantifiable_invalidArg_MeasureUnitCode_arg_decimal_throws_InvalidEnumArgumentException()
    {
        // Arrange
        QuantifiableFactoryObject factory = new();
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit, factory);

        // Act
        var actual = _quantifiable.GetQuantifiable(Fields.measureUnitCode, Fields.defaultQuantity);

        // Assert
        Assert.IsInstanceOfType<IQuantifiable>(actual);
    }
    #endregion
    #endregion

    #region object? GetQuantity
    #region IRound<IQuantifiable>.GetQuantity(RoundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild();

        // Act
        void attempt() => _ = _quantifiable.GetQuantity(SampleParams.NotDefinedRoundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory ("UnitTest")]
    [DynamicData(nameof(GetGetQuantityRoundingModeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_validArg_RoundingMode_returns_expected(Enum measureUnit, decimal defaultQuantity, object expected, RoundingMode roundingMode)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.GetQuantity(roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IQuantity.GetQuantity(TypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityInvalidTypeCodeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_invalidArg_TypeCode_throws_InvalidEnumArgumentException(TypeCode typeCode)
    {
        // Arrange
        decimal quantity = typeCode == TypeCode.UInt64 ? Fields.RandomParams.GetRandomNegativeDecimal() : Fields.defaultQuantity;
        SetQuantifiableChild(quantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.GetQuantity(typeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.quantityTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityValidTypeCodeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_validArg_TypeCode_returns_expected(Enum measureUnit, decimal defaultQuantity, object expected, TypeCode quantityTypeCode)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.GetQuantity(quantityTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region IExchangeable<Enum>.IsExchangeableTo(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArg), DynamicDataSourceType.Method)]
    public void IsExchangeableTo_arg_Enum_returns_expected(bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.IsExchangeableTo(context);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal ProportionalTo
    #region IProportional<IQuantifiable>.ProportionalTo(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_nullArg_IQuantifiable_throws_ArgumentNullException()
    {
        // Arrange
        SetQuantifiableChild();
        IQuantifiable other = null;

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_invalidArg_IQuantifiable_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = 0;
        IQuantifiable other = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);
        decimal expected = Math.Abs(_quantifiable.GetDefaultQuantity() / other.GetDefaultQuantity());

        // Act
        var actual = _quantifiable.ProportionalTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable Round
    #region abstract IRound<IQuantifiable>.Round(RoundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void Round_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild();

        // Act
        void attempt() => _ = _quantifiable.Round(SampleParams.NotDefinedRoundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Round_validArg_RoundingMode_returns_expected()
    {
        //// Arrange
        SetQuantifiableChild();
        Fields.roundingMode = Fields.RandomParams.GetRandomRoundingMode();

        Fields.defaultQuantity = Fields.defaultQuantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        var actual = _quantifiable.Round(Fields.roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool TryExchangeTo
    #region abstract ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum?, out IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetTryExchangeToArgs), DynamicDataSourceType.Method)]
    public void TryExchangeTo_arg_returns_success_out_expected(Enum measureUnit, Enum otherMeasureUnit, IQuantifiable expected)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        var success = _quantifiable.TryExchangeTo(otherMeasureUnit, out IQuantifiable actual);

        // Assert
        Assert.IsTrue(DoesSucceedAsExpected(success, actual));
        Assert.AreEqual(expected?.GetType(), actual?.GetType());
    }
    #endregion
    #endregion

    #region void IMeasureUnitCode.ValidateMeasureUnitCode
    #region override sealed IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitCodeInvalidArgs), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        void attempt() => _quantifiable.ValidateMeasureUnitCode(measureUnitCode, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnitCode_validArg_MeasureUnitCode_arg_string_returns()
    {
        // Arrange
        SetQuantifiableChild();
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void validator() => _quantifiable.ValidateMeasureUnitCode(Fields.measureUnitCode, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(validator));
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    private void SetQuantifiableChild(decimal defaultQuantity, Enum measureUnit = null, IQuantifiableFactory factory = null)
    {
        _quantifiable = GetQuantifiableChild(defaultQuantity, measureUnit, factory);
    }

    private void SetQuantifiableChild()
    {
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        return DynamicDataSource.GetFitsInILimiterArgs();
    }

    private static IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        return DynamicDataSource.GetFitsInIQuantifiableLimitModeArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityRoundingModeArgs()
    {
        return DynamicDataSource.GetGetQuantityRoundingModeArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityInvalidTypeCodeArgs()
    {
        return DynamicDataSource.GetGetQuantityInvalidTypeCodeArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityValidTypeCodeArgs()
    {
        return DynamicDataSource.GetGetQuantityValidTypeCodeArgs();
    }

    private static IEnumerable<object[]> GetIsExchangeableToArg()
    {
        return DynamicDataSource.GetIsExchangeableToArg();
    }

    private static IEnumerable<object[]> GetTryExchangeToArgs()
    {
        return DynamicDataSource.GetTryExchangeToArgs();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitCodeInvalidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitCodeInvalidArgs();
    }
    #endregion
    #endregion
}
