namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<ICircle, IRectangle>
{
    IRectangleFactory TangentShapeFactory { get; init; }

    ICircle Create(IExtent radius);
}
