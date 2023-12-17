using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Behaviors
{
    public interface IRectangularShape : ITangentShape, IHorizontalRotation, ILength, IWidth
    {
    }

    public interface IRectangularShape<out TSelf, out TTangent> : IRectangularShape, ITangentShape<TTangent> where TSelf : class, IShape, IRectangularShape where TTangent : class, IShape, ICircularShape
    {
        TTangent GetInnerTangentShape(ComparisonCode comparisonCode);
    }
}
