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
        _factory = null;
        _paramName = null;
        _rootObject = SampleParams.rootObject;
        _measureUnit = RandomParams.GetRandomValidMeasureUnit();
        _baseMeasurement = new(_rootObject, _paramName)
        {
            Returns = new()
            {
                GetFactory = _factory,
                GetName = _paramName,
                GetBaseMeasureUnit = _measureUnit,
            }
        };

        _measureUnitType = _measureUnit.GetType();
        _measureUnitCode = GetMeasureUnitCode(_measureUnitType);
    }

    [TestCleanup]
    public void CleanupBaseMeasurementTests()
    {
        RestoreConstantExchangeRates();
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
    #region IEquatable<IBaseMeasurement>.Equals(IBaseMeasurement?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetBaseMeasurementEqualsObjectArgArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit)
    {
        // Arrange
        _baseMeasurement.Returns = new()
        {
            GetBaseMeasureUnit = measureUnit,
        };

        // Act
        var actual = _baseMeasurement.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region BaseMeasurement.Equals(object?)

    #endregion
    #endregion

    // IBaseMeasurement IBaseMeasurement.GetBaseMeasurement(Enum context)
    // IDictionary<object, decimal> IConstantExchangeRateCollection.GetConstantExchangeRateCollection()
    // decimal IExchangeRate.GetExchangeRate()
    // IDictionary<object, decimal> IExchangeRateCollection.GetExchangeRateCollection()
    // int BaseMeasurement.GetHashCode()
    // string INamed.GetName()
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum context)
    // decimal IProportional<IBaseMeasurement>.ProportionalTo(IBaseMeasurement other)
    // void IExchangeRate.ValidateExchangeRate(decimal exchangeRate, string paramName)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)

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
    #endregion
}