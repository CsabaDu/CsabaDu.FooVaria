namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<ICircle, IRectangle>, IConcreteFactory
{
    IRectangleFactory TangentShapeFactory { get; init; }

    ICircle Create(IExtent radius);
}
