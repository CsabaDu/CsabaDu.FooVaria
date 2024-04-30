namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class SpreadTests
{
    #region Tested in parent classes' tests

    // Spread(IRootObject rootObject, string paramName)
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
    private RandomParams _randomParams;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _randomParams = Fields.RandomParams;

        Fields.SetBaseMeasureFields(
            _randomParams.GetRandomSpreadMeasureUnitCode(),
            _randomParams.GetRandomPositiveDouble());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        _spreadMeasure = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
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
    #region ISpread.GetSpread(ISpreadMeasure)
    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_nullArg_ISpreadMeasure_throws_ArgumentNullException()
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;

        // Act
        void attempt() =>_ = _spread.GetSpread(_spreadMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.spreadMeasure, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_invalidArg_ISpreadMeasure_throws_ArgumentNullException() // TODO
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;

        // Act
        void attempt() => _ = _spread.GetSpread(_spreadMeasure);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.spreadMeasure, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetSpread_validArg_ISpreadMeasure_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantity = _randomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(Fields);
        ISpread expected = GetSpreadChild(Fields, new SpreadFactoryObject());

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

        ISpreadMeasure expected = GetSpreadMeasureBaseMeasureObject(Fields);

        // Act
        var actual = _spread.GetSpreadMeasure();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region ISpread.GetSpreadMeasure(IQuantifiable?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetGetSpreadMeasureArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void GetSpreadMeasure_arg_ISpreadMeasure_returns_expected(string testCase, Enum measureUnit, ISpreadMeasure expected, IQuantifiable quantifiable)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        var actual = _spread.GetSpreadMeasure(quantifiable);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region override sealed IExchangeable<Enum>.IsExchangeableTo(Enum?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void IsExchangeableTo_arg_Enum_returns_expected(string testCase, bool expected, Enum measureUnit, Enum context)
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
    [TestMethod, TestCategory("UnitTest")]
    public void Round_invalidArg_RoundingMode_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetSpreadChild();

        Fields.roundingMode = SampleParams.NotDefinedRoundingMode;

        // Act
        void attempt() => _ = _spread.Round(Fields.roundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Round_validArg_RoundingMode_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        Fields.roundingMode = _randomParams.GetRandomRoundingMode();
        Fields.quantity = (ValueType)Fields.quantity.Round(Fields.roundingMode);
        IQuantifiable expected = GetCompleteSpreadChild(Fields);

        // Act
        var actual = _spread.Round(Fields.roundingMode);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateSpreadMeasure
    #region ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_nullArg_ISpreadMeasure_throws_ArgumentNullException()
    {
        // Arrange
        SetSpreadChild();

        _spreadMeasure = null;
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateSpreadMeasureArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_ArgumentOutOfRangeException(string testCase, Enum measureUnit, ISpreadMeasure spreadMeasure)
    {
        // Arrange
        SetSpreadChild(measureUnit, Fields.quantity);

        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetSpreadChild();

        Fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(Fields); 
        Fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_validArg_ISpreadMeasure_returns()
    {
        // Arrange
        SetSpreadChild();

        Fields.measureUnit = _randomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        Fields.quantity = _randomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(Fields);

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, Fields.paramName);

        // Assert
        Assert.IsTrue(DoesNotThrowException(attempt));
    }
    #endregion
    #endregion

    //    //#region Static methods

    //    //#endregion
    //    #endregion
    #endregion

    #region Private methods
    #region DynamicDataSource
    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    {
        return DynamicDataSource.GetIsExchangeableToArgs();
    }

    private static IEnumerable<object[]> GetGetSpreadMeasureArgs()
    {
        return DynamicDataSource.GetGetSpreadMeasureArgs();
    }

    private static IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    {
        return DynamicDataSource.GetValidateSpreadMeasureArgs();
    }
    #endregion

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
        _spread = GetSpreadChild(Fields);
    }

    private void SetCompleteSpreadChild()
    {
        _spread = GetCompleteSpreadChild(Fields);
    }
    #endregion
}