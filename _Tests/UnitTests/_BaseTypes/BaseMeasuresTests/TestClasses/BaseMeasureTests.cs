namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasureTests
{
    #region Tested in parent classes' tests

    // decimal IDecimalQuantity.GetDecimalQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal quantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? rightBaseMeasure)
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
        Fields.quantityTypeCode = Fields.RandomParams.GetRandomQuantityTypeCode(Fields.rateComponentCode);
        Fields.quantity = (ValueType)Fields.RandomParams.GetRandomQuantity(Fields.quantityTypeCode);
        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();
        Fields.limitMode = Fields.RandomParams.GetRandomNullableLimitMode();
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

    #endregion

    #region IEqualityComparer<IBaseMeasure>.Equals(IBaseMeasure?, IBaseMeasure?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_args_IBaseMeasure_IBaseMeasure_returns_expected(IBaseMeasure leftBaseMeasure, bool expected, IBaseMeasure rightBaseMeasure)
    {
        // Arrange
        SetBaseMeasureChild();

        // Act
        var actual = _baseMeasure.Equals(leftBaseMeasure, rightBaseMeasure);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region bool? FitsIn
    #region override sealed ILimitable.FitsIn(ILimiter?)

    #endregion

    #region override sealed IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)

    #endregion
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
    private void SetBaseMeasureChild(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null, LimitMode? limitMode = null)
    {
        _baseMeasure = GetBaseMeasureChild(measureUnit, quantity, rateComponentCode, limitMode);
    }

    private void SetBaseMeasureChild()
    {
        SetBaseMeasureChild(Fields.measureUnit, Fields.quantity);
    }

    #region DynamicDataSource
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
