﻿namespace CsabaDu.FooVaria.BaseTypes.Shapes.Types.Implementations;

public abstract class ComplexShape : Shape, IComplexShape
{
    protected ComplexShape(IShape other) : base(other)
    {
    }

    protected ComplexShape(IShapeFactory factory, IShape shape) : base(factory, shape)
    {
    }

    protected ComplexShape(ISpreadFactory factory, MeasureUnitCode measureUnitCode, params IShapeComponent[] shapeComponents) : base(factory, measureUnitCode, shapeComponents)
    {
    }
}
