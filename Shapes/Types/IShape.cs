namespace CsabaDu.FooVaria.Shapes.Types;

public interface IShape : IBaseShape, IShapeExtents, IDiagonal
{
    IShape GetShape(ExtentUnit measureUnit);
    IShape GetShape(params IExtent[] shapeExtents);
    IShape GetShape(IShape other);
}
