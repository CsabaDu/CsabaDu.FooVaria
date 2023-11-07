using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Types;

public interface IShape : IBaseShape, IShapeExtents, IDimensions, IDiagonal
{
    IShape GetShape(ExtentUnit measureUnit);
    IShape GetShape(IEnumerable<IExtent> shapeExtentList);

    ISpreadFactory GetSpreadFactory();
    ITangentShapeFactory GetTangentShapeFactory();
}
