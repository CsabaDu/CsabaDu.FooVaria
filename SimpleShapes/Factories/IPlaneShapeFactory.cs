namespace CsabaDu.FooVaria.SimpleShapes.Factories;

public interface IPlaneShapeFactory : ISimpleShapeFactory, ISurfaceFactory
{
    IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular);
}
