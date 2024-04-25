namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasureTests
{
    #region Tested in parent classes' tests

    // BaseMeasure(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
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
        Fields.SetRandomMeasureUnit(Fields.RandomParams.GetRandomConstantMeasureUnitCode());
        Fields.SetRandomQuantity(Fields.RandomParams.GetRandomQuantityTypeCode());

        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();
        Fields.limitMode = Fields.RandomParams.GetRandomNullableLimitMode();
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
    //#region int CompareTo
    //#region override sealed IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
    //[TestMethod, TestCategory("UnitTest")]
    //public void CompareTo_nullArg_IQuantifiable_returns_expected()
    //{
    //    // Arrange
    //    SetBaseMeasureChild();

    //    IQuantifiable other = null;
    //    const int expected = 1;

    //    // Act
    //    var actual = _baseMeasure.CompareTo(other);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void CompareTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    //{
    //    // Arrange
    //    SetBaseMeasureChild();

    //    Fields.measureUnitCode = Fields.RandomParams.GetRandomConstantMeasureUnitCode(Fields.measureUnitCode);
    //    Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
    //    IQuantifiable other = GetBaseMeasureChild();

    //    // Act
    //    void attempt() => _ = _baseMeasure.CompareTo(other);

    //    // Assert
    //    var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
    //    Assert.AreEqual(ParamNames.other, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void CompareTo_validArg_IQuantifiable_returns_expected()
    //{
    //    // Arrange
    //    SetBaseMeasureChild();

    //    Fields.measureUnit = Fields.RandomParams.GetRandomSameTypeValidMeasureUnit(Fields.measureUnit);
    //    Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity(Fields.quantityTypeCode, Fields.quantity);
    //    IQuantifiable other = GetBaseMeasureChild();
    //    int expected = _baseMeasure.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

    //    // Act
    //    var actual = _baseMeasure.CompareTo(other);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    #region bool Equals
    //#region override sealed IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetEqualsArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void Equals_arg_IQuantifiable_returns_expected(bool expected, Enum measureUnit, ValueType quantity, IQuantifiable other)
    //{
    //    // Arrange
    //    SetBaseMeasureChild(measureUnit, quantity);

    //    // Act
    //    var actual = _baseMeasure.Equals(other);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion

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

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.decimalQuantity = Fields.RandomParams.GetRandomDecimal();
        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
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
    //    Assert.IsTrue(actual);
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

    //    Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
    //    Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
    //    IQuantifiable quantifiable = GetBaseMeasureChild();
    //    Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
    //    bool? expected = Fields.defaultQuantity.FitsIn(quantifiable.GetDefaultQuantity(), Fields.limitMode);

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

        Fields.quantityTypeCode = Fields.RandomParams.GetRandomInvalidQuantityTypeCode();
        Fields.quantity = Fields.RandomParams.GetRandomValueType(Fields.quantityTypeCode);

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

        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
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

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit();
        _baseMeasurement = TestHelpers.Fakes.BaseTypes.BaseMeasurements.BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);
        Fields.quantityTypeCode = Fields.RandomParams.GetRandomInvalidQuantityTypeCode();
        Fields.quantity = Fields.RandomParams.GetRandomValueType(Fields.quantityTypeCode);

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

        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
        IBaseMeasure expected = GetCompleteBaseMeasureChild(Fields);
        _baseMeasurement = TestHelpers.Fakes.BaseTypes.BaseMeasurements.BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);

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

        IBaseMeasurement expected = TestHelpers.Fakes.BaseTypes.BaseMeasurements.BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);

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

        Fields.measureUnit = Fields.RandomParams.GetRandomConstantMeasureUnit();
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
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

        Fields.roundingMode = Fields.RandomParams.GetRandomRoundingMode();
        Fields.quantity = (ValueType)Fields.quantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetCompleteBaseMeasureChild(Fields);

        // Act
        var actual = _baseMeasure.Round(Fields.roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateExchangeRate
    #region IExchangeRate.ValidateExchangeRate(decimal, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateExchangeRate_invalidArg_decimal_arg_string_throws_ArgumentOutOfRangeException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.decimalQuantity = GetExchangeRate(Fields.measureUnit, Fields.paramName);
        Fields.decimalQuantity = Fields.RandomParams.GetRandomDecimal(Fields.decimalQuantity);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

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
