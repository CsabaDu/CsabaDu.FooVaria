using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICylinder : IDryBody, ICircularShape, IProjection<ICircle>
    {
        IRectangle GetVerticalProjection();
    }
}
