using CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;
using CsabaDu.FooVaria.Tests.TestHelpers.Params;
using CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;
using System.ComponentModel;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.TestClasses;

[TestClass, TestCategory("UnitTest")]
public sealed class ShapeTests
{
    #region Tested in parent classes' tests

    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // int IComparable<IShape>.CompareTo(IShape? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool IEquatable<IShape>.Equals(IShape? other)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // bool? IFit<IShape>.FitsIn(IShape? other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantity()
    // IFactory ICommonBase.GetFactory()
    // MeasureUnitCode IMeasureUnitCode.GetMeasureUnitCode()
    // IEnumerable<MeasureUnitCode> IMeasureUnitCodes.GetMeasureUnitCodes()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // double IQuantity<double>.GetQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // IShape IShape.GetShape()
    // IShape? IShape.GetShape(params IShapeComponent[] shapeComponents)
    // int IShapeComponentCount.GetShapeComponentCount()
    // IEnumerable<IShapeComponent> IShapeComponents.GetShapeComponents()
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // ISpreadMeasure? ISpread.GetSpreadMeasure(IQuantifiable? quantifiable)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // IShapeComponent? IShapeComponents.GetValidShapeComponent(IBaseQuantifiable? shapeComponent)
    // bool IMeasureUnitCode.HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // bool IMeasureUnitCodes.IsValidMeasureUnitCode(MeasureUnitCode measureUnitCode)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, string paramName)
    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IBaseQuantifiable? baseQuantifiable, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IShape.ValidateShapeComponent(IBaseQuantifiable? shapeComponent, string paramName)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? spreadMeasure, string paramName)

    #endregion

    #region Private fields
    private ShapeChild _shape;

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

    }

    [TestCleanup]
    public void TestCleanup()
    {
        Fields.paramName = null;
    }
    #endregion

    #region Test methods



    #endregion
    //    //    //#region Static methods

    //    //    //#endregion
    //    //    #endregion

    #region Private methods
    //    private void SetSpreadChild(Enum measureUnit, ValueType quantity, ISpreadFactory factory = null, RateComponentCode? rateComponentCode = null)
    //    {
    //        _spread = SpreadChild.GetSpreadChild(measureUnit, quantity, factory, rateComponentCode);
    //    }

    //    private void SetSpreadChild(ISpreadMeasure spreadMeasure, ISpreadFactory factory = null)
    //    {
    //        _spread = SpreadChild.GetSpreadChild(spreadMeasure, factory);
    //    }


    //    private void SetSpreadChild()
    //    {
    //        SetSpreadChild(Fields.measureUnit, Fields.quantity);
    //    }

    //    private void SetCompleteSpreadChild()
    //    {
    //        Fields.rateComponentCode = Fields.RandomParams.GetRandomRateComponentCode();

    //        SetSpreadChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), Fields.rateComponentCode);
    //    }

    //    private SpreadChild GetSpreadChild(ISpreadFactory factory = null)
    //    {
    //        return SpreadChild.GetSpreadChild(Fields.measureUnit, Fields.quantity, factory);
    //    }


    //    private SpreadChild GetCompleteSpreadChild(RateComponentCode? rateComponentCode = null)
    //    {
    //        return SpreadChild.GetSpreadChild(Fields.measureUnit, Fields.quantity, new SpreadFactoryObject(), rateComponentCode ?? RateComponentCode.Numerator);
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