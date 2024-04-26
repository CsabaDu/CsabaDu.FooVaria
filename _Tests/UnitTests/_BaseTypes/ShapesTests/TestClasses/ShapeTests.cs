using CsabaDu.FooVaria.BaseTypes.BaseMeasures.Types;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // Shape(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? _other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? _other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? _other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // double IQuantity<double>.GetQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // ISpreadMeasure? ISpread.GetSpreadMeasure(IQuantifiable? quantifiable)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? _other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? defaultQuantity, string paramName)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)

    #endregion

    #region Private fields
    private ShapeChild _shape;
    private ShapeChild _other;
    private IShapeComponent _shapeComponent;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource = new();
    private const string DisplayName = nameof(GetDisplayName);
    #endregion
    #endregion

    #region Initialize
    [TestInitialize]
    public void TestInitialize()
    {
        Fields.measureUnit = Fields.RandomParams.GetRandomSpreadMeasureUnit();
        Fields.measureUnitCode = Fields.GetMeasureUnitCode();
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal();
        _shapeComponent = GetShapeComponentQuantifiableObject(Fields);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
        _other = null;
    }

    public static string GetDisplayName(MethodInfo methodInfo, object[] args)
    {
        return CommonDynamicDataSource.GetDisplayName(methodInfo, args);
    }
    #endregion

    #region Test methods
    #region int CompareTo
    #region IComparable<IShape>.CompareTo(IShape?)
    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_nullArg_IShape_returns_expected()
    {
        // Arrange
        SetShapeChild();

        IShape other = null;
        const int expected = 1;

        // Act
        var actual = _shape.CompareTo(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_invalidArg_IShape_throws_InvalidEnumArgumentException()
    {
        // Arrange
        SetCompleteShapeChild();

        Fields.measureUnitCode = SampleParams.GetOtherSpreadMeasureUnitCode(Fields.measureUnitCode);
        Fields.measureUnit = Fields.RandomParams.GetRandomMeasureUnit(Fields.measureUnitCode);
        _other = GetCompleteShapeChild(Fields);

        // Act
        void attempt() => _ = _shape.CompareTo(_other);

        // Assert
        var ex = Assert.ThrowsException<InvalidEnumArgumentException>(attempt);
        Assert.AreEqual(ParamNames.other, ex.ParamName);
    }

    [TestMethod, TestCategory("UnitTest")]
    public void CompareTo_validArg_IQuantifiable_returns_expected()
    {
        // Arrange
        SetCompleteShapeChild();

        Fields.measureUnit = Fields.RandomParams.GetRandomSameTypeValidMeasureUnit(Fields.measureUnit);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal(Fields.defaultQuantity);
        _other = GetCompleteShapeChild(Fields);
        int expected = _shape.GetDefaultQuantity().CompareTo(_other.GetDefaultQuantity());

        // Act
        var actual = _shape.CompareTo(_other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    #region bool Equals
    #region virtual IEquatable<IShape>.Equals(IShape?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArg), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_arg_IQuantifiable_returns_expected(string testCase, Enum measureUnit, decimal defaultQuantity, bool expected, IShape other)
    {
        // Arrange
        SetShapeChild(measureUnit, defaultQuantity);

        // Act
        var actual = _shape.Equals(other);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion

    #region IEqualityComparer<IShape>.Equals(IShape?, IShape?)
    [TestMethod, TestCategory("UnitTest")]
    [DynamicData(nameof(GetEqualsArgs), DynamicDataSourceType.Method, DynamicDataDisplayName = DisplayName)]
    public void Equals_args_IBaseMeasure_IBaseMeasure_returns_expected(string testCase, IShape left, bool expected, IShape right)
    {
        // Arrange
        SetCompleteShapeChild();

        // Act
        var actual = _shape.Equals(left, right);

        // Assert
        Assert.AreEqual(expected, actual);
    }
    #endregion
    #endregion

    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IShape>.FitsIn(IShape?, LimitMode?)

    // int Shape.GetHashCode()
    // int IEqualityComparer<IShape>.GetHashCode(IShape)

    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()

    // IShape IShape.GetBaseShape()
    // IShape? IShape.GetBaseShape(params IShapeComponent[] shapeComponents)

    // int IShapeComponentCount.GetShapeComponentCount()

    // IEnumerable<IShapeComponent> IShapeComponents.GetShapeComponents()

    // IShapeComponent? IShapeComponents.GetValidShapeComponent(IQuantifiable?)

    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode)

    // bool IMeasureUnitCodes.IsValidMeasureUnitCode(MeasureUnitCode)

    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable?, string)

    // void IShape.ValidateShapeComponent(IBaseQuantifiable?, string)

    #endregion

    //    //    //#region Static methods

    //    //    //#endregion

    #region Private methods
    private void SetShapeChild(Enum measureUnit, decimal defaultQuantity, IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(measureUnit, defaultQuantity, baseShape, factory);
    }

    private void SetShapeChild(IShapeComponent shapeComponent, IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(shapeComponent, baseShape, factory);
    }

    private void SetShapeChild(IShape baseShape = null, IShapeFactory factory = null)
    {
        _shape = GetShapeChild(Fields, baseShape, factory);
    }

    private void SetCompleteShapeChild(IShapeFactory factory = null)
    {
        _shape = GetCompleteShapeChild(Fields, factory);
    }

    #region DynamicDataSource
    private static IEnumerable<object[]> GetEqualsArg()
    {
        return DynamicDataSource.GetEqualsArg();
    }

    private static IEnumerable<object[]> GetEqualsArgs()
    {
        return DynamicDataSource.GetEqualsArgs();
    }
    
    //    private static IEnumerable<object[]> GetIsExchangeableToArgs()
    //    {
    //        return DynamicDataSource.GetIsExchangeableToArgs();
    //    }

    //    private static IEnumerable<object[]> GetGetSpreadMeasureArgs()
    //    {
    //        return DynamicDataSource.GetGetSpreadMeasureArgs();
    //    }

    //    private static IEnumerable<object[]> GetValidateSpreadMeasureArgs()
    //    {
    //        return DynamicDataSource.GetValidateSpreadMeasureArgs();
    //    }
    #endregion
    #endregion
}