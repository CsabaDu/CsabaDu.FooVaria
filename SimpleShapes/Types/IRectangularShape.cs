namespace CsabaDu.FooVaria.SimpleShapes.Types;

public interface IRectangularShape<out TSelf, out TTangent> : IRectangularShape, ISimpleShape<TTangent>, IHorizontalRotation, ILength, IWidth
    where TSelf : class, ISimpleShape, IRectangularShape
    where TTangent : class, ISimpleShape, ICircularShape
{
    TTangent GetInnerTangentShape(ComparisonCode comparisonCode);
}
