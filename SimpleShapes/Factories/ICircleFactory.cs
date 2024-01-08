namespace CsabaDu.FooVaria.SimpleShapes.Factories;

public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<ICircle, IRectangle>/*, IFactory<ICircle>*/
{
    ICircle Create(IExtent radius);
}
