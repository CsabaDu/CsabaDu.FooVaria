namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICylinder : IDryBody<ICylinder, ICircle>, ICircularShape<ICylinder, ICuboid>/*, IProjection<ICircle>*/
    {
        ICylinder GetCylinder(IExtent radius, IExtent height);
        IRectangle GetVerticalProjection();
    }
}
