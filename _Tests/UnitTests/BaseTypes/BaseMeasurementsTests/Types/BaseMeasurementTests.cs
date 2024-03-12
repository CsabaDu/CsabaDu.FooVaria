using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Factories;
using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasurementTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseMeasurementTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeBaseMeasurementTests()
    {
        _rootObject = SampleParams.rootObject;
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode();
        _measureUnit = RandomParams.GetRandomValidMeasureUnit(_measureUnitCode);
        _measureUnitType = _measureUnit.GetType();
    }

    [TestCleanup]
    public void CleanupBaseMeasurementTests()
    {
        _factory = null;
        _paramName = null;
        _measureUnit = null;
        _baseMeasurement = null;

        if (ExchangeRateCollection.Count != ConstantExchangeRateCollection.Count)
        {
            RestoreConstantExchangeRates();
        }
    }
    #endregion

    #region Private fields
    private BaseMeasurementChild _baseMeasurement;
    private BaseMeasurementFactoryObject _factory;
    private Enum _measureUnit;
    private MeasureUnitCode _measureUnitCode;
    private Type _measureUnitType;
    private string _paramName;
    private IRootObject _rootObject;

    //private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    private void SetBaseMeasurement(Enum measureUnit, IBaseMeasurementFactory factory, string measureUnitName)
    {
        _baseMeasurement = new(_rootObject, _paramName)
        {
            Returns = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetName = measureUnitName,
            }
        };
    }
    #region Test methods

    #region Tested in parent classes' tests
    // BaseMeasurement(IRootObject rootObject, string measureUnitName)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string measureUnitName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string measureUnitName)
    #endregion

    #region int CompareTo
    #region IComparable<IBaseMeasurement>.CompareTo(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IBaseMeasurement_returns_expected()
    {
        // Arrange
        SetBaseMeasurement(_measureUnit, null, null);
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
        SetBaseMeasurement(_measureUnit, null, null);
        _measureUnitCode = RandomParams.GetRandomMeasureUnitCode(_measureUnitCode);
        IBaseMeasurement other = new BaseMeasurementChild(_rootObject, _paramName)
        {
            Returns = new()
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
        SetBaseMeasurement(_measureUnit, null, null);
        IBaseMeasurement other = new BaseMeasurementChild(_rootObject, _paramName)
        {
            Returns = new()
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
        SetBaseMeasurement(measureUnit, null, null);

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
        SetBaseMeasurement(measureUnit, null, null);

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
        SetBaseMeasurement(_measureUnit, new BaseMeasurementFactoryObject(), null);
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
        SetBaseMeasurement(_measureUnit, null, null);
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
        SetBaseMeasurement(_measureUnit, null, null);
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
        SetBaseMeasurement(measureUnit, null, null);
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

    // int BaseMeasurement.GetHashCode()
    // string INamed.GetName()
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string measureUnitName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string measureUnitName)
    #endregion

    #region DynamicDataSources
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurementEqualsObjectArgArrayList()
    {
        return DynamicDataSources.GetBaseMeasurementEqualsObjectArgArrayList();
    }

    private static IEnumerable<object[]> GetBaseMeasurementEqualsBaseMeasurementArgArrayList()
    {
        return DynamicDataSources.GetBaseMeasurementEqualsBaseMeasurementArgArrayList();
    }

    private static IEnumerable<object[]> GetExchangeRateCollectionArgArrayList()
    {
        return DynamicDataSources.GetExchangeRateCollectionArgArrayList();
    }

    #endregion
}