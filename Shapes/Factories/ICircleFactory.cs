using CsabaDu.FooVaria.Shapes.Types;

namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface ICircleFactory : IPlaneShapeFactory, ICircularShapeFactory<ICircle, IRectangle>/*, IFactory<ICircle>*/
    {
        ICircle Create(IExtent radius);
    }
}
