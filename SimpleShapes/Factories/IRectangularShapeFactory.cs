namespace CsabaDu.FooVaria.SimpleShapes.Factories
{
    public interface IRectangularShapeFactory<T, out TTangent> : ISimpleShapeFactory<T, TTangent>, IRectangularShapeFactory
        where T : class, ISimpleShape, IRectangularShape
        where TTangent : class, ISimpleShape, ICircularShape
    {
        TTangent CreateInnerTangentShape(T rectangularShape, ComparisonCode comparisonCode);
    }
}
