using CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.Returns;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class QuantifiableTests
{
    #region Tested in parent classes' tests

    // Shape(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnitValue()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // IFactory ICommonBase.GetFactoryValue()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

    #endregion

    #region Private fields
    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion

    private QuantifiableChild _quantifiable;
    private QuantifiableChild _other;
    private ILimiter _limiter;
    private RandomParams _randomParams;
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _randomParams = Fields.RandomParams;
        
        Fields.SetMeasureUnit(_randomParams.GetRandomMeasureUnit());

        Fields.defaultQuantity = _randomParams.GetRandomDecimal();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        //Fields.paramName = null;
        _other = null;
        _limiter = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region int CompareTo
    #region IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
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

        Fields.measureUnitCode = _randomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = _randomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields);

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

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = _randomParams.GetRandomDecimal();
        IQuantifiable other = GetQuantifiableChild(Fields);
        int expected = _quantifiable.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _quantifiable.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_IQuantifiable_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, bool expected, IQuantifiable other)
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

        _limiter = null;

        // Act
        var actual = _quantifiable.FitsIn(_limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter)
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

        Fields.limitMode = _randomParams.GetRandomLimitMode();
        decimal otherQuantity = _randomParams.GetRandomDecimal();
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _limiter = GetLimiterQuantifiableObject(Fields.limitMode.Value, Fields.measureUnit, otherQuantity);
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(_limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.limitMode = null;

        // Act
        var actual = _quantifiable.FitsIn(null, Fields.limitMode);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIQuantifiableLimitModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArgs_IQuantifiable_LimitMode_returns_null(string testCase, Enum measureUnit, LimitMode? limitMode, IQuantifiable other)
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

        decimal otherQuantity = _randomParams.GetRandomDecimal();
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _other = GetQuantifiableChild(otherQuantity, Fields.measureUnit);
        Fields.limitMode = _randomParams.GetRandomLimitMode();
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(_other, Fields.limitMode);

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

        Fields.quantity = _quantifiable.GetBaseQuantity();
        decimal expected = (decimal)Fields.quantity.ToQuantity(TypeCode.Decimal);

        // Act
        var actual = _quantifiable.GetDecimalQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    //#region MeasureUnitCode GetMeasureUnitCode
    //#region override sealed IMeasureUnitCode.GetMeasureUnitCode()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetMeasureUnitCode_returns_expected()
    //{
    //    // Arrange
    //    SetQuantifiableChild();
    //    MeasureUnitCode expected = Fields.measureUnitCode;

    //    // Act
    //    var actual = _quantifiable.GetMeasureUnitCode();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

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

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityRoundingModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetQuantity_validArg_RoundingMode_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, object expected, RoundingMode roundingMode)
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
    [DynamicData(nameof(GetGetQuantityInvalidTypeCodeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetQuantity_invalidArg_TypeCode_throws_InvalidEnumArgumentException(string testCase, TypeCode typeCode)
    {
        // Arrange
        decimal quantity = typeCode == TypeCode.UInt64 ? _randomParams.GetRandomNegativeDecimal() : Fields.defaultQuantity;
        SetQuantifiableChild(quantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.GetQuantity(typeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.quantityTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityValidTypeCodeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetQuantity_validArg_TypeCode_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, object expected, TypeCode quantityTypeCode)
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
    [DynamicData(nameof(GetIsExchangeableToArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void IsExchangeableTo_arg_Enum_returns_expected(string testCase, bool expected, Enum measureUnit, Enum context)
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
        _other = null;

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(_other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnitCode = _randomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = _randomParams.GetRandomDecimal();
        _other = GetQuantifiableChild(Fields);

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(_other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_invalidArg_IQuantifiable_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = 0;
        _other = GetQuantifiableChild(Fields);

        // Act
        void attempt() => _ = _quantifiable.ProportionalTo(_other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.defaultQuantity = _randomParams.GetRandomDecimal();
        _other = GetQuantifiableChild(Fields);
        decimal expected = Math.Abs(_quantifiable.GetDefaultQuantity() / _other.GetDefaultQuantity());

        // Act
        var actual = _quantifiable.ProportionalTo(_other);

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
        Fields.roundingMode = _randomParams.GetRandomRoundingMode();

        Fields.defaultQuantity = Fields.defaultQuantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetQuantifiableChild(Fields);

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
    [DynamicData(nameof(GetTryExchangeToArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void TryExchangeTo_arg_returns_success_out_expected(string testCase, Enum measureUnit, Enum otherMeasureUnit, IQuantifiable expected)
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
    [DynamicData(nameof(GetValidateMeasureUnitCodeInvalidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateMeasureUnitCode_invalidArg_MeasureUnitCode_arg_string_throws_InvalidEnumArgumentException(string testCase, Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        Fields.paramName = _randomParams.GetRandomParamName();

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
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _quantifiable.ValidateMeasureUnitCode(Fields.measureUnitCode, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
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

    private void SetQuantifiableChild(decimal defaultQuantity, Enum measureUnit = null, IQuantifiableFactory factory = null)
    {
        _quantifiable = GetQuantifiableChild(defaultQuantity, measureUnit, factory);
    }

    private void SetQuantifiableChild()
    {
        _quantifiable = GetQuantifiableChild(Fields);
    }
    #endregion
}
