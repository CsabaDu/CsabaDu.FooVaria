namespace CsabaDu.FooVaria.Shapes.Factories;

public interface IPlaneShapeFactory : IShapeFactory, ISurfaceFactory
{
    IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular);
}
