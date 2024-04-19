namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.ShapesTests.Fakes;

internal sealed class ShapeComponentShapeObject(IRootObject rootObject, string paramName) : ShapeChild(rootObject, paramName), IShapeComponent
{
    internal static ShapeComponentShapeObject GetShapeComponentShapeObject(ISpreadMeasure spreadMeasure, IShapeFactory factory = null)
    {
        ShapeComponentSpreadMeasureObject shapeComponent = GetShapeComponentSpreadMeasureObject(spreadMeasure);

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetFactory = factory,
                GetShapeComponents = [shapeComponent],
            },
            SpreadMeasure = spreadMeasure.GetSpreadMeasure(),
        };
    }
}
