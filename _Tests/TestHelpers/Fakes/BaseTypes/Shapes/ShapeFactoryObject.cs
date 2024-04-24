﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Shapes;

public sealed class ShapeFactoryObject : IShapeFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        IShapeComponent shapeComponent = ShapeComponentQuantifiableObject.GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

        return CreateShape(shapeComponent);
    }

    public IShape CreateShape(params IShapeComponent[] shapeComponents)
    {
        return ShapeChild.GetShapeChild(shapeComponents[0], this);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return SpreadChild.GetSpreadChild(spreadMeasure, this);
    }

    public ISpreadMeasure CreateSpreadMeasure(Enum measureUnit, double quantity)
    {
        return SpreadMeasureBaseMeasureObject.GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
    }
}
