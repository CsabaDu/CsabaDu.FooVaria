using CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Quantifiables;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal class ShapeChild(IRootObject rootObject, string paramName) : Shape(rootObject, paramName)
{
    #region Members

    // Shape(IRootObject rootObject, string paramName)
    // int IComparable<IQuantifiable>.CompareTo(IQuantifiable? other)
    // int IComparable<IShape>.CompareTo(IShape? other)
    // bool IEquatable<IQuantifiable>.Equals(IQuantifiable? other)
    // bool IEquatable<IShape>.Equals(IShape? other)
    // bool? ILimitable.FitsIn(ILimiter? limiter)
    // bool? IFit<IQuantifiable>.FitsIn(IQuantifiable? other, LimitMode? limitMode)
    // bool? IFit<IShape>.FitsIn(IShape? other, LimitMode? limitMode)
    // Enum IMeasureUnit.GetBaseMeasureUnit()
    // ValueType IQuantity.GetBaseQuantity()
    // IShape IShape.GetBaseShape()
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
    // IShape? IShape.GetShape(params IShapeComponent[] shapeComponents)
    // int IShapeComponentCount.GetShapeComponentCount()
    // IEnumerable<IShapeComponent> IShapeComponents.GetShapeComponents()
    // ISpread ISpread.GetSpread(ISpreadMeasure spreadMeasure)
    // ISpreadMeasure? ISpread.GetSpreadMeasure(IQuantifiable? quantifiable)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // IShapeComponent? IShapeComponents.GetValidShapeComponent(IQuantifiable? quantifiable)
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

    #region Test helpers
    protected static DataFields Fields = new();
    public ShapeReturn Return { private get; set; } = new();
    protected ISpreadMeasure SpreadMeasure { get; set; }

    internal static ShapeChild GetShapeChild(IShapeComponent shapeComponent, IShapeFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactory = factory,
                GetShapeComponents = [shapeComponent],
            },
            SpreadMeasure = getSpreadMeasure(),
        };

        ISpreadMeasure getSpreadMeasure()
        {
            if (shapeComponent is not IQuantifiable quantifiable) return null;

            if (quantifiable is ISpreadMeasure spreadMeasure) return spreadMeasure.GetSpreadMeasure();

            Enum measureUnit = quantifiable.GetBaseMeasureUnit();
            ValueType quantity = quantifiable.GetBaseQuantity();

            return TestHelpers.Fakes.BaseTypes.BaseMeasures.SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
        }
    }
    #endregion

    public override sealed  int CompareTo(IShape other)
    {
        return CompareTo(other as IQuantifiable);
    }

    public override sealed  bool? FitsIn(IShape other, LimitMode? limitMode)
    {
        return FitsIn(other as IQuantifiable, limitMode);
    }

    public override sealed IFactory GetFactory() => Return.GetFactory;

    public override sealed IShape GetBaseShape() => this;

    public override sealed IEnumerable<IShapeComponent> GetShapeComponents() => Return.GetShapeComponents;

    public override sealed ISpreadMeasure GetSpreadMeasure() => SpreadMeasure;

    public override sealed IShapeComponent GetValidShapeComponent(IQuantifiable quantifiable)
    {
        if (quantifiable is not IShapeComponent shapeComponent) return null;

        return shapeComponent;
    }

    public override sealed bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        return FakeMethods.TryExchange(this, getShapeChild, context, out exchanged);

        #region Local methods
        IQuantifiable getShapeChild()
        {
            Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;
            decimal defaultQuantity = GetDefaultQuantity();
            ShapeComponentQuantifiableObject shapeComponent = ShapeComponentQuantifiableObject.GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

            return GetShapeChild(shapeComponent);
        }
        #endregion
    }
}
