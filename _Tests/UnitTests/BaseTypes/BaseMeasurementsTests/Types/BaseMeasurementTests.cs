namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Types;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseMeasurementTests
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
        measureUnitCode = Measurable.GetMeasureUnitCode(measureUnit);
        factoryObject = new BaseMeasurementFactoryClass();
        baseMeasurementObject = new BaseMeasurementChild(factoryObject, measureUnit);
    }
    #endregion

    #region Private fields
    private IBaseMeasurement baseMeasurementObject;
    private IBaseMeasurementFactory factoryObject;
    private Enum measureUnit;
    string name;
    MeasureUnitCode measureUnitCode;

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
        factoryObject = null;
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArg_IBaseMeasurementFactory_nullArg_Enum_throws_ArgumentNullException()
    {
        // Arrange
        measureUnit = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetInvalidEnumMeasureUnitArgArrayList), DynamicDataSourceType.Method)]
    public void BaseMeasurement_validArg_IBaseMeasurementFactory_invalidArg_Enum_throws_InvalidEnumArgumentException(Enum measureUnit)
    {
        // Arrange
        // Act
        void attempt() => _ = new BaseMeasurementChild(factoryObject, measureUnit);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.measureUnit, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArgs_IBaseMeasurementFactory_Enum_creates()
    {
        // Arrange
        measureUnit = RandomParams.GetRandomValidMeasureUnit();
        measureUnitCode = Measurable.GetMeasureUnitCode(measureUnit);

        // Act
        var actual = new BaseMeasurementChild(factoryObject, measureUnit);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
        Assert.AreEqual(measureUnitCode, actual.MeasureUnitCode);
    }
    #endregion
    #endregion

    #region CompareTo
    #region CompareTo(IBaseMeasurement?)

    #endregion
    #endregion

    #endregion

    #region DynamicDataSources
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion
}