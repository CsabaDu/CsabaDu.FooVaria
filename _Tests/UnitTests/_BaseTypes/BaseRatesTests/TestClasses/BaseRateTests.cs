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
    #region IEquatable<IBaseRate>.Equals(IBaseRate?)
    [TestMethod, TestCategory("UnitTest"), Ignore]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_IQuantifiable_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, bool expected, IBaseRate other, MeasureUnitCode denominatorCode)
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


    // bool? IFit<IBaseRate>.FitsIn(IBaseRate? other, LimitMode? limitMode)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
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

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }
    #endregion
    #endregion
}
