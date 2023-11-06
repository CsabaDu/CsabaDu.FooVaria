using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IProjection
    {
        IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
        IExtent GetShapeExtent(IPlaneShape projection, IVolume volume);
    }

    //public interface IProjection<out T> : IProjection where T : IPlaneShape, ITangentShape
    //{
    //    //T GetHorizontalProjection();
    //}
}
