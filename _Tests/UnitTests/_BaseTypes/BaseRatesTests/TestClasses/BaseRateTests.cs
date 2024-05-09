using CsabaDu.FooVaria.BaseTypes.BaseRates.Types;
using System.ComponentModel;

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

    // bool IEquatable<IBaseRate>.Equals(IBaseRate? other)
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

    //#region bool Equals
    //#region override BaseQuantifiable.Equals(object?)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void Equals_arg_object_returns_expected(string testCase, bool expected, object obj, Enum measureUnit, decimal defaultQuantity)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.Equals(obj);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region bool? FitsIn
    //#region virtual ILimitable.FitsIn(ILimiter?)
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
    //    SetBaseQuantifiableChild(measureUnit, Fields.defaultQuantity);

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

    //    Fields.limitMode = _randomParams.GetRandomLimitMode();
    //    decimal otherQuantity = _randomParams.GetRandomDecimal();
    //    Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
    //    ILimiter limiter = GetLimiterBaseQuantifiableObject(Fields.limitMode, Fields.measureUnit, otherQuantity);
    //    bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region decimal GetDefaultQuantity
    //#region abstract IDefaultQuantity.GetDefaultQuantity()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetDefaultQuantity_returns_expected()
    //{
    //    // Arrange
    //    decimal expected = Fields.defaultQuantity;

    //    SetBaseQuantifiableChild(null, expected);

    //    // Act
    //    var actual = _baseQuantifiable.GetDefaultQuantity();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region int GetHashCode
    //#region override BaseQuantifiable.GetHashCode()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetHashCode_returns_expected()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild();

    //    var expected = HashCode.Combine(Fields.measureUnitCode, Fields.defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.GetHashCode();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region void ValidateQuantity
    //#region virtual IBaseQuantifiable.ValidateQuantity(ValueType?, string)
    //[TestMethod, TestCategory("UnitTest")]
    //public void ValidateQuantity_nullArg_ValueType_arg_string_throws_ArgumentNullException()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild();

    //    Fields.paramName = _randomParams.GetRandomParamName();
    //    ValueType quantity = null;

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
    //    Assert.AreEqual(Fields.paramName, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetInvalidQuantityTypeCodeArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void ValidateQuantity_invalidArg_ValueType_arg_string_throws_ArgumentOutOfRangeException(string testCase, TypeCode typeCode) // negative!
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild();

    //    ValueType quantity = _randomParams.GetRandomValueType(typeCode);
    //    Fields.paramName = _randomParams.GetRandomParamName();

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, Fields.paramName);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
    //    Assert.AreEqual(Fields.paramName, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetValidQuantityTypeCodeArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    //public void ValidateQuantity_validArg_ValueType_arg_string_returns_expected(string testCase, TypeCode typeCode)
    //{
    //    // Arrange
    //    if (typeCode == TypeCode.UInt64 && Fields.defaultQuantity < 0)
    //    {
    //        Fields.defaultQuantity = _randomParams.GetRandomNotNegativeDecimal();
    //    }
    //    SetBaseQuantifiableChild();

    //    ValueType quantity = _randomParams.GetRandomValueType(typeCode);

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, null);

    //    // Assert
    //    Assert.IsTrue(DoesNotThrowException(attempt));
    //}
    //#endregion
    //#endregion
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
    #endregion
}
