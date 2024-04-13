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
    private ISpreadMeasure _spreadMeasure;

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
        Fields.SetBaseMeasureFields(
            Fields.RandomParams.GetRandomSpreadMeasureUnitCode(),
            Fields.RandomParams.GetRandomPositiveDouble());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        _spreadMeasure = null;
    }
    #endregion

    #region Test methods
    #region Enum GetBaseMeasureUnit
    #region override IMeasureUnit.GetBaseMeasureUnit()
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

    #region ValueType GetBaseQuantity
    #region override sealed IQuantity.GetBaseQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseQuantity_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        ValueType expected = Fields.quantity;

        // Act
        var actual = _spread.GetBaseQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region double GetQuantity
    #region virtual IQuantity<double>.GetQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        double expected = Fields.doubleQuantity;

        // Act
        var actual = _spread.GetQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region TypeCode GetQuantityTypeCode
    #region IQuantityType.GetQuantityTypeCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantityTypeCode_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        TypeCode expected = Fields.quantityTypeCode;

        // Act
        var actual = _spread.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ISpread GetSpread
    #region abstract ISpread.GetSpread(ISpreadMeasure)
    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_arg_ISpreadMeasure_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(Fields.measureUnit, Fields.quantity);
        SpreadFactoryObject factory = new();
        ISpread expected = GetSpreadChild(Fields.measureUnit, Fields.quantity, factory);

        // Act
        var actual = _spread.GetSpread(_spreadMeasure);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ISpreadMeasure GetSpreadMeasure
    #region abstract ISpreadMeasure.GetSpreadMeasure()
    [TestMethod, TestCategory("UnitTest")]
    public void GetSpreadMeasure_returns_expected()
    {
        // Arrange
        SetSpreadChild();

        ISpreadMeasure expected = GetSpreadMeasureBaseMeasureObject(Fields.measureUnit, Fields.quantity);

        // Act
        var actual = _spread.GetSpreadMeasure();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region override sealed IExchangeable<Enum>.IsExchangeableTo(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArgs), DynamicDataSourceType.Method)]
    public void IsExchangeableTo_arg_Enum_returns_expected(bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        // Act
        var actual = _spread.IsExchangeableTo(context);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable Round
    #region override sealed IRound<IQuantifiable>.Round(RoundingMode)

    #endregion
    #endregion





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
    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        return DynamicDataSource.GetIsExchangeableToArgs();
    }

    #endregion
    #endregion
}