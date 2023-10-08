using CsabaDu.FooVaria.Measurables.Factories.Implementations;
using CsabaDu.FooVaria.Measurables.Statics;
using CsabaDu.FooVaria.Measurables.Types.Implementations;

namespace CsabaDu.FooVaria.Tests.MeasurablesTests.UnitTests.Types;

[TestClass, TestCategory("UnitTest")]
public class MeasurementTests
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
        Measurement = Factory.Create(MeasureUnit);
    }
    #endregion

    #region Private fields
    private IMeasurement Measurement;
    private IMeasurementFactory Factory;
    private Enum MeasureUnit;
    private RandomParams RandomParams;

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Constructors
    #region Measurement(IMeasurementFactory, Enum)
    [TestMethod, TestCategory("UnitTest")]
    public void Measurement_nullArg_IMeasurementFactory_arg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        IMeasurementFactory factory = null;
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new Measurement(factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurement_validArg_IMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        Enum measureUnit = null;

        // Act
        void attempt() => _ = new Measurement(Factory, measureUnit);

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
        void attempt() => _ = new Measurement(Factory, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void Measurement_validArgs_IMeasurementFactory_Enum_createsInstance()
    {
        // Arrange
        Enum esxpected_MeasureUnit = RandomParams.GetRandomValidMeasureUnit();
        decimal esxpected_ExchnageRate = BaseMeasurement.GetExchangeRateCollection().FirstOrDefault(x => x.Key.Equals(esxpected_MeasureUnit)).Value;
        // Act
        var actual = new Measurement(Factory, esxpected_MeasureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IMeasurement));
        Assert.AreEqual(esxpected_MeasureUnit, actual.MeasureUnit);
        Assert.AreEqual(esxpected_ExchnageRate, actual.ExchangeRate);
    }
    #endregion
    #endregion

    #region ArrayList methods
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion
}
