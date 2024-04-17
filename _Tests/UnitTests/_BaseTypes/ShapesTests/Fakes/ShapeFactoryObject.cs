namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal sealed class ShapeFactoryObject : IShapeFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        IShapeComponent shapeComponent = GetShapeComponentQuantifiableObject(measureUnit, defaultQuantity);

        return CreateShape(shapeComponent);
    }

    public IShape CreateShape(params IShapeComponent[] shapeComponents)
    {
        return GetShapeChild(shapeComponents[0], this);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return GetSpreadChild(spreadMeasure, this);
    }

    public ISpreadMeasure CreateSpreadMeasure(Enum measureUnit, double quantity)
    {
        return GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
    }
}
