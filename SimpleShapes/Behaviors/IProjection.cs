namespace CsabaDu.FooVaria.Shapes.Behaviors;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentCode perpendicular);
}
