using CsabaDu.FooVaria.Spreads.Factories;

namespace CsabaDu.FooVaria.Shapes.Types;

public interface IShape : IBaseShape, IShapeExtents, IDimensions, IDiagonal
{
    IShape GetShape(ExtentUnit measureUnit);
    IShape GetShape(params IExtent[] shapeExtents);
    IShape GetShape(IEnumerable<IExtent> shapeExtentList);
    //IShape GetShape(IShape other);

    ISpreadFactory GetSpreadFactory();
}
