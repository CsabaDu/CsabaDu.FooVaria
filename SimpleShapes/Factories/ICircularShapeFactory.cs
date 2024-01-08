namespace CsabaDu.FooVaria.SimpleShapes.Factories
{
    public interface ICircularShapeFactory : ITangentShapeFactory
    {
    }

    public interface ICircularShapeFactory<T, out TTangent> : IShapeFactory<T, TTangent>, ICircularShapeFactory
        where T : class,  ISimpleShape, ICircularShape
        where TTangent : class, ISimpleShape, IRectangularShape
    {
        TTangent CreateInnerTangentShape(T circularShape, IExtent tangentRectangleSide);
    }
}
