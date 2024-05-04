namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasureTests
{
    #region Tested in parent classes' tests

    // BaseMeasure(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // ValueType IQuantity.GetBaseQuantityValue()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactoryValue()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal quantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? right)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Private fields
    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion

    private BaseMeasureChild _baseMeasure;
    private IBaseMeasurement _baseMeasurement;
    private ILimiter _limiter;
    private RandomParams _randomParams;
    #endregion

    #region Initialize
    //[ClassInitialize]
    //public static void ClassInitialize(TestContext context)
    //{
    //    DynamicDataSource = new();
    //}

    [TestInitialize]
    public void TestInitialize()
    {
        _randomParams = Fields.RandomParams;

        Fields.SetRandomMeasureUnit(_randomParams.GetRandomConstantMeasureUnitCode());
        Fields.SetRandomQuantity(_randomParams.GetRandomQuantityTypeCode());

        Fields.rateComponentCode = _randomParams.GetRandomRateComponentCode();
        Fields.limitMode = _randomParams.GetRandomNullableLimitMode();
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        _baseMeasurement = null;
        _limiter = null;

        RestoreConstantExchangeRates();
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region bool Equals
    #region IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure?, IBaseMeasure?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_args_IBaseMeasure_IBaseMeasure_returns_expected(string testCase, IBaseMeasure left, bool expected, IBaseMeasure right)
    {
        // Arrange
        SetBaseMeasureChild();

        // Act
        var actual = _baseMeasure.Equals(left, right);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region override sealed ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        _limiter = null;

        // Act
        var actual = _baseMeasure.FitsIn(_limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, Fields.quantity);

        // Act
        var actual = _baseMeasure.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.decimalQuantity = _randomParams.GetRandomDecimal();
        Fields.rateComponentCode = _randomParams.GetRandomRateComponentCode();
        Fields.limitMode = _randomParams.GetRandomLimitMode();
        _limiter = TestHelpers.Fakes.BaseTypes.Quantifiables.LimiterQuantifiableObject.GetLimiterQuantifiableObject(Fields.limitMode.Value, Fields.measureUnit, Fields.decimalQuantity);
        Fields.decimalQuantity = (_limiter as IQuantifiable).GetDefaultQuantity();
        bool? expected = Fields.defaultQuantity.FitsIn(Fields.decimalQuantity, Fields.limitMode);

        // Act
        var actual = _baseMeasure.FitsIn(_limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    //#region override sealed IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)
    //[TestMethod, TestCategory("UnitTest")]
    //public void FitsIn_nullArgs_IQuantifiable_LimitMode_returns_expected()
    //{
    //    // Arrange
    //    SetBaseMeasureChild();

    //    _limiter = null;

    //    // Act
    //    var actual = _baseMeasure.FitsIn(_limiter);

    //    // Assert
    //    Assert.Quantifiable(actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetFitsInIQuantifiableLimitModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void FitsIn_invalidArgs_IQuantifiable_LimitMode_returns_null(Enum measureUnit, LimitMode? limitMode, IQuantifiable other)
    //{
    //    // Arrange
    //    SetBaseMeasureChild(measureUnit, Fields.quantity);

    //    // Act
    //    var actual = _baseMeasure.FitsIn(other, limitMode);

    //    // Assert
    //    Assert.IsNull(actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void FitsIn_validArgs_IQuantifiable_LimitMode_returns_expected()
    //{
    //    // Arrange
    //    SetBaseMeasureChild();

    //    Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
    //    Fields.quantity = (ValueType)_randomParams.GetRandomQuantity();
    //    IQuantifiable quantifiable = GetBaseMeasureChild();
    //    Fields.limitMode = _randomParams.GetRandomLimitMode();
    //    bool? expected = Fields.defaultQuantity.FitsIn(quantifiable.GetDefaultQuantityValue(), Fields.limitMode);

    //    // Act
    //    var actual = _baseMeasure.FitsIn(quantifiable, Fields.limitMode);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    #endregion

    #region IBaseMeasure GetBaseMeasure
    #region IBaseMeasure.GetBaseMeasure(ValueType)
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_nullArg_ValueType_thorws_ArgumentNullException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.quantity = null;

        // Act
        void attempt() => _ = _baseMeasure.GetBaseMeasure(Fields.quantity);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_invalidArg_ValueType_thorws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.quantityTypeCode = _randomParams.GetRandomInvalidQuantityTypeCode();
        Fields.quantity = _randomParams.GetRandomValueType(Fields.quantityTypeCode);

        // Act
        void attempt() => _ = _baseMeasure.GetBaseMeasure(Fields.quantity);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_validArg_ValueType_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        Fields.quantity = (ValueType)_randomParams.GetRandomQuantity();
        IBaseMeasure expected = GetCompleteBaseMeasureChild(Fields);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(Fields.quantity);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IBaseMeasure.GetBaseMeasure(IBaseMeasurement, ValueType)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseMeasureNullCheckArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseMeasure_nullArgs_IBaseMeasurement_ValueType_thorws_ArgumentNullException(string testCase, string paramName, IBaseMeasurement baseMeasurement)
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.quantity = null;

        // Act
        void attempt() => _ = _baseMeasure.GetBaseMeasure(baseMeasurement, Fields.quantity);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_arg_IBaseMeasurement_invalidArg_ValueType_thorws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit();
        _baseMeasurement = GetBaseMeasurementChild(Fields.measureUnit);
        Fields.quantityTypeCode = _randomParams.GetRandomInvalidQuantityTypeCode();
        Fields.quantity = _randomParams.GetRandomValueType(Fields.quantityTypeCode);

        // Act
        void attempt() => _ = _baseMeasure.GetBaseMeasure(Fields.quantity);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(ParamNames.quantity, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasure_validArgs_IBaseMeasurement_ValueType_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        Fields.quantity = (ValueType)_randomParams.GetRandomQuantity();
        IBaseMeasure expected = GetCompleteBaseMeasureChild(Fields);
        _baseMeasurement = GetBaseMeasurementChild(Fields.measureUnit);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(_baseMeasurement, Fields.quantity);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IBaseMeasurement GetBaseMeasurement
    #region abstract IBaseMeasure.GetBaseMeasurement()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasurement_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        IBaseMeasurement expected = GetBaseMeasurementChild(Fields.measureUnit);

        // Act
        var actual = _baseMeasure.GetBaseMeasurement();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IBaseMeasurementFactory GetBaseMeasurementFactory
    #region abstract IBaseMeasure.GetBaseMeasurementFactory()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasurementFactory_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        // Act
        var actual = _baseMeasure.GetBaseMeasurementFactory();

        // Assert
        Assert.IsInstanceOfType<IBaseMeasurementFactory>(actual);
    }
    #endregion
    #endregion

    #region Enum GetBaseMeasureUnit
    #region override sealed IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        Enum expected = Fields.measureUnit;

        // Act
        var actual = _baseMeasure.GetBaseMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal GetDefaultQuantity
    #region override sealed IDefaultQuantity.GetDefaultQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDefaultQuantity_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        decimal expected = Fields.defaultQuantity;

        // Act
        var actual = _baseMeasure.GetDefaultQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal GetExchangeRate
    #region IExchangeRate.GetExchangeRate()
    [TestMethod, TestCategory("UnitTest")]
    public void GetExchangeRate_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        decimal expected = ExchangeRateCollection[Fields.measureUnit];

        // Act
        var actual = _baseMeasure.GetExchangeRate();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region IEqualityComparer<IBaseMeasure>.GetHashCode(IBaseMeasure)
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_arg_IBaseMeasure_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnit = _randomParams.GetRandomConstantMeasureUnit();
        Fields.quantity = (ValueType)_randomParams.GetRandomQuantity();
        IBaseMeasure baseMeasure = GetCompleteBaseMeasureChild(Fields);
        int expected = HashCode.Combine(Fields.rateComponentCode, Fields.limitMode, baseMeasure.GetHashCode());

        // Act
        var actual = _baseMeasure.GetHashCode(baseMeasure);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region LimitMode? GetLimitMode
    #region abstract ILimitMode.GetLimitMode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetLimitMode_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();
        LimitMode? expected = Fields.limitMode;

        // Act
        var actual = _baseMeasure.GetLimitMode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TypeCode GetQuantityTypeCode
    #region IQuantityTypeCode.GetQuantityTypeCode(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityTypeCodeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetQuantityTypeCode_arg_object_returns_expected(string testCase, TypeCode expected, object quantity)
    {
        // Arrange
        SetBaseMeasureChild();

        // Act
        var actual = _baseMeasure.GetQuantityTypeCode(quantity);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region RateComponentCode IRateComponentCode
    #region IRateComponentCode.GetRateComponentCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetRateComponentCode_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        RateComponentCode expected = Fields.rateComponentCode;

        // Act
        var actual = _baseMeasure.GetRateComponentCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region override sealed IExchangeable<Enum>.IsExchangeableTo(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void IsExchangeableTo_arg_Enum_returns_expected(string testCase, bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, Fields.quantity);

        // Act
        var actual = _baseMeasure.IsExchangeableTo(context);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable Round
    #region override sealed IRound<IQuantifiable>.Round(RoundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void Round_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.roundingMode = SampleParams.NotDefinedRoundingMode;

        // Act
        void attempt() => _ = _baseMeasure.Round(Fields.roundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Round_validArg_RoundingMode_returns_expected()
    {
        // Arrange
        SetCompleteBaseMeasureChild();

        Fields.roundingMode = _randomParams.GetRandomRoundingMode();
        Fields.quantity = (ValueType)Fields.quantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetCompleteBaseMeasureChild(Fields);

        // Act
        var actual = _baseMeasure.Round(Fields.roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region override sealed IDefaultMeasureUnit.ValidateMeasureUnit(Enum?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetBaseMeasureChild();
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _baseMeasure.ValidateMeasureUnit(null, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasureChild();
        Fields.measureUnitCode = _randomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Enum context = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);

        // Act
        void attempt() => _baseMeasure.ValidateMeasureUnit(context, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitValidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(string testCase, Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, Fields.quantity);

        // Act
        void attempt() => _baseMeasure.ValidateMeasureUnit(context, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion

    #region void ValidateExchangeRate
    #region IExchangeRate.ValidateExchangeRate(decimal, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateExchangeRate_invalidArg_decimal_arg_string_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.decimalQuantity = GetExchangeRate(Fields.measureUnit, Fields.paramName);
        Fields.decimalQuantity = _randomParams.GetRandomDecimal(Fields.decimalQuantity);
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _baseMeasure.ValidateExchangeRate(Fields.decimalQuantity, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }


    [TestMethod, TestCategory("UnitTest")]
    public void ValidateExchangeRate_validArg_decimal_arg_returns()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.decimalQuantity = GetExchangeRate(Fields.measureUnit, Fields.paramName);

        // Act
        void attempt() => _baseMeasure.ValidateExchangeRate(Fields.decimalQuantity, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion
    #endregion

    #region Private methods
    private void SetBaseMeasureChild(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null, LimitMode? limitMode = null)
    {
        _baseMeasure = GetBaseMeasureChild(measureUnit, quantity, rateComponentCode, limitMode);
    }
    private void SetBaseMeasureChild()
    {
        _baseMeasure = GetBaseMeasureChild(Fields);
    }

    private void SetCompleteBaseMeasureChild()
    {
        _baseMeasure = GetCompleteBaseMeasureChild(Fields);
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitValidArgs();
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

    private static IEnumerable<object[]> GetGetBaseMeasureNullCheckArgs()
    {
        return DynamicDataSource.GetGetBaseMeasureNullCheckArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityTypeCodeArgs()
    {
        return DynamicDataSource.GetGetQuantityTypeCodeArgs();
    }

    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        return DynamicDataSource.GetIsExchangeableToArgs();
    }
    #endregion
    #endregion
}
