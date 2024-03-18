namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class QuantifiableTests
{
    #region Test methods

    #region Tested in parent classes' tests

    // bool? ILimitable.FitsIn(ILimiter limiter)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

    #endregion

    //  int IComparable<IQuantifiable>.CompareTo(IQuantifiable other)
    //  bool IEquatable<IQuantifiable>.Equals(IQuantifiable other)
    //  IQuantifiable IExchange<IQuantifiable, Enum>.ExchangeTo(Enum context)
    //  bool? IFit<IQuantifiable>.FitsIn(IQuantifiable other, LimitMode? limitMode)
    //  ValueType IQuantity.GetBaseQuantity()
    //  decimal IDecimalQuantity.GetDecimalQuantity()
    //  MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    //  IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    //  object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    //  object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    //  bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    //  decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable other)
    //  IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    //  bool IExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable exchanged)
    //  void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)

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
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
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
    private QuantifiableChild _quantifiable;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    //#region Test methods

    //#region Tested in parent classes' tests

    //// Enum IMeasureUnit.GetBaseMeasureUnit()
    //// Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    //// IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    //// IFactory ICommonBase.GetFactory()
    //// MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    //// Type IMeasureUnit.GetMeasureUnitType()
    //// TypeCode IQuantityType.GetQuantityTypeCode()
    //// bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    //// void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    //// void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    //// void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    //#endregion

    //#region bool Equals
    //#region BaseQuantifiable.Equals(object?)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetEqualsArgsArrayList), DynamicDataSourceType.Method)]
    //public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit, decimal defaultQuantity)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.Equals(obj);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region bool? FitsIn
    //#region ILimitable.FitsIn(ILimiter?)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetFitsInArgsArrayList), DynamicDataSourceType.Method)]
    //public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.IsNull(actual);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //public void FitsIn_validArg_ILimiter_returns_expected()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);
    //    limitMode = Fields.RandomParams.GetRandomLimitMode();
    //    decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
    //    LimiterBaseQuantifiableOblect limiter = new(RootObject, null)
    //    {
    //        Return = new()
    //        {
    //            GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(measureUnitCode),
    //            GetDefaultQuantity = otherQuantity,
    //        },
    //        LimitMode = limitMode,
    //    };
    //    bool? expected = defaultQuantity.FitsIn(otherQuantity, limitMode);

    //    // Act
    //    var actual = _baseQuantifiable.FitsIn(limiter);

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion

    //#endregion

    //#region decimal GetDefaultQuantity
    //#region IDefaultQuantity.GetDefaultQuantity()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetDefaultQuantity_returns_expected()
    //{
    //    // Arrange
    //    decimal expected = defaultQuantity;
    //    SetBaseQuantifiableChild(null, null, expected);

    //    // Act
    //    var actual = _baseQuantifiable.GetDefaultQuantity();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region int GetHashCode
    //#region Measurable.GetHashCode()
    //[TestMethod, TestCategory("UnitTest")]
    //public void GetHashCode_returns_expected()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);
    //    var expected = HashCode.Combine(measureUnitCode, defaultQuantity);

    //    // Act
    //    var actual = _baseQuantifiable.GetHashCode();

    //    // Assert
    //    Assert.AreEqual(expected, actual);
    //}
    //#endregion
    //#endregion

    //#region void ValidateQuantity
    //#region IBaseQuantifiable.ValidateQuantity(ValueType?, string)
    //[TestMethod, TestCategory("UnitTest")]
    //public void ValidateQuantity_nullArg_ValueType_arg_string_throws_ArgumentNullException()
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);
    //    paramName = Fields.RandomParams.GetRandomParamName();
    //    ValueType quantity = null;

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, paramName);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
    //    Assert.AreEqual(paramName, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetValidateQuantityInvalidQuantityTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    //public void ValidateQuantity_invalidArg_ValueType_arg_string_throws_ArgumentOutOfRangeException(TypeCode typeCode)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);
    //    ValueType quantity = Fields.RandomParams.GetRandomValueType(typeCode);
    //    paramName = Fields.RandomParams.GetRandomParamName();

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, paramName);

    //    // Assert
    //    var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
    //    Assert.AreEqual(paramName, ex.ParamName);
    //}

    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetValidateQuantityValidQuantityTypeCodeArgArrayList), DynamicDataSourceType.Method)]
    //public void ValidateQuantity_validArg_ValueType_arg_string_returns_expected(TypeCode typeCode)
    //{
    //    // Arrange
    //    SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);
    //    ValueType quantity = Fields.RandomParams.GetRandomValueType(typeCode);

    //    // Act
    //    void attempt() => _baseQuantifiable.ValidateQuantity(quantity, null);

    //    // Assert
    //    Assert.IsTrue(Returned(attempt));
    //}
    //#endregion
    //#endregion
    //#endregion

    //#region Private methods
    //private void SetBaseQuantifiableChild(Enum measureUnit, IBaseMeasurementFactory factory, decimal defaultQuantity)
    //{
    //    _baseQuantifiable = new(RootObject, paramName)
    //    {
    //        Return = new()
    //        {
    //            GetBaseMeasureUnit = measureUnit,
    //            GetFactory = factory,
    //            GetDefaultQuantity = defaultQuantity,
    //        }
    //    };
    //}

    //#region DynamicDataSource
    //private static IEnumerable<object[]> GetEqualsArgsArrayList()
    //{
    //    return DynamicDataSource.GetEqualsArgsArrayList();
    //}

    //private static IEnumerable<object[]> GetFitsInArgsArrayList()
    //{
    //    return DynamicDataSource.GetFitsInArgsArrayList();
    //}

    //private static IEnumerable<object[]> GetValidateQuantityInvalidQuantityTypeCodeArgArrayList()
    //{
    //    return DynamicDataSource.GetValidateQuantityInvalidQuantityTypeCodeArgArrayList();
    //}

    //private static IEnumerable<object[]> GetValidateQuantityValidQuantityTypeCodeArgArrayList()
    //{
    //    return DynamicDataSource.GetValidateQuantityValidQuantityTypeCodeArgArrayList();
    //}
    //#endregion
    //#endregion
}
