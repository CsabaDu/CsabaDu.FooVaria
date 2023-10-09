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
        RandomParams = new();
        MeasureUnit = RandomParams.GetRandomValidMeasureUnit();
        Factory = new MeasurementFactory();
        BaseMeasurement = new BaseMeasurementChild(Factory, MeasureUnit);
    }
    #endregion

    #region Private fields
    private IBaseMeasurement BaseMeasurement;
    private IMeasurementFactory Factory;
    private Enum MeasureUnit;
    private RandomParams RandomParams;

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
        Factory = null;
        MeasureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(Factory, MeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurement_validArg_IMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        MeasureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(Factory, MeasureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void Measurement_validArg_IMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = new BaseMeasurementChild(Factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArgs_IMeasurementFactory_Enum_createsInstance()
    {
        // Arrange
        MeasureUnitTypeCode measureUnitTypeCode = RandomParams.GetRandomMeasureUnitTypeCode();
        MeasureUnit = RandomParams.GetRandomMeasureUnit(measureUnitTypeCode);

        // Act
        var actual = new BaseMeasurementChild(Factory, MeasureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
        Assert.AreEqual(measureUnitTypeCode, actual.MeasureUnitTypeCode);
    }
    #endregion
    #endregion

    #region GetConstantExchangeRateCollection
    #region GetConstantExchangeRateCollection()
    [TestMethod, TestCategory("UnitTest")]
    public void GetConstantExchangeRateCollection_returnsExpected()
    {
        // Arrange
        BaseMeasurement.RestoreConstantExchangeRates();
        IDictionary<object, decimal> expected = GetExchangeRateCollection();

        // Act
        var actual = BaseMeasurement.GetConstantExchangeRateCollection();

        // Assert
        Assert.IsTrue(expected.SequenceEqual(actual));
    }
    #endregion
    #endregion


    #endregion

    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion

}

