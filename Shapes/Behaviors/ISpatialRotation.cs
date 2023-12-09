using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ISpatialRotation
    {
        //IRectangle GetComparedVerticalFace(ComparisonCode comparisonCode);
    }

    public interface ISpatialRotation<T> : ISpatialRotation where T : class, IDryBody, IRectangularShape
    {
        T RotateSpatially();
        //TNum RotateSpatiallyWith(IDryBody other);
    }
}
