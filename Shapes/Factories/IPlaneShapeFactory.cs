namespace CsabaDu.FooVaria.Shapes.Factories;

public interface IPlaneShapeFactory : IShapeFactory, ISurfaceFactory
{
    IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
}
