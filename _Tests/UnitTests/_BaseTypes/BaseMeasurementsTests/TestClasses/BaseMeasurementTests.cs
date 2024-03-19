namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasurementTests
{
    #region Tested in parent classes' tests

    // BaseMeasurement(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseMeasurementTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeBaseMeasurementTests()
    {
        Fields.measureUnit = Fields.RandomParams.GetRandomValidMeasureUnit();
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        Fields.measureUnitType = Fields.measureUnit.GetType();
    }

    [TestCleanup]
    public void CleanupBaseMeasurementTests()
    {
        Fields.paramName = null;
        _baseMeasurement = null;

        TestSupport.RestoreConstantExchangeRates();
    }
    #endregion

    #region Private fields
    private BaseMeasurementChild _baseMeasurement;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region int CompareTo
    #region IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IBaseMeasurement_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        IBaseMeasurement other = null;
        const int expected = 1;

        // Act
        var actual = _baseMeasurement.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IBaseMeasurement_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        IBaseMeasurement other = new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomValidMeasureUnit(Fields.measureUnitCode),
            }
        };

        // Act
        void attempt() => _ = _baseMeasurement.CompareTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IBaseMeasurement_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        IBaseMeasurement other = new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomValidMeasureUnit(Fields.measureUnitCode),
            }
        };
        int expected = _baseMeasurement.GetExchangeRate().CompareTo(other.GetExchangeRate());

        // Act
        var actual = _baseMeasurement.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region IEquatable<IBaseMeasurement>.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsObjectArg), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit);

        // Act
        var actual = _baseMeasurement.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region BaseMeasurement.Equals(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsBaseMeasurementArg), DynamicDataSourceType.Method)]
    public void Equals_arg_IBaseMeasurement_returns_expected(bool expected, Enum measureUnit, IBaseMeasurement other)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit);

        // Act
        var actual = _baseMeasurement.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IBaseMeasurement GetBaseMeasurement
    #region IBaseMeasurement.GetBaseMeasurement(Enum context)
    [TestMethod, TestCategory("UnitTest")]
    public void GetBaseMeasurement_arg_creates()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit, new BaseMeasurementFactoryObject());
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnitOrMeasureUnitCode();

        // Act
        var actual = _baseMeasurement.GetBaseMeasurement(Fields.measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
    }
    #endregion
    #endregion

    #region IDictionary<object, decimal> GetConstantExchangeRateCollection
    #region IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    [TestMethod, TestCategory("UnitTest")]
    public void GetConstantExchangeRateCollection_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        IDictionary<object, decimal> expected = ConstantExchangeRateCollection
            .Where(x => x.Key.GetType().Name == Enum.GetName(Fields.measureUnitCode))
            .ToDictionary(x => x.Key, x => x.Value);

        // Act
        var actual = _baseMeasurement.GetConstantExchangeRateCollection();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region decimal GetExchangeRate
    #region IExchangeRate.GetExchangeRate()
    [TestMethod, TestCategory("UnitTest")]
    public void GetExchangeRate_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        decimal expected = ExchangeRateCollection
            .FirstOrDefault(x => x.Key.Equals(Fields.measureUnit))
            .Value;

        // Act
        var actual = _baseMeasurement.GetExchangeRate();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region IDictionary<object, decimal> GetExchangeRateCollection
    #region IExchangeRateCollection.GetExchangeRateCollection()
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetExchangeRateCollectionArg), DynamicDataSourceType.Method)]
    public void GetExchangeRateCollection_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit);
        IDictionary<object, decimal> expected = ExchangeRateCollection
            .Where(x => x.Key.GetType().Name == Enum.GetName(measureUnitCode))
            .ToDictionary(x => x.Key, x => x.Value);

        // Act
        var actual = _baseMeasurement.GetExchangeRateCollection();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region int GetHashCode
    #region BaseMeasurement.GetHashCode()
    [TestMethod, TestCategory("UnitTest")]
    public void GetHashCode_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        int expected = HashCode.Combine(_baseMeasurement.GetMeasureUnitCode(), _baseMeasurement.GetExchangeRate());

        // Act
        var actual = _baseMeasurement.GetHashCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region string GetName
    #region INamed.GetName()
    [TestMethod, TestCategory("UnitTest")]
    public void GetName_returns_expected()
    {
        // Arrange
        string expected = Fields.RandomParams.GetRandomParamName();
        SetBaseMeasurementChild(null, null, expected);

        // Act
        var actual = _baseMeasurement.GetName();

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
        SetBaseMeasurementChild(Fields.measureUnit);
        TypeCode expected = Fields.measureUnitCode.GetQuantityTypeCode();

        // Act
        var actual = _baseMeasurement.GetQuantityTypeCode();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region IExchangeable<Enum>.IsExchangeableTo(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetIsExchangeableToArg), DynamicDataSourceType.Method)]
    public void IsExchangeableTo_returns_expected(bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit);

        // Act
        var actual = _baseMeasurement.IsExchangeableTo(context);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region decimal ProportionalTo
    #region IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement)
    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_nullArg_IBaseMeasurement_throws_ArgumentNullException()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        BaseMeasurementChild other = null;

        // Act
        void attempt() => _ = _baseMeasurement.ProportionalTo(other);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_invalidArg_IBaseMeasurement_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        BaseMeasurementChild other = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomValidMeasureUnit(Fields.measureUnitCode)
            }
        };

        // Act
        void attempt() => _ = _baseMeasurement.ProportionalTo(other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ProportionalTo_validArg_IBaseMeasurement_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        BaseMeasurementChild other = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = Fields.RandomParams.GetRandomValidMeasureUnit(Fields.measureUnitCode)
            }
        };
        decimal expected = _baseMeasurement.GetExchangeRate() / other.GetExchangeRate();

        // Act
        var actual = _baseMeasurement.ProportionalTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region void ValidateExchangeRate
    #region IExchangeRate.ValidateExchangeRate(decimal, string)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateExchangeRateArg), DynamicDataSourceType.Method)]
    public void ValidateExchangeRate_invalidArg_decimal_arg_string_throws_ArgumentOutOfRangeException(Enum measureUnit, decimal exchangeRate)
    {
        // Arrange
        Fields.paramName = Fields.RandomParams.GetRandomParamName();
        SetBaseMeasurementChild(measureUnit);

        // Act
        void attempt() => _baseMeasurement.ValidateExchangeRate(exchangeRate, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateExchangeRate_validArg_decimal_arg_string_returns()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        decimal exchangeRate = GetExchangeRate(Fields.measureUnit, null);

        // Act
        void validator() => _baseMeasurement.ValidateExchangeRate(exchangeRate, null);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion
    #endregion

    #region void ValidateMeasureUnit
    #region IDefaultMeasureUnit.ValidateMeasureUnit(Enum?, string)
    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_nullArg_Enum_arg_string_throws_ArgumentNullException()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        Fields.paramName = Fields.RandomParams.GetRandomParamName();

        // Act
        void attempt() => _baseMeasurement.ValidateMeasureUnit(null, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasurementChild(Fields.measureUnit);
        Fields.measureUnitCode = Fields.RandomParams.GetRandomMeasureUnitCode(Fields.measureUnitCode);
        Enum context = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);

        // Act
        void attempt() => _baseMeasurement.ValidateMeasureUnit(context, Fields.paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(Fields.paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetValidateMeasureUnitValidArgs), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit);

        // Act
        void validator() => _baseMeasurement.ValidateMeasureUnit(context, Fields.paramName);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion
    #endregion

    //#region Static methods

    //#endregion
    #endregion

    #region Private methods
    private void SetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory = null, string measureUnitName = null)
    {
        _baseMeasurement = new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetName = measureUnitName,
            }
        };
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsObjectArg()
    {
        return DynamicDataSource.GetEqualsObjectArg();
    }

    private static IEnumerable<object[]> GetEqualsBaseMeasurementArg()
    {
        return DynamicDataSource.GetEqualsBaseMeasurementArg();
    }

    private static IEnumerable<object[]> GetExchangeRateCollectionArg()
    {
        return DynamicDataSource.GetExchangeRateCollectionArg();
    }

    private static IEnumerable<object[]> GetIsExchangeableToArg()
    {
        return DynamicDataSource.GetIsExchangeableToArg();
    }

    private static IEnumerable<object[]> GetValidateExchangeRateArg()
    {
        return DynamicDataSource.GetValidateExchangeRateArg();
    }

    private static IEnumerable<object[]> GetValidateMeasureUnitValidArgs()
    {
        return DynamicDataSource.GetValidateMeasureUnitValidArgs();
    }
    #endregion
    #endregion
}