using static CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations.BaseMeasurement;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasureTests
{
    #region Tested in parent classes' tests

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
    private static DynamicDataSource DynamicDataSource;
    #endregion

    private BaseMeasureChild _baseMeasure;
    private IBaseMeasurement _baseMeasurement;
    private ILimiter _limiter;
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
        Fields.measureUnitCode = Fields.RandomParams.GetRandomConstantMeasureUnitCode();
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantityTypeCode = Fields.RandomParams.GetRandomQuantityTypeCode(Fields.rateComponentCode);
        Fields.quantity = Fields.RandomParams.GetRandomValueType(Fields.quantityTypeCode);
        Fields.defaultQuantity = (Convert.ToDecimal(Fields.quantity)
            * GetExchangeRate(Fields.measureUnit, nameof(Fields.measureUnit)))
            .Round(RoundingMode.DoublePrecision);
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
    #endregion

    #region Test methods
    #region int CompareTo
    #region override sealed IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        IQuantifiable other = null;
        const int expected = 1;

        // Act
        var actual = _baseMeasure.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnitCode = Fields.RandomParams.GetRandomConstantMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        IQuantifiable other = GetBaseMeasureChild(Fields.measureUnit, Fields.quantity);

        // Act
        void attempt() => _ = _baseMeasure.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomSameTypeValidMeasureUnit(Fields.measureUnit);
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity(Fields.quantityTypeCode, Fields.quantity);
        IQuantifiable other = GetBaseMeasureChild(Fields.measureUnit, Fields.quantity);
        int expected = _baseMeasure.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _baseMeasure.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region override sealed IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArg), DynamicDataSourceType.Method)]
    public void Equals_arg_IQuantifiable_returns_expected(bool expected, Enum measureUnit, ValueType quantity, IQuantifiable other)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, quantity);

        // Act
        var actual = _baseMeasure.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure?, IBaseMeasure?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_args_IBaseMeasure_IBaseMeasure_returns_expected(IBaseMeasure left, bool expected, IBaseMeasure right)
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
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
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
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        _limiter = GetLimiterBaseMeasureObject(Fields.measureUnit, Fields.quantity, Fields.limitMode.Value);
        bool? expected = Fields.defaultQuantity.FitsIn(_limiter.GetLimiterDefaultQuantity(), Fields.limitMode);

        // Act
        var actual = _baseMeasure.FitsIn(_limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region override sealed IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IQuantifiable_LimitMode_returns_expected()
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
    [DynamicData(nameof(GetFitsInIQuantifiableLimitModeArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArgs_IQuantifiable_LimitMode_returns_null(Enum measureUnit, LimitMode? limitMode, IQuantifiable other)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, Fields.quantity);

        // Act
        var actual = _baseMeasure.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetBaseMeasureChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity();
        IQuantifiable quantifiable = GetBaseMeasureChild(Fields.measureUnit, Fields.quantity);
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        bool? expected = Fields.defaultQuantity.FitsIn(quantifiable.GetDefaultQuantity(), Fields.limitMode);

        // Act
        var actual = _baseMeasure.FitsIn(quantifiable, Fields.limitMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
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
        IBaseMeasure expected = GetBaseMeasureChild(Fields.measureUnit, Fields.quantity, Fields.rateComponentCode, Fields.limitMode);

        // Act
        var actual = _baseMeasure.GetBaseMeasure(Fields.quantity);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IBaseMeasure.GetBaseMeasure(IBaseMeasurement, ValueType)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseBeasureNullCheckArgs), DynamicDataSourceType.Method)]
    public void GetBaseMeasure_nullArgs_IBaseMeasurement_ValueType_thorws_ArgumentNullException(string paramName, IBaseMeasurement baseMeasurement)
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
        _baseMeasurement = BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);
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
        IBaseMeasure expected = GetBaseMeasureChild(Fields.measureUnit, Fields.quantity, Fields.rateComponentCode, Fields.limitMode);
        _baseMeasurement = BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);

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

        IBaseMeasurement expected = BaseMeasurementChild.GetBaseMeasurementChild(Fields.measureUnit);

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

    #endregion
    #endregion

    // LimitMode? ILimitMode.GetLimitMode()
    // TypeCode? IQuantityTypeCode.GetQuantityTypeCode(object quantity)
    // RateComponentCode IRateComponentCode.GetRateComponentCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)

    #endregion

    #region Private methods
    private void SetBaseMeasureChild(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null, LimitMode? limitMode = null)
    {
        _baseMeasure = GetBaseMeasureChild(measureUnit, quantity, rateComponentCode, limitMode);
    }

    private void SetBaseMeasureChild()
    {
        SetBaseMeasureChild(Fields.measureUnit, Fields.quantity);
    }

    private void SetCompleteBaseMeasureChild()
    {
        SetBaseMeasureChild(Fields.measureUnit, Fields.quantity, Fields.rateComponentCode, Fields.limitMode);
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArg()
    {
        return DynamicDataSource.GetEqualsArg();
    }

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

    private static IEnumerable<object[]> GetBaseBeasureNullCheckArgs()
    {
        return DynamicDataSource.GetBaseBeasureNullCheckArgs();
    }

    //private static IEnumerable<object[]> GetEqualsArgs()
    //{
    //    return DynamicDataSource.GetEqualsArgs();
    //}

    //private static IEnumerable<object[]> GetFitsInArgs()
    //{
    //    return DynamicDataSource.GetFitsInArgs();
    //}

    //private static IEnumerable<object[]> GetInvalidQuantityTypeCodeArg()
    //{
    //    return DynamicDataSource.GetInvalidQuantityTypeCodeArg();
    //}

    //private static IEnumerable<object[]> GetValidQuantityTypeCodeArg()
    //{
    //    return DynamicDataSource.GetValidQuantityTypeCodeArg();
    //}
    #endregion
    #endregion
}
