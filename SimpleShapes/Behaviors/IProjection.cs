namespace CsabaDu.FooVaria.SimpleShapes.Behaviors;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
}
