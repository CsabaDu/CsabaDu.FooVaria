using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Types;

public interface IShape : IBaseShape, IShapeExtents, IDimensions, IDiagonal
{
    IShape GetShape(ExtentUnit measureUnit);
    IShape GetShape(params IExtent[] shapeExtents);

    ISpreadFactory GetSpreadFactory();
    ITangentShapeFactory GetTangentShapeFactory();
}
