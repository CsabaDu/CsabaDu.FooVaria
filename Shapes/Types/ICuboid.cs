using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICuboid : IDryBody, IRectangularShape, IProjection<IRectangle>, IHorizontalRotation<ICuboid>, ISpatialRotation<ICuboid>
    {
        IRectangle GetVerticalProjection(ComparisonCode comparisonCode);
    }
}
