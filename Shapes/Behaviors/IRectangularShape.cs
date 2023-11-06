using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
    }

    public interface IRectangularShape<out T, out U> : IRectangularShape, ITangentShape<U> where T : class, IShape, IRectangularShape where U : class, IShape, ICircularShape
    {
        U GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
