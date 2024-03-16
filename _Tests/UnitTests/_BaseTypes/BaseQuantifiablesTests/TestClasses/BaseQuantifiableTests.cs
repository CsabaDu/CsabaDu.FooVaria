using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;
using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Enums;
using CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Statics;
using CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors;

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
    private LimitMode _limitMode;
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
    [DynamicData(nameof(GetFitsInArgsArrayList), DynamicDataSourceType.Method)]
    public void FitsIn_invalidArg_ILimiter_returns_null(Enum measureUnit, ILimiter limiter)
    {
        // Arrange
        SetBaseQuantifiableChild(measureUnit, null, _defaultQuantity);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.IsNull(actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void FitsIn_vidArg_ILimiter_returns_expected()
    {
        // Arrange
        SetBaseQuantifiableChild(_measureUnit, null, _defaultQuantity);
        _limitMode = RandomParams.GetRandomLimitMode();
        decimal otherQuantity = RandomParams.GetRandomDecimal();
        LimiterBaseQuantifiableOblect limiter = new(RootObject, null)
        {
            Return = new()
            {
                GetBaseMeasureUnit = RandomParams.GetRandomMeasureUnit(_measureUnitCode),
                GetDefaultQuantity = otherQuantity,
            },
            LimitMode = _limitMode,
        };
        bool? expected = _defaultQuantity.FitsIn(otherQuantity, _limitMode);

        // Act
        var actual = _baseQuantifiable.FitsIn(limiter);

        // Assert
        Assert.AreEqual(expected, actual);
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

    private static IEnumerable<object[]> GetFitsInArgsArrayList()
    {
        return DynamicDataSource.GetFitsInArgsArrayList();
    }

    #endregion
}
