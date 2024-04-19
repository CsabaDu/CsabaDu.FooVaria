using static CsabaDu.FooVaria.BaseTypes.Measurables.Types.Implementations.Measurable;
using CsabaDu.FooVaria.Tests.TestHelpers.Params;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
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
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)

    #endregion

    #region Private fields
    private ShapeChild _shape;
    private IShapeComponent _shapeComponent;

    #region Readonly fields
    private readonly DataFields Fields = new();
    #endregion

    #region Static fields
    private static DynamicDataSource DynamicDataSource;
    #endregion
    #endregion

    #region Initialize
    [ClassInitialize]
    public static void ClassInitialize(TestContext context)
    {
        DynamicDataSource = new();
    }

    [TestInitialize]
    public void TestInitialize()
    {
        Fields.measureUnit = Fields.RandomParams.GetRandomSpreadMeasureUnit();
        Fields.measureUnitCode = GetMeasureUnitCode(Fields.measureUnit);
        Fields.defaultQuantity = Fields.RandomParams.GetRandomPositiveDecimal();
        _shapeComponent = GetShapeComponentQuantifiableObject(Fields.measureUnit, Fields.defaultQuantity);
    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Test methods
    #region int CompareTo
    #region IComparable<IShape>.CompareTo(IShape?)

    #endregion
    #endregion

    // bool IEquatable<IShape>.Equals(IShape?)
    // bool IEqualityComparer<IShape>.Equals(IShape?, IShape?)

    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IShape>.FitsIn(IShape?, LimitMode?)

    // int Shape.GetHashCode()
    // int IEqualityComparer<IShape>.GetHashCode(IShape)

    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()

    // IShape IShape.GetShape()
    // IShape? IShape.GetShape(params IShapeComponent[] shapeComponents)

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
    //    private void SetShapeChild(Enum measureUnit, ValueType quantity, IShapeFactory factory = null)
    //    {
    //        _spreadMeasure = SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);

    //        SetShapeChild(_spreadMeasure, factory);
    //    }

    //    private void SetShapeChild(ISpreadMeasure spreadMeasure, IShapeFactory factory = null)
    //    {
    //        _shape = ShapeChild.GetShapeChild(spreadMeasure, factory);
    //    }

    //    private void SetShapeChild()
    //    {
    //        SetShapeChild(_spreadMeasure);
    //    }

    //    private void SetCompleteShapeChild()
    //    {
    //        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();

    //        SetShapeChild(_spreadMeasure, new ShapeFactoryObject(), Fields.rateComponentCode);
    //    }

    //    private ShapeChild GetShapeChild(IShapeFactory factory = null)
    //    {
    //        return ShapeChild.GetShapeChild(_spreadMeasure, factory);
    //    }


    //    private ShapeChild GetCompleteShapeChild(RateComponentCode? rateComponentCode = null)
    //    {
    //        return ShapeChild.GetShapeChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), rateComponentCode ?? RateComponentCode.Numerator);
    //    }

    //    private ISpreadMeasure GetSpreadMeasureBaseMeasureObject()
    //    {
    //        return SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(Fields.measureUnit, Fields.quantity);
    //    }

    //    #region DynamicDataSource
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
    //    #endregion
    #endregion
}