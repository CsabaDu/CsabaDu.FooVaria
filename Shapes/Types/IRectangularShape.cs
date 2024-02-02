namespace CsabaDu.FooVaria.Shapes.Types;

public interface IRectangularShape<out TSelf, out TTangent> : IRectangularShape, IShape<TTangent>, IHorizontalRotation, ILength, IWidth
    where TSelf : class, IShape, IRectangularShape
    where TTangent : class, IShape, ICircularShape
{
    TTangent GetInnerTangentShape(ComparisonCode comparisonCode);
}
