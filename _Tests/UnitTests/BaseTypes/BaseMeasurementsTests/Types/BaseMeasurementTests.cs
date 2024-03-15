namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasurementTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseMeasurementTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeBaseMeasurementTests()
    {
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _measureUnitType = _measureUnit.GetType();
    }

    [TestCleanup]
    public void CleanupBaseMeasurementTests()
    {
        _paramName = null;
        _baseMeasurement = null;

        TestSupport.RestoreConstantExchangeRates();
    }
    #endregion

    #region Private fields
    private BaseMeasurementChild _baseMeasurement;
    private Enum _measureUnit;
    private MeasureUnitCode _measureUnitCode;
    private Type _measureUnitType;
    private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    private readonly RootObject RootObject = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Test methods
    #region Tested in parent classes' tests

    // BaseMeasurement(IRootObject rootObject, string paramName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region int CompareTo
    #region IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IBaseMeasurement_returns_expected()
    {
        // Arrange
        SetBaseMeasurementChild(_measureUnit, null, null);
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
        SetBaseMeasurementChild(_measureUnit, null, null);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        IBaseMeasurement other = new BaseMeasurementChild(RootObject, _paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode)
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
        SetBaseMeasurementChild(_measureUnit, null, null);
        BaseMeasurementChild other = new(RootObject, _paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode)
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
    [DynamicData(nameof(GetBaseMeasurementEqualsObjectArgArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit, null, null);

        // Act
        var actual = _baseMeasurement.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region BaseMeasurement.Equals(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurementEqualsBaseMeasurementArgArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_IBaseMeasurement_returns_expected(bool expected, Enum measureUnit, IBaseMeasurement other)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit, null, null);

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
        SetBaseMeasurementChild(_measureUnit, new BaseMeasurementFactoryObject(), null);
        _measureUnit = RandomParams.GetRandomMeasureUnitOrMeasureUnitCode();

        // Act
        var actual = _baseMeasurement.GetBaseMeasurement(_measureUnit);

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
        SetBaseMeasurementChild(_measureUnit, null, null);
        IDictionary<object, decimal> expected = ConstantExchangeRateCollection
            .Where(x => x.Key.GetType().Name == Enum.GetName(_measureUnitCode))
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
        SetBaseMeasurementChild(_measureUnit, null, null);
        decimal expected = ExchangeRateCollection
            .FirstOrDefault(x => x.Key.Equals(_measureUnit))
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
    [DynamicData(nameof(GetExchangeRateCollectionArgArrayList), DynamicDataSourceType.Method)]
    public void GetExchangeRateCollection_returns_expected(Enum measureUnit, MeasureUnitCode measureUnitCode)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit, null, null);
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
        SetBaseMeasurementChild(_measureUnit, null, null);
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
        string expected = RandomParams.GetRandomParamName();
        SetBaseMeasurementChild(null, null, expected);

        // Act
        var actual = _baseMeasurement.GetName();

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool IsExchangeableTo
    #region IExchangeable<Enum>.IsExchangeableTo(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurementIsExchangeableToArgArrayList), DynamicDataSourceType.Method)]
    public void IsExchangeableTo_returns_expected(bool expected, Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit, null, null);

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
        SetBaseMeasurementChild(_measureUnit, null, null);
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
        SetBaseMeasurementChild(_measureUnit, null, null);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        BaseMeasurementChild other = new(RootObject, _paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode)
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
        SetBaseMeasurementChild(_measureUnit, null, null);
        BaseMeasurementChild other = new(RootObject, _paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode)
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
    [DynamicData(nameof(GetValidateExchangeRateArgArrayList), DynamicDataSourceType.Method)]
    public void ValidateExchangeRate_invalidArg_decimal_arg_string_throws_ArgumentOutOfRangeException(Enum measureUnit, decimal exchangeRate)
    {
        // Arrange
        _paramName = RandomParams.GetRandomParamName();
        SetBaseMeasurementChild(measureUnit, null, null);

        // Act
        void attempt() => _baseMeasurement.ValidateExchangeRate(exchangeRate, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentOutOfRangeException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateExchangeRate_validArg_decimal_arg_string_returns()
    {
        // Arrange
        SetBaseMeasurementChild(_measureUnit, null, null);
        decimal exchangeRate = GetExchangeRate(_measureUnit, null);

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
        SetBaseMeasurementChild(_measureUnit, null, null);
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _baseMeasurement.ValidateMeasureUnit(null, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void ValidateMeasureUnit_invalidArg_Enum_arg_string_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetBaseMeasurementChild(_measureUnit, null, null);
        Enum context = RandomParams.GetRandomValidMeasureUnit(_measureUnit);

        // Act
        void attempt() => _baseMeasurement.ValidateMeasureUnit(context, _paramName);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurementValidateMeasureUnitValidArgsArrayList), DynamicDataSourceType.Method)]
    public void ValidateMeasureUnit_validArg_Enum_arg_string_returns(Enum measureUnit, Enum context)
    {
        // Arrange
        SetBaseMeasurementChild(measureUnit, null, null);

        // Act
        void validator() => _baseMeasurement.ValidateMeasureUnit(context, _paramName);

        // Assert
        Assert.IsTrue(Returned(validator));
    }
    #endregion
    #endregion

    //#region Static methods

    //#endregion
    #endregion

    #region Private methods
    private void SetBaseMeasurementChild(Enum measureUnit, IBaseMeasurementFactory factory, string measureUnitName)
    {
        _baseMeasurement = new(RootObject, _paramName)
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
    private static IEnumerable<object[]> GetBaseMeasurementEqualsObjectArgArrayList()
    {
        return DynamicDataSource.GetBaseMeasurementEqualsObjectArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurementEqualsBaseMeasurementArgArrayList()
    {
        return DynamicDataSource.GetBaseMeasurementEqualsBaseMeasurementArgArrayList();
    }

    private static IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    {
        return DynamicDataSource.GetExchangeRateCollectionArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurementIsExchangeableToArgArrayList()
    {
        return DynamicDataSource.GetBaseMeasurementIsExchangeableToArgArrayList();
    }

    private static IEnumerable<object[]> GetValidateExchangeRateArgArrayList()
    {
        return DynamicDataSource.GetValidateExchangeRateArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurementValidateMeasureUnitValidArgsArrayList()
    {
        return DynamicDataSource.GetBaseMeasurementValidateMeasureUnitValidArgsArrayList();
    }
    #endregion
    #endregion
}