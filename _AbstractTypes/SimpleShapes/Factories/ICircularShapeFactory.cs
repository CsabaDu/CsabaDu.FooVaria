namespace CsabaDu.FooVaria.AbstractTypes.SimpleShapes.Factories
{
    public interface ICircularShapeFactory<T, out TTangent> : ISimpleShapeFactory<T, TTangent>, ICircularShapeFactory
        where T : class,  ISimpleShape, ICircularShape
        where TTangent : class, ISimpleShape, IRectangularShape
    {
        TTangent CreateInnerTangentShape(T circularShape, IExtent tangentRectangleSide);
    }
}
