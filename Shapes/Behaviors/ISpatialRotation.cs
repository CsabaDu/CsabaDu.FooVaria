using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface ISpatialRotation
    {
        //IRectangle GetComparedVerticalFace(ComparisonCode comparisonCode);
    }

    public interface ISpatialRotation<TSelf> : ISpatialRotation
        where TSelf : class, IDryBody, IRectangularShape
    {
        TSelf RotateSpatially();
        //TSelf RotateSpatiallyWith(TSelf other);
    }
}
