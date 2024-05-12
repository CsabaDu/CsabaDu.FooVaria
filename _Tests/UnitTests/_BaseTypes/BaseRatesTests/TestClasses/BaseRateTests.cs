namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseRatesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseRateTests
{
    #region Tested in parent classes' tests

    // BaseRate(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Type IQuantity.GetBaseQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // decimal IQuantity<decimal>.GetQuantity()

    #endregion

    #region Private fields
    private BaseRateChild _baseRate;
    private IBaseRate _other;
    private ILimiter _limiter;
    private MeasureUnitCode _denominatorCode;
    private RandomParams _randomParams;
    private DataFields _fields;

    #region Static fields
    private static readonly DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _fields = new();
        _randomParams = _fields.RandomParams;
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        _denominatorCode = _randomParams.GetRandomMeasureUnitCode();

        _fields.SetMeasureUnit(_randomParams.GetRandomMeasureUnit());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _baseRate = null;
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
    #region IComparable<IBaseRate>.CompareTo(IBaseRate?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IBaseRate_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        IBaseRate other = null;
        const int expected = 1;

        // Act
        var actual = _baseRate.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IBaseRate_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseRateChild();

        _fields.measureUnit = _randomParams.GetRandomSpreadMeasureUnit(_fields.measureUnitCode);
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        IBaseRate other = GetBaseRateChild(_fields);

        // Act
        void attempt() => _ = _baseRate.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        IBaseRate other = GetBaseRateChild(_fields);
        int expected = _baseRate.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _baseRate.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region override sealed BaseRate.Equals(object? obj)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsObjectArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_object_returns_expected(string testCase, bool expected, object obj, Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode)
    {
        // Arrange
        SetBaseRateChild(measureUnit, defaultQuantity, denominatorCode);

        // Act
        var actual = _baseRate.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEquatable<IBaseRate>.Equals(IBaseRate?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsIBaseRateArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_IBaseRate_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, bool expected, MeasureUnitCode denominatorCode, IBaseRate other)
    {
        // Arrange
        SetBaseRateChild(measureUnit, defaultQuantity, denominatorCode);

        // Act
        var actual = _baseRate.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region IFit<IBaseRate>.FitsIn(IBaseRate?, LimitMode?)
    #region override sealed IFit<IBaseRate>.FitsIn(IBaseRate?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IBaseRate_LimitMode_returns_true()
    {
        // Arrange
        SetBaseRateChild();

        _other = null;
        _fields.limitMode = null;

        // Act
        var actual = _baseRate.FitsIn(_other, _fields.limitMode);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIBaseRateLimitModeArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArgs_IBaseRate_LimitMode_returns_null(string testCase, Enum measureUnit, LimitMode? limitMode, IBaseRate other, MeasureUnitCode denominatorCode)
    {
        // Arrange
        SetBaseRateChild(measureUnit, _fields.defaultQuantity, denominatorCode);

        // Act
        var actual = _baseRate.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IBaseRate_LimitMode_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        IBaseRate baseRate = GetBaseRateChild(_fields);
        _fields.limitMode = _randomParams.GetRandomLimitMode();
        bool? expected = _baseRate.GetDefaultQuantity().FitsIn(baseRate.GetDefaultQuantity(), _fields.limitMode);

        // Act
        var actual = _baseRate.FitsIn(baseRate, _fields.limitMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ILimitable.FitsIn(ILimiter?)
    #region virtual ILimitable.FitsIn(ILimiter?)
    //[TestMethod, TestCategory("UnitTest")]
    //public void FitsIn_nullArg_ILimiter_returns_expected()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild();

    //    ILimiter limiter = null;

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.IsTrue(actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetFitsInArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, _fields.defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.IsNull(actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void FitsIn_validArg_ILimiter_returns_expected()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild();

    //    _fields.limitMode = _randomParams.GetRandomLimitMode();
    //    decimal otherQuantity = _randomParams.GetRandomDecimal();
    //    _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
    //    ILimiter limiter = GetLimiterBaseQuantifiableObject(_fields.limitMode, _fields.measureUnit, otherQuantity);
    //    bool? expected = _fields.defaultQuantity.FitsIn(otherQuantity, _fields.limitMode);

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    #endregion
    #endregion
    #endregion
    #endregion

    // int BaseRate.GetHashCode()
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, Enum denominator)
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
    // IBaseRate IBaseRate.GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    // MeasureUnitCode IDenominate.GetDenominatorCode()
    // MeasureUnitCode IBaseRate.GetMeasureUnitCode(RateComponentCode rateComponentCode)
    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()
    // LimitMode? ILimitMode.GetLimitMode()
    // MeasureUnitCode IBaseRate.GetNumeratorCode()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // object? IValidRateComponent.GetRateComponent(RateComponentCode rateComponentCode)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<IBaseRate>.IsExchangeableTo(IBaseRate? context)
    // bool IValidRateComponent.IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    // decimal IProportional<IBaseRate>.ProportionalTo(IBaseRate? other)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IBaseQuantifiable? baseQuantifiable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(Type? quantity, string paramName)
    // void IBaseRate.ValidateRateComponentCode(RateComponentCode rateComponentCode, string paramName)

    #endregion

    #region Private methods
    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsObjectArgs()
    {
        return DynamicDataSource.GetEqualsObjectArgs();
    }

    private static IEnumerable<object[]> GetEqualsIBaseRateArgs()
    {
        return DynamicDataSource.GetEqualsIBaseRateArgs();
    }

    private static IEnumerable<object[]> GetFitsInIBaseRateLimitModeArgs()
    {
        return DynamicDataSource.GetFitsInIBaseRateLimitModeArgs();
    }
    #endregion

    private void SetBaseRateChild(Enum measureUnit, decimal defaultQuantity, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        _baseRate = GetBaseRateChild(measureUnit, defaultQuantity, denominatorCode, factory);
    }

    private void SetBaseRateChild(IBaseRateFactory factory = null)
    {
        _baseRate = GetBaseRateChild(_fields, factory);
    }

    private void SetBaseRateChild(IQuantifiable quantifiable, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        _baseRate = GetBaseRateChild(quantifiable, denominatorCode, factory);
    }
    #endregion
}
