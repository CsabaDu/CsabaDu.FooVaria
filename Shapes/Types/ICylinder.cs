using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICylinder : IDryBody<ICylinder, ICircle>, ICircularShape<ICylinder, ICuboid>/*, IProjection<ICircle>*/
    {
        IRectangle GetVerticalProjection();
    }
}
