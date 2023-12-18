using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IPlaneShapeFactory : IShapeFactory
    {
        IPlaneShape Create(IDryBody dryBody, ShapeExtentTypeCode perpendicular);
    }
}
