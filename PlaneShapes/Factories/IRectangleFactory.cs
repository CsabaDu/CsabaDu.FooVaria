namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface IRectangleFactory : IPlaneShapeFactory, IRectangularShapeFactory<IRectangle, ICircle>
{
    ICircleFactory CircleFactory { get; init; }

    IRectangle Create(IExtent length, IExtent width);
}
