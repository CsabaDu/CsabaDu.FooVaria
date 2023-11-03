using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
    }

    public interface IRectangularShape<T, U> : IRectangularShape, ITangentShape<U> where T : class, IShape, ITangentShape where U : class, IShape, ITangentShape
    {
        U GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
