﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Shapes;

public class ShapeChild(IRootObject rootObject, string paramName) : Shape(rootObject, paramName)
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
    // Enum IMeasureUnit.GetBaseMeasureUnitReturnValue()
    // ValueType IQuantity.GetBaseQuantityReturnValue()
    // IShape IShape.GetBaseShapeValue()
    // decimal IDecimalQuantity.GetDecimalQuantity()
    // Enum IDefaultMeasureUnit.GetDefaultMeasureUnit()
    // IEnumerable<string> IDefaultMeasureUnit.GetDefaultMeasureUnitNames()
    // decimal IDefaultQuantity.GetDefaultQuantityReturnValue()
    // IFactory ICommonBase.GetFactoryReturnValue()
    // RateComponentCode IMeasureUnitCode.GetMeasureUnitCode()
    // IEnumerable<RateComponentCode> IMeasureUnitCodes.GetMeasureUnitCodes()
    // Type IMeasureUnit.GetMeasureUnitType()
    // IQuantifiable IQuantifiable.GetQuantifiable(RateComponentCode measureUnitCode, decimal defaultQuantity)
    // object IRound<IQuantifiable>.GetQuantity(RoundingMode roundingMode)
    // double IQuantity<double>.GetQuantity()
    // object IQuantity.GetQuantity(TypeCode quantityTypeCode)
    // TypeCode IQuantityType.GetQuantityTypeCode()
    // IShape? IShape.GetShape(params IShapeComponent[] shapeComponents)
    // int IShapeComponentCount.GetShapeComponentCount()
    // IEnumerable<IShapeComponent> IShapeComponents.GetShapeComponentsReturnValue()
    // ISpread ISpread.GetSpread(ISpreadMeasure numerator)
    // ISpreadMeasure? ISpread.GetSpreadMeasure(IQuantifiable? quantifiable)
    // ISpreadMeasure ISpreadMeasure.GetSpreadMeasure()
    // IShapeComponent? IShapeComponents.GetValidShapeComponent(IQuantifiable? quantifiable)
    // bool IMeasureUnitCode.HasMeasureUnitCode(RateComponentCode measureUnitCode)
    // bool IExchangeable<Enum>.IsExchangeableTo(Enum? context)
    // decimal IProportional<IQuantifiable>.ProportionalTo(IQuantifiable? other)
    // IQuantifiable IRound<IQuantifiable>.Round(RoundingMode roundingMode)
    // bool ITryExchange<IQuantifiable, Enum>.TryExchangeTo(Enum context, out IQuantifiable? exchanged)
    // void IDefaultMeasureUnit.ValidateMeasureUnit(Enum? measureUnit, string paramName)
    // void IMeasurable.ValidateMeasureUnitCode(IMeasurable? measurable, string paramName)
    // void IMeasureUnitCode.ValidateMeasureUnitCode(RateComponentCode measureUnitCode, string paramName)
    // void IMeasureUnitCodes.ValidateMeasureUnitCodes(IMeasureUnitCodes? measureUnitCodes, string paramName)
    // void IBaseQuantifiable.ValidateQuantity(ValueType? quantity, string paramName)
    // void IShape.ValidateShapeComponent(IQuantifiable? quantifiable, string paramName)
    // void ISpreadMeasure.ValidateSpreadMeasure(ISpreadMeasure? numerator, string paramName)

    #endregion

    #region Test helpers
    public ShapeReturnValues ReturnValues { private get; set; } = new();
    protected ISpreadMeasure SpreadMeasure { private get; set; }

    public static ShapeChild GetShapeChild(IShapeComponent shapeComponent, IShape baseShape = null, IShapeFactory factory = null)
    {
        return new(Fields.RootObject, Fields.paramName)
        {
            ReturnValues = GetReturn(shapeComponent, baseShape, factory),
            SpreadMeasure = GetSpreadMeasure(shapeComponent),
        };
    }

    public static ShapeChild GetShapeChild(Enum measureUnit, decimal defaultQuantity, IShape baseShape = null, IShapeFactory factory = null)
    {
        IShapeComponent shapeComponent = GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

        return GetShapeChild(shapeComponent, baseShape, factory);
    }

    public static ShapeChild GetCompleteShapeChild(Enum measureUnit, decimal defaultQuantity, IShapeFactory factory = null)
    {
        IShape baseShape = GetShapeChild(measureUnit, defaultQuantity);

        return GetShapeChild(measureUnit, defaultQuantity, baseShape, factory);
    }

    public static ShapeChild GetCompleteShapeChild(DataFields fields, IShapeFactory factory = null)
    {
        IShape baseShape = GetShapeChild(fields);

        return GetShapeChild(fields, baseShape, factory);
    }

    public static ShapeChild GetShapeChild(DataFields fields, IShape baseShape = null, IShapeFactory factory = null)
    {
        IShapeComponent shapeComponent = GetShapeComponentQuantifiableObject(fields);

        return GetShapeChild(shapeComponent, baseShape, factory);
    }

    protected static ShapeReturnValues GetReturn(IShapeComponent shapeComponent, IShape baseShape = null, IShapeFactory factory = null)
    {
        return new()
        {
            GetShapeComponentsReturnValue = [shapeComponent],
            GetFactoryReturnValue = factory,
            GetBaseShapeValue = baseShape,
        };
    }

    protected static ISpreadMeasure GetSpreadMeasure(IShapeComponent shapeComponent)
    {
        if (shapeComponent is not IQuantifiable quantifiable) return null;

        if (quantifiable is ISpreadMeasure spreadMeasure) return spreadMeasure.GetSpreadMeasure();

        Enum measureUnit = quantifiable.GetBaseMeasureUnit();
        ValueType quantity = quantifiable.GetBaseQuantity();

        return GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
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

    public override sealed IShape GetBaseShape() => ReturnValues.GetBaseShapeValue;

    public override sealed IFactory GetFactory() => ReturnValues.GetFactoryReturnValue;

    public override sealed IEnumerable<IShapeComponent> GetShapeComponents() => ReturnValues.GetShapeComponentsReturnValue;

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
            ShapeComponentQuantifiableObject shapeComponent = GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

            return GetShapeChild(shapeComponent);
        }
        #endregion
    }
}
