namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IRectangularShapeFactory<T, out TTangent> : IShapeFactory<T, TTangent>, IRectangularShapeFactory
        where T : class, IShape, IRectangularShape
        where TTangent : class, IShape, ICircularShape
    {
        TTangent CreateInnerTangentShape(T rectangularShape, ComparisonCode comparisonCode);
    }
}
