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
    // decimal IDefaultQuantity.GetDefaultQuantityValue()
    // IFactory ICommonBase.GetFactoryValue()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(RateComponentCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)

    #endregion

    #region Private fields
    private SpreadChild _spread;
    private ISpreadMeasure _spreadMeasure;
    private RandomParams _randomParams;

    #region Readonly fields
    private DataFields _fields;
    //private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static readonly DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    private const string LogFileName = "testLog_SpreadTestsLogs";
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        _fields = new();
        _randomParams = _fields.RandomParams;

        _fields.SetBaseMeasureFields(
            _randomParams.GetRandomSpreadMeasureUnitCode(),
            _randomParams.GetRandomPositiveDouble());
    }

    [TestCleanup]
    public void TestCleanup()
    {
        _spread = null;
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

        Enum expected = _fields.measureUnit;

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

        ValueType expected = (ValueType)_fields.quantity.ToQuantity(TypeCode.Double);

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

        double expected = (double)_fields.doubleQuantity.ToQuantity(TypeCode.Double);

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

        TypeCode expected = _fields.quantityTypeCode;

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

        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _fields.quantity = _randomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(_fields);
        ISpread expected = GetSpreadChild(_fields, new SpreadFactoryObject());

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

        ISpreadMeasure expected = GetSpreadMeasureBaseMeasureObject(_fields);

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
        SetSpreadChild(measureUnit, _fields.quantity);

        _fields.paramName = _randomParams.GetRandomParamName();

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
        SetSpreadChild(measureUnit, _fields.quantity);

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

        _fields.roundingMode = SampleParams.NotDefinedRoundingMode;

        // Act
        void attempt() => _ = _spread.Round(_fields.roundingMode);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.roundingMode, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Round_validArg_RoundingMode_returns_expected()
    {
        // Arrange
        SetCompleteSpreadChild();

        _fields.roundingMode = _randomParams.GetRandomRoundingMode();
        _fields.quantity = (ValueType)_fields.quantity.Round(_fields.roundingMode);
        IQuantifiable expected = GetCompleteSpreadChild(_fields);

        // Act
        var actual = _spread.Round(_fields.roundingMode);

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
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateSpreadMeasureArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_ArgumentOutOfRangeException(string testCase, Enum measureUnit, ISpreadMeasure spreadMeasure)
    {
        // Arrange
        SetSpreadChild(measureUnit, _fields.quantity);

        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(spreadMeasure, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_invalidArg_ISpreadMeasure_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetSpreadChild();

        _fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(_fields.measureUnitCode);
        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(_fields); 
        _fields.paramName = _randomParams.GetRandomParamName();

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, _fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateSpreadMeasure_validArg_ISpreadMeasure_returns()
    {
        // Arrange
        SetSpreadChild();

        _fields.measureUnit = _randomParams.GetRandomMeasureUnit(_fields.measureUnitCode);
        _fields.quantity = _randomParams.GetRandomPositiveDouble();
        _spreadMeasure = GetSpreadMeasureBaseMeasureObject(_fields);

        // Act
        void attempt() => _spread.ValidateSpreadMeasure(_spreadMeasure, _fields.paramName);

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

    #region Logger
    private void WriteLog(object actual, string testMethodName)
    {
        StartLog(BaseTypesLogDirectory, LogFileName, testMethodName);

        double doubleQuantity = _fields.doubleQuantity;

        LogVariable(BaseTypesLogDirectory, LogFileName, "doubleQuantity", doubleQuantity);
        LogVariable(BaseTypesLogDirectory, LogFileName, "quantity", _fields.quantity);

        Enum measureUnit = _fields.measureUnit;

        LogVariable(BaseTypesLogDirectory, LogFileName, "measureUnit name", measureUnit);
        LogVariable(BaseTypesLogDirectory, LogFileName, "defaultQuantity", _fields.defaultQuantity);
        LogVariable(BaseTypesLogDirectory, LogFileName, "exchangeRate", BaseMeasurement.GetExchangeRate(measureUnit, string.Empty));
        LogVariable(BaseTypesLogDirectory, LogFileName, "actual", actual);

        double difference = doubleQuantity - (double)actual;

        LogVariable(BaseTypesLogDirectory, LogFileName, "difference", difference.ToString("F30"));

        EndLog(BaseTypesLogDirectory, LogFileName);
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
        _spread = GetSpreadChild(_fields);
    }

    private void SetCompleteSpreadChild()
    {
        _spread = GetCompleteSpreadChild(_fields);
    }
    #endregion
}