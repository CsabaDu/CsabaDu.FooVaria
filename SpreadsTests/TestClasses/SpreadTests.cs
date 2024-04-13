namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class SpreadTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Private fields
    private SpreadChild _spread;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
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
        Fields.SetMeasureUnit(Fields.RandomParams.GetRandomSpreadMeasureUnitCode());
        Fields.SetDoubleQuantity(Fields.RandomParams.GetRandomPositiveDouble());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Test methods

    #region Enum GetBaseMeasureUnit
    #region IMeasureUnit.GetBaseMeasureUnit()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasureUnit_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        Enum expected = Fields.measureUnit;

        // Act
        var actual = _spread.GetBaseMeasureUnit();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion



    // ValueType IQuantity.GetBaseQuantity()
    // double IQuantity<double>.GetQuantity()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // MeasureUnitCode ISpreadMeasure.GetSpreadMeasureUnitCode()
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)

    #endregion

    //    //#region Static methods

    //    //#endregion
    //    #endregion

    #region Private methods
    private void SetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    {
        _spread = GetSpreadChild(measureUnit, quantity, factory, rateComponentCode);
    }

    private void SetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    {
        _spread = GetSpreadChild(spreadMeasure, factory);
    }


    private void SetSpreadChild()
    {
        SetSpreadChild(Fields.measureUnit, Fields.quantity);
    }

    private void SetCompleteSpreadChild()
    {
        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();

        SetSpreadChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), Fields.rateComponentCode);
    }

    #region DynamicDataSource
    //private static IEnumerable<object[]> GetEqualsObjectArg()
    //{
    //    return DynamicDataSource.GetEqualsObjectArg();
    //}

    //private static IEnumerable<object[]> GetEqualsBaseMeasurementArg()
    //{
    //    return DynamicDataSource.GetEqualsBaseMeasurementArg();
    //}

    //private static IEnumerable<object[]> GetExchangeRateCollectionArg()
    //{
    //    return DynamicDataSource.GetExchangeRateCollectionArg();
    //}

    //private static IEnumerable<object[]> GetIsExchangeableToArg()
    //{
    //    return DynamicDataSource.GetIsExchangeableToArg();
    //}

    //private static IEnumerable<object[]> GetHasMeasureUnitCodeArgs()
    //{
    //    return DynamicDataSource.GetHasMeasureUnitCodeArgs();
    //}

    //private static IEnumerable<object[]> GetValidateExchangeRateArg()
    //{
    //    return DynamicDataSource.GetValidateExchangeRateArg();
    //}

    //private static IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    //{
    //    return DynamicDataSource.GetValidateMeasureUnitValidArgs();
    //}
    #endregion
    #endregion
}