namespace CsabaDu.FooVaria.Shapes.Factories;

public interface IPlaneShapeFactory : ISimpleShapeFactory, ISurfaceFactory
{
    IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular);
}
