namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface IRectangleFactory : IPlaneShapeFactory, IRectangularShapeFactory<IRectangle, ICircle>, IConcreteFactory
{
    ICircleFactory TangentShapeFactory { get; init; }

    IRectangle Create(IExtent length, IExtent width);
}
