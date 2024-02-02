namespace CsabaDu.FooVaria.PlaneShapes.Factories;

public interface IRectangleFactory : IPlaneShapeFactory, IRectangularShapeFactory<IRectangle, ICircle>/*, IFactory<IRectangle>*/
{
    IRectangle Create(IExtent length, IExtent width);
}
