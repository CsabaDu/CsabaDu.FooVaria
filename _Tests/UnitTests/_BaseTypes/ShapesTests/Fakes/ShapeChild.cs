namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal sealed class ShapeChild(IRootObject rootObject, string paramName) : Shape(rootObject, paramName)
{
    #region Members

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

    #region Test helpers
    private static DataFields Fields = new();
    public ShapeReturn Return { private get; set; } = new();
    private ISpreadMeasure SpreadMeasure { get; set; }

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
            if (shapeComponent is ISpreadMeasure spreadMeasure) return spreadMeasure;

            if (shapeComponent is IQuantifiable quantifiable)
            {
                Enum measureUnit = quantifiable.GetBaseMeasureUnit();
                ValueType quantity = quantifiable.GetBaseQuantity();

                return GetSpreadMeasureObject(measureUnit, quantity);
            }

            MeasureUnitCode measureUnitCode = shapeComponent.GetMeasureUnitCode();
            decimal defaultQuantity = shapeComponent.GetDefaultQuantity();

            return GetSpreadMeasureObject(measureUnitCode, defaultQuantity);
        }
    }
    #endregion

    public override int CompareTo(IShape other)
    {
        return CompareTo(other as IQuantifiable);
    }

    public override bool? FitsIn(IShape other, LimitMode? limitMode)
    {
        return FitsIn(other as IQuantifiable, limitMode);
    }

    public override IFactory GetFactory() => Return.GetFactory;

    public override IShape GetShape() => this;

    public override IEnumerable<IShapeComponent> GetShapeComponents() => Return.GetShapeComponents;

    public override ISpreadMeasure GetSpreadMeasure() => SpreadMeasure;

    public override IShapeComponent GetValidShapeComponent(IBaseQuantifiable baseQuantifiable)
    {
        if (baseQuantifiable is IShapeComponent shapeComponent) return shapeComponent;
        throw new NotImplementedException();
    }

    public override bool TryExchangeTo(Enum context, [NotNullWhen(true)] out IQuantifiable exchanged)
    {
        return FakeMethods.TryExchange(this, getShapeChild, context, out exchanged);

        #region Local methods
        IQuantifiable getShapeChild()
        {
            decimal defaultQuantity = GetDefaultQuantity();
            ShapeComponentObject shapeComponentObject = GetShapeComponentObject(context, defaultQuantity);

            return GetShapeChild(shapeComponentObject, new ShapeFactoryObject());
        }
        #endregion
    }
}
