using CsabaDu.FooVaria.Tests.TestHelpers.HelperMethods;

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

    #region override sealed ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_true()
    {
        // Arrange
        SetBaseRateChild();

        _limiter = null;

        // Act
        var actual = _baseRate.FitsIn(_limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void FitsIn_invalidArg_ILimiter_returns_null(string testCase, Enum measureUnit, ILimiter limiter, MeasureUnitCode denominatorCode)
    {
        // Arrange
        SetBaseRateChild(measureUnit, _fields.defaultQuantity, denominatorCode);

        // Act
        var actual = _baseRate.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        decimal defaultQuantity = _baseRate.GetDefaultQuantity();
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _fields.defaultQuantity = _randomParams.GetRandomDecimal();
        _fields.limitMode = _randomParams.GetRandomLimitMode();
        _limiter = GetLimiterBaseRateObject(_fields);

        bool? expected = defaultQuantity.FitsIn(_fields.defaultQuantity, _fields.limitMode);

        // Act
        var actual = _baseRate.FitsIn(_limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IBaseRate GetBaseRate
    #region IBaseRate.GetBaseRate(IQuantifiable numerator, Enum denominator)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseRateEnumNullArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseRate_nullArgs_IQuantifiable_Enum_throws_ArgumentNullException(string testCase, IQuantifiable numerator, Enum denominator, string paramName)
    {
        // Arrange
        SetCompleteBaseRateChild();

        // Act
        void attempt() => _ = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseRateEnumInvalidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseRate_arg_IQuantifiable_invalidArg_Enum_throws_InvalidEnumArgumentException(string testCase, IQuantifiable numerator, Enum denominator)
    {
        // Arrange
        SetCompleteBaseRateChild();

        // Act
        void attempt() => _ = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseRateEnumValidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseRate_validArgs_IQuantifiable_Enum_returns_expected(string testCase, IQuantifiable numerator, Enum denominator)
    {
        // Arrange
        SetCompleteBaseRateChild();

        MeasureUnitCode denominatorCode = Measurable.GetMeasureUnitElements(denominator, nameof(denominator)).MeasureUnitCode;
        IBaseRate expected = GetBaseRateChild(numerator, denominatorCode);

        // Act
        var actual = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IBaseRate.GetBaseRate(IQuantifiable numerator, IMeasurable denominator)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseRateIMeasurableNullArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseRate_nullArgs_IQuantifiable_IMeasurable_throws_ArgumentNullException(string testCase, IQuantifiable numerator, string paramName, IMeasurable denominator)
    {
        // Arrange
        SetCompleteBaseRateChild();

        // Act
        void attempt() => _ = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseRate_validArgs_IQuantifiable_IMeasurable_returns_expected()
    {
        // Arrange
        SetCompleteBaseRateChild();

        IQuantifiable numerator = GetQuantifiableChild(_fields);
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit();
        IMeasurable denominator = GetMeasurableChild(_fields);

        IBaseRate expected = GetBaseRateChild(numerator, _fields.GetMeasureUnitCode());

        // Act
        var actual = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IBaseRate.GetBaseRate(IQuantifiable numerator, IQuantifiable denominator)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetBaseRateIQuantifiableNullArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetBaseRate_nullArgs_IQuantifiable_IQuantifiable_throws_ArgumentNullException(string testCase, IQuantifiable numerator, string paramName, IQuantifiable denominator)
    {
        // Arrange
        SetCompleteBaseRateChild();

        // Act
        void attempt() => _ = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseRate_validArgs_IQuantifiable_IQuantifiable_returns_expected()
    {
        // Arrange
        SetCompleteBaseRateChild();

        IQuantifiable numerator = GetQuantifiableChild(_fields);
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit();
        IQuantifiable denominator = GetQuantifiableChild(_fields);

        IBaseRate expected = GetBaseRateChild(numerator, _fields.GetMeasureUnitCode());

        // Act
        var actual = _baseRate.GetBaseRate(numerator, denominator);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region MeasureUnitCode GetDenominatorCode
    #region IDenominate.GetDenominatorCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDenominatorCode_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        MeasureUnitCode expected = _fields.denominatorCode;

        // Act
        var actual = _baseRate.GetDenominatorCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region BaseRate.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        int expected = HashCode.Combine(_fields.measureUnitCode, _fields.defaultQuantity, _fields.denominatorCode);

        // Act
        var actual = _baseRate.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region LimitMode? GetLimitMode
    #region virtual ILimitMode.GetLimitMode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetLimitMode_returns_null()
    {
        // Arrange
        SetBaseRateChild();

        // Act
        var actual = _baseRate.GetLimitMode();

        // Assert
        Assert.IsNull(actual);
    }
    #endregion
    #endregion

    #region MeasureUnitCode GetMeasureUnitCode
    #region IBaseRate.GetMeasureUnitCode(RateComponentCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetMeasureUnitCodeInvalidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetMeasureUnitCode_invalidArg_RateComponentCode_throws_InvalidEnumArgumentException(string testCase, RateComponentCode rateComponentCode)
    {
        // Arrange
        SetBaseRateChild();

        // Act
        void attempt() => _ = _baseRate.GetMeasureUnitCode(rateComponentCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.rateComponentCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetMeasureUnitCodeValidArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetMeasureUnitCode_validArg_RateComponentCode_returns_expected(string testCase, Enum measureUnit, MeasureUnitCode denominatorCode, MeasureUnitCode expected, RateComponentCode rateComponentCode)
    {
        // Arrange
        SetBaseRateChild(measureUnit, _fields.defaultQuantity, denominatorCode);

        // Act
        var actual = _baseRate.GetMeasureUnitCode(rateComponentCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IEnumerable<MeasureUnitCode> GetMeasureUnitCodes
    #region IMeasureUnitCodes.GetMeasureUnitCodes()
    [TestMethod, TestCategory("UnitTest")]
    public void GetMeasureUnitCodes_returns_expected()
    {
        // Arrange
        SetBaseRateChild();

        IEnumerable<MeasureUnitCode> expected = [_fields.measureUnitCode, _fields.denominatorCode];

        // Act
        var actual = _baseRate.GetMeasureUnitCodes();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion



    // MeasureUnitCode IBaseRate.GetNumeratorCode()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // object? IValidRateComponent.GetRateComponent(RateComponentCode rateComponentCode)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode denominatorCode)
    // bool IExchangeable<IBaseRate>.IsExchangeableTo(IBaseRate? context)
    // bool IValidRateComponent.IsValidRateComponent(object? rateComponent, RateComponentCode rateComponentCode)
    // decimal IProportional<IBaseRate>.ProportionalTo(IBaseRate? other)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IBaseQuantifiable? baseQuantifiable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode denominatorCode, string paramName)
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

    private static IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        return DynamicDataSource.GetFitsInILimiterArgs();
    }

    private static IEnumerable<object[]> GetGetBaseRateEnumNullArgs()
    {
        return DynamicDataSource.GetGetBaseRateEnumNullArgs();
    }

    private static IEnumerable<object[]> GetGetBaseRateEnumInvalidArgs()
    {
        return DynamicDataSource.GetGetBaseRateEnumInvalidArgs();
    }

    private static IEnumerable<object[]> GetGetBaseRateEnumValidArgs()
    {
        return DynamicDataSource.GetGetBaseRateEnumValidArgs();
    }

    private static IEnumerable<object[]> GetGetBaseRateIMeasurableNullArgs()
    {
        return DynamicDataSource.GetGetBaseRateIMeasurableNullArgs();
    }

    private static IEnumerable<object[]> GetGetBaseRateIQuantifiableNullArgs()
    {
        return DynamicDataSource.GetGetBaseRateIQuantifiableNullArgs();
    }

    private static IEnumerable<object[]> GetGetMeasureUnitCodeInvalidArgs()
    {
        return DynamicDataSource.GetGetMeasureUnitCodeInvalidArgs();
    }

    private static IEnumerable<object[]> GetGetMeasureUnitCodeValidArgs()
    {
        return DynamicDataSource.GetGetMeasureUnitCodeValidArgs();
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

    private void SetCompleteBaseRateChild()
    {
        _baseRate = GetBaseRateChild(_fields, new BaseRateFactoryObject());
    }

    private void SetBaseRateChild(IQuantifiable quantifiable, MeasureUnitCode denominatorCode, IBaseRateFactory factory = null)
    {
        _baseRate = GetBaseRateChild(quantifiable, denominatorCode, factory);
    }
    #endregion
}
