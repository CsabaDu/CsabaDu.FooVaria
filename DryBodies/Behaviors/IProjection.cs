namespace CsabaDu.FooVaria.DryBodies.Behaviors;

public interface IProjection
{
    IPlaneShape GetProjection(ShapeExtentCode perpendicular);
}
