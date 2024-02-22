namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<ICircle, IRectangle>
{
    IRectangleFactory RectangleFactory { get; init; }

    ICircle Create(IExtent radius);
}
