using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.QuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class QuantifiableTests
{
    #region Tested in parent classes' tests

    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)

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
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit();
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

    #region Test methods
    #region int CompareTo
    #region IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        IQuantifiable other = null;
        const int expected = 1;

        // Act
        var actual = _quantifiable.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IQuantifiable_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        IQuantifiable other = new QuantifiableChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
                GetDefaultQuantity = Fields.RandomParams.GetRandomDecimal(),
            }
        };

        // Act
        void attempt() => _ = _quantifiable.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        IQuantifiable other = new QuantifiableChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
                GetDefaultQuantity = Fields.RandomParams.GetRandomDecimal(),
            }
        };
        int expected = _quantifiable.GetDefaultQuantity().CompareTo(other.GetDefaultQuantity());

        // Act
        var actual = _quantifiable.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(Enum measureUnit, decimal defaultQuantity, bool expected, IQuantifiable other)
    {
        // Arrange
        SetQuantifiableChild(measureUnit, null, defaultQuantity);

        // Act
        var actual = _quantifiable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable? ExchangeTo
    #region IExchange<IQuantifiable, Enum>.ExchangeTo(Enum? context)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetExchangeToArgs), DynamicDataSourceType.Method)]
    public void ExchangeTo_arg_returns_expected(Enum measureUnit, Enum context, IQuantifiable expected)
    {
        // Arrange
        SetQuantifiableChild(measureUnit, null, Fields.defaultQuantity);

        // Act
        var actual = _quantifiable.ExchangeTo(context);

        // Assert
        Assert.AreEqual(expected?.GetType(), actual?.GetType());
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetQuantifiableChild(measureUnit, null, Fields.defaultQuantity);

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.measureUnit, null, Fields.defaultQuantity);
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
        LimiterQuantifiableOblect limiter = new(Fields.RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
                GetDefaultQuantity = otherQuantity,
            },
            LimitMode = Fields.limitMode,
        };
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)

    #endregion
    #endregion



    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool IExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable exchanged)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)

    #endregion



    #region Private methods
    private void SetQuantifiableChild(Enum measureUnit, IQuantifiableFactory factory, decimal defaultQuantity)
    {
        _quantifiable = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetDefaultQuantity = defaultQuantity,
            }
        };
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }

    private static IEnumerable <object[]> GetExchangeToArgs()
    {
        return DynamicDataSource.GetExchangeToArgs();
    }

    private static IEnumerable<object[]> GetFitsInILimiterArgs()
    {
        return DynamicDataSource.GetFitsInILimiterArgs();
    }
    #endregion
    #endregion
}
