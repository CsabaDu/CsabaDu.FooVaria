using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IShape : IBaseShape, IShapeExtents, IDiagonal
    {
        IShape GetShape(ExtentUnit measureUnit);
        IShape GetShape(params IExtent[] shapeExtents);
        IShape GetShape(IShape other);
    }

    public interface IPlaneShape : IShape, ISurface
    {
        IArea Area { get; }
    }

    public interface IDryBody : IShape, IBody
    {
        IVolume Volume { get; }
    }
}
