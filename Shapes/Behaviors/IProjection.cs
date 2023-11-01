using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IProjection
    {
        IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
    }

    public interface IProjection<out T> : IProjection where T : IPlaneShape, ITangentShape
    {
        T GetHorizontalProjection();
    }
}
