using CsabaDu.FooVaria.Measurables.Factories.Implementations;
using static CsabaDu.FooVaria.Measurables.Types.Implementations.BaseMeasurement;

namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class BaseMeasurementTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeMeasurableTestsClass(TestContext context)
    {
        DynamicDataSources = new();
    }

    [TestInitialize]
    public void InitializeMeasurableTests()
    {
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        factory = new MeasurementFactory();
        baseMeasurement = new BaseMeasurementChild(factory, measureUnit);
        baseMeasurement.RestoreConstantExchangeRates();
    }
    #endregion

    #region Private fields
    private IBaseMeasurement baseMeasurement;
    private IMeasurementFactory factory;
    private Enum measureUnit;
    string name;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurement(IBaseMeasurementFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_nullArg_IBaseMeasurementFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        factory = null;
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArg_IMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void BaseMeasurement_validArg_IMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = new BaseMeasurementChild(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArgs_IMeasurementFactory_Enum_creates()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        measureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurementChild(factory, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion
    #endregion

    #region GetConstantExchangeRateCollection
    #region GetConstantExchangeRateCollection()
    [TestMethod, TestCategory("UnitTest")]
    public void GetConstantExchangeRateCollection_returns_expected()
    {
        // Arrange
        baseMeasurement.RestoreConstantExchangeRates();
        IDictionary<object, decimal> expected = GetExchangeRateCollection();

        // Act
        var actual = baseMeasurement.GetConstantExchangeRateCollection();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion

    #region GetCustomName
    #region GetCustomName(Enum)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidGetCustomNameArgArrayList), DynamicDataSourceType.Method)]
    public void GetCustomName_nullArg_Enum_returns_null(Enum measureUnit)
    {
        // Arrange
        // Act
        var actual = baseMeasurement.GetCustomName(measureUnit);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void GetCustomName_validArg_Enum_returns_expected()
    {
        // Arrange
        name = Guid.NewGuid().ToString();
        baseMeasurement.SetCustomName(measureUnit, name);

        // Act
        var actual = baseMeasurement.GetCustomName(measureUnit);

        // Assert
        Assert.AreEqual(name, actual);
    }
    #endregion
    #endregion

    #endregion

    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }

    private static IEnumerable<object[]> GetInvalidGetCustomNameArgArrayList()
    {
        return DynamicDataSources.GetInvalidGetCustomNameArgArrayList();
    }
    #endregion

}

