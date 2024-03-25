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

        TestSupport.RestoreConstantExchangeRates();
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
    #region virtual IComparable<IQuantifiable>.CompareTo(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity);

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
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

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
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

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
    #region virtual IEquatable<IQuantifiable>.Equals(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(Enum measureUnit, decimal defaultQuantity, bool expected, IQuantifiable other)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    //#region IQuantifiable? ExchangeTo
    //#region IExchange<IQuantifiable, Enum>.ExchangeTo(Enum? context)
    //[TestMethod, TestCategory("UnitTest")]
    //[DynamicData(nameof(GetExchangeToArgs), DynamicDataSourceType.Method)]
    //public void ExchangeTo_arg_returns_expected(Enum measureUnit, Enum context, IQuantifiable expected)
    //{
    //    // Arrange
    //    SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

    //    // Act
    //    var actual = _quantifiable.ExchangeTo(context);

    //    // Assert
    //    Assert.AreEqual(expected?.GetType(), actual?.GetType());
    //}
    //#endregion
    //#endregion

    #region bool? FitsIn
    #region override ILimitable.FitsIn(ILimiter?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArg_ILimiter_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        ILimiter limiter = null;

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInILimiterArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(ILimiter limiter)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity);

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArg_ILimiter_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

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

    #region virtual IFit<IQuantifiable>.FitsIn(IQuantifiable?, LimitMode?)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_nullArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity);

        ILimiter limiter = null;

        // Act
        var actual = _quantifiable.FitsIn(limiter);

        // Assert
        Assert.IsTrue(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetFitsInIQuantifiableLimitModeArgs), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArgs_IQuantifiable_LimitMode_returns_null(Enum measureUnit, LimitMode? limitMode, IQuantifiable other)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.FitsIn(other, limitMode);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_validArgs_IQuantifiable_LimitMode_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        decimal otherQuantity = Fields.RandomParams.GetRandomDecimal();
        QuantifiableChild quantifiable = new(Fields.RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode),
                GetDefaultQuantity = otherQuantity,
            },
        };
        Fields.limitMode = Fields.RandomParams.GetRandomLimitMode();
        bool? expected = Fields.defaultQuantity.FitsIn(otherQuantity, Fields.limitMode);

        // Act
        var actual = _quantifiable.FitsIn(quantifiable, Fields.limitMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region ValueType GetBaseQuantity
    #region IQuantity.GetBaseQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseQuantity_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        TypeCode typeCode = Fields.measureUnitCode.GetQuantityTypeCode();
        ValueType expected = (ValueType)Fields.defaultQuantity.ToQuantity(typeCode);

        // Act
        var actual = _quantifiable.GetBaseQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal GetDecimalQuantity
    #region IDecimalQuantity.GetDecimalQuantity()
    [TestMethod, TestCategory("UnitTest")]
    public void GetDecimalQuantity_returns_expected()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        ValueType quantity = _quantifiable.GetBaseQuantity();
        decimal expected = (decimal)quantity.ToQuantity(TypeCode.Decimal);

        // Act
        var actual = _quantifiable.GetDecimalQuantity();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IQuantifiable GetQuantifiable
    #region IQuantifiable.GetQuantifiable(MeasureUnitCode, decimal)
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantifiable_invalidArg_MeasureUnitCode_arg_decimal_throws_InvalidEnumArgumentException()
    {
        // Arrange
        QuantifiableFactoryObject factory = new();
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit, factory);

        // Act
        var actual = _quantifiable.GetQuantifiable(Fields.measureUnitCode, Fields.defaultQuantity);

        // Assert
        Assert.IsInstanceOfType<IQuantifiable>(actual);
    }
    #endregion
    #endregion

    #region object? GetQuantity
    #region IRound<IQuantifiable>.GetQuantity(RoundingMode)
    [TestMethod, TestCategory("UnitTest")]
    public void GetQuantity_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.GetQuantity(SampleParams.NotDefinedRoundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory ("UnitTest")]
    [DynamicData(nameof(GetGetQuantityRoundinModeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_arg_RoundingMode_returns_expected(Enum measureUnit, decimal defaultQuantity, object expected, RoundingMode roundingMode)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.GetQuantity(roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    #endregion

    #region IQuantity.GetQuantity(TypeCode)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityInvalidTypeCodeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_invalidArg_TypeCode_throws_InvalidEnumArgumentException(TypeCode typeCode)
    {
        // Arrange
        SetQuantifiableChild(Fields.defaultQuantity, Fields.measureUnit);

        // Act
        void attempt() => _ = _quantifiable.GetQuantity(typeCode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.quantityTypeCode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetQuantityValidTypeCodeArgs), DynamicDataSourceType.Method)]
    public void GetQuantity_arg_TypeCode_returns_expected(Enum measureUnit, decimal defaultQuantity, object expected, TypeCode quantityTypeCode)
    {
        // Arrange
        SetQuantifiableChild(defaultQuantity, measureUnit);

        // Act
        var actual = _quantifiable.GetQuantity(quantityTypeCode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion


    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(TypeCode quantityTypeCode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum? context, out IQuantifiable? exchanged)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)

    #endregion



    #region Private methods
    private void SetQuantifiableChild(decimal defaultQuantity, Enum measureUnit = null, IQuantifiableFactory factory = null)
    {
        _quantifiable = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetDefaultQuantity = defaultQuantity,
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
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

    private static IEnumerable<object[]> GetFitsInIQuantifiableLimitModeArgs()
    {
        return DynamicDataSource.GetFitsInIQuantifiableLimitModeArgs();
    }

    private static IEnumerable<object[]> GetQuantifiableInvalidArgs()
    {
        return DynamicDataSource.GetQuantifiableInvalidArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityRoundinModeArgs()
    {
        return DynamicDataSource.GetGetQuantityRoundinModeArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityInvalidTypeCodeArgs()
    {
        return DynamicDataSource.GetGetQuantityInvalidTypeCodeArgs();
    }

    private static IEnumerable<object[]> GetGetQuantityValidTypeCodeArgs()
    {
        return DynamicDataSource.GetGetQuantityValidTypeCodeArgs();
    }
    #endregion
    #endregion
}
