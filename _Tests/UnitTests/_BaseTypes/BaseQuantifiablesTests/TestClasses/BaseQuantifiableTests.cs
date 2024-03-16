using CsabaDu.FooVaria.BaseTypes.BaseMeasurements.Factories;
using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Enums;
using Moq;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseQuantifiablesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class BaseQuantifiableTests
{
    #region Initialize
    [ClassInitialize]
    public static void InitializeBaseQuantifiableTestsClass(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void InitializeBaseQuantifiableTests()
    {
        _measureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode);
        _measureUnitCode = GetMeasureUnitCode(_measureUnit);
        _measureUnitType = _measureUnit.GetType();
        _defaultQuantity = RandomParams.GetRandomDecimal();
    }

    [TestCleanup]
    public void CleanupBaseQuantifiableTests()
    {
        _paramName = null;
    }
    #endregion

    #region Private fields
    private BaseQuantifiableChild _baseQuantifiable;
    private decimal _defaultQuantity;
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

    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)

    #endregion

    #region bool Equals
    #region BaseQuantifiable.Equals(object?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgsArrayList), DynamicDataSourceType.Method)]
    public void Equals_arg_object_returns_expected(bool expected, object obj, Enum measureUnit, decimal defaultQuantity)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, null, defaultQuantity);

        // Act
        var actual = _baseQuantifiable.Equals(obj);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool? FitsIn
    #region ILimitable.FitsIn(ILimiter)
    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_arg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild(_measureUnit, null, _defaultQuantity);
        Mock<ILimiter> limiter = new Mock<ILimiter>();
        LimitMode? limitMode = limiter.Object.GetLimitMode();

        // Act
        var actual = _baseQuantifiable.Equals(limiter);

        //// Assert
        //Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    // decimal IDefaultQuantity.GetDefaultQuantity()
    // int Measurable.GetHashCode()
    // void IBaseQuantifiable.ValidateQuantity(ValueType quantity, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(IBaseQuantifiable baseQuantifiable, string paramName)

    #endregion

    private void SetBaseQuantifiableChild(Enum measureUnit, IBaseMeasurementFactory factory, decimal defaultQuantity)
    {
        _baseQuantifiable = new(RootObject, _paramName)
        {
            Return = new()
            {
                GetBaseMeasureUnit = measureUnit,
                GetFactory = factory,
                GetDefaultQuantity = defaultQuantity,
            }
        };
    }

    #region DynamicDataSource

    private static IEnumerable<object[]> GetEqualsArgsArrayList()
    {
        return DynamicDataSource.GetEqualsArgsArrayList();
    }

    #endregion
}
