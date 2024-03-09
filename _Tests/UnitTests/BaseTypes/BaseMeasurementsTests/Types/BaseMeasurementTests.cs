using CsabaDu.FooVaria.BaseTypes.Common;

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
        _paramName = string.Empty;
        _baseMeasurement = new(_rootObject, _paramName);

        _measureUnit = RandomParams.GetRandomMeasureUnit();
        _paramName = RandomParams.GetRandomParamName();
        _baseMeasurement.Returns = new()
        {
            GetFactory = new BaseMeasurementFactoryObject(),
            GetName = _paramName,
            GetBaseMeasureUnit = _measureUnit,
        };

        _measureUnitType = _measureUnit.GetType();
        _measureUnitCode = GetMeasureUnitCode(_measureUnitType);
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
    #region Constructors
    #region BaseMeasurement(IRootObject)
    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_nullArg_IBaseMeasurementFactory_throws_ArgumentNullException()
    {
        // Arrange
        _rootObject = null;
        _paramName = RandomParams.GetRandomParamName();

        // Act
        void attempt() => _ = new BaseMeasurementChild(_rootObject, _paramName);

        // Assert
        var ex = Assert.ThrowsException<ArgumentNullException>(attempt);
        Assert.AreEqual(_paramName, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void BaseMeasurement_validArg_IBaseMeasurementFactory_creates()
    {
        // Arrange
        _rootObject = SampleParams.rootObject;
        _paramName = string.Empty;

        // Act
        var actual = new BaseMeasurementChild(_rootObject, _paramName);

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