namespace CsabaDu.FooVaria.SimpleShapes.Factories
{
    public interface IRectangularShapeFactory : ITangentShapeFactory
    {
    }

    public interface IRectangularShapeFactory<T, out TTangent> : IShapeFactory<T, TTangent>, IRectangularShapeFactory
        where T : class, ISimpleShape, IRectangularShape
        where TTangent : class, ISimpleShape, ICircularShape
    {
        TTangent CreateInnerTangentShape(T rectangularShape, ComparisonCode comparisonCode);
    }
}
