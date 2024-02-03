namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface ICircularShapeFactory<T, out TTangent> : IShapeFactory<T, TTangent>, ICircularShapeFactory
        where T : class,  IShape, ICircularShape
        where TTangent : class, IShape, IRectangularShape
    {
        TTangent CreateInnerTangentShape(T circularShape, IExtent tangentRectangleSide);
    }
}
