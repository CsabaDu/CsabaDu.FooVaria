namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentTypeCode perpendicular);
}
