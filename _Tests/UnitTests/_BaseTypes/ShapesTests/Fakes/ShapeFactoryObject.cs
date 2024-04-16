using CsabaDu.FooVaria.BaseTypes.Shapes.Factories;
using CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasuresTests.Fakes;
using CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.SpreadsTests.Fakes;

namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal sealed class ShapeFactoryObject : IShapeFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        ShapeComponentObject shapeComponentObject = new()
        {
            MeasureUnitCode = measureUnitCode,
            DefaultQuantity = defaultQuantity,
        };
        return CreateShape(shapeComponentObject);
    }

    public IShape CreateShape(params IShapeComponent[] shapeComponents)
    {
        throw new NotImplementedException();
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
