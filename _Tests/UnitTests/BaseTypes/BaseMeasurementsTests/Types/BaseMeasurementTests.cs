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
        _factory = new();
        _baseMeasurement = new(_factory);

        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _baseMeasurement.GetMeasureUnit_returns = _measureUnit;

        _measureUnitType = _measureUnit.GetType();
        _measureUnitCode = GetMeasureUnitCode(_measureUnitType);
        
        _name = RandomParams.GetRandomParamName();
        _baseMeasurement.GetName_returns = _name;
    }
    #endregion

    #region Private fields
    private MeasureUnitCode _measureUnitCode;
    private BaseMeasurementChild _baseMeasurement;
    private BaseMeasurementFactoryClass _factory;
    private Enum _measureUnit;
    Type _measureUnitType;
    string _name;

    private string _paramName;

    #region Readonly fields
    private readonly RandomParams RandomParams = new();
    #endregion

    #region Static fields
    private static DynamicDataSources DynamicDataSources;
    #endregion
    #endregion

    #region Test methods
    #region Constructors
    #region BaseMeasurement(IBaseMeasurementFactory)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_nullArg_IBaseMeasurementFactory_throws_ArgumentNullException()
    {
        // Arrange
        _factory = null;

        // Act
        void attempt() => _ = new BaseMeasurementChild(_factory);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(ParamNames.factory, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArg_IBaseMeasurementFactory_creates()
    {
        // Arrange
        // Act
        var actual = new BaseMeasurementChild(_factory);

        // Assert
        Assert.IsInstanceOfType(actual, typeof(IBaseMeasurement));
    }
    #endregion
    #endregion

    //#region CompareTo
    //#region CompareTo(IBaseMeasurement?)

    //#endregion
    //#endregion

    #endregion

    #region DynamicDataSources
    private static IEnumerable<object[]> GetInvalidEnumMeasureUnitArgArrayList()
    {
        return DynamicDataSources.GetInvalidEnumMeasureUnitArgArrayList();
    }
    #endregion
}