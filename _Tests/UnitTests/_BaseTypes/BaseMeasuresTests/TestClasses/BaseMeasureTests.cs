namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasureTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? baseMeasure)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? baseMeasure)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? baseMeasure, LimitMode? limitMode)
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal quantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? baseMeasure)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

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
        Fields.measureUnit = Fields.RandomParams.GetRandomValidMeasureUnit();
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();
        Fields.quantityTypeCode = Fields.RandomParams.GetRandomQuantityTypeCode(Fields.rateComponentCode);
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity(Fields.quantityTypeCode);
    }

    [TestCleanup]
    public void CleanupBaseQuantifiableTests()
    {
        Fields.paramName = null;

        RestoreConstantExchangeRates();
    }
    #endregion

    #region Private fields
    private BaseMeasureChild _baseMeasure;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods

    #region bool Equals
    #region IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure?, IBaseMeasure?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArg), DynamicDataSourceType.Method)]
    public void Equals_nullArg_IBaseMeasure_arg_IBaseMeasure_returns_expected(IBaseMeasure baseMeasure)
    {
        // Arrange
        SetBaseMeasureChild();
        bool expected = baseMeasure is null;

        // Act
        var actual = _baseMeasure.Equals(null, baseMeasure);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest"), Ignore]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_args_IBaseMeasure_IBaseMeasure_returns_expected(bool expected, Enum measureUnit, ValueType quantity, RateComponentCode rateComponentCode, IBaseMeasure other)
    {
        // Arrange
        SetBaseMeasureChild(measureUnit, quantity, rateComponentCode);

        // Act
        var actual = _baseMeasure.Equals(_baseMeasure, other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    // IBaseMeasure IBaseMeasure.GetBaseMeasure(ValueType quantity)
    // IBaseMeasure IBaseMeasure.GetBaseMeasure(IBaseMeasurement baseMeasurement, ValueType quantity)
    // IBaseMeasurement IBaseMeasure.GetBaseMeasurement()
    // IBaseMeasurementFactory IBaseMeasure.GetBaseMeasurementFactory()
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IExchangeRate.GetExchangeRate()
    // int IEqualityComparer<IBaseMeasure>.GetHashCode(IBaseMeasure obj)
    // LimitMode? ILimitMode.GetLimitMode()
    // TypeCode? IQuantityTypeCode.GetQuantityTypeCode(object quantity)
    // RateComponentCode IRateComponentCode.GetRateComponentCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)

    #endregion

    #region Private methods
    private void SetBaseMeasureChild(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null)
    {
        _baseMeasure = GetBaseMeasureChild(measureUnit, quantity, rateComponentCode);
    }

    private void SetBaseMeasureChild()
    {
        SetBaseMeasureChild(Fields.measureUnit, Fields.quantity);
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
