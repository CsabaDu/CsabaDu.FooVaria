namespace CsabaDu.FooVaria.SimpleShapes.Types
{
    public interface ICylinder : IDryBody<ICylinder, ICircle>, ICircularShape<ICylinder, ICuboid>, ICommonBase<ICylinder>
    {
        ICylinder GetCylinder(IExtent radius, IExtent height);
        IRectangle GetVerticalProjection();
    }
}
