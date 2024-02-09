namespace CsabaDu.FooVaria.DryBodies.Behaviors;

public interface IProjection
{
    IPlaneShape GetProjection(SimpleShapeExtentCode perpendicular);
}
