namespace CsabaDu.FooVaria.SimpleShapes.Factories;

public interface IPlaneShapeFactory : IShapeFactory, ISurfaceFactory
{
    IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
}
