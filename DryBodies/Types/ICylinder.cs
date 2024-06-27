using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.DryBodies.Types
{
    public interface ICylinder : IDryBody<ICylinder, ICircle>, ICircularShape<ICylinder, ICuboid>, IConcreteFactory<ICylinderFactory>
    {
        //ICylinderFactory Factory { get; init; }

        ICylinder GetCylinder(IExtent radius, IExtent height);
        IRectangle GetVerticalProjection();
    }
}
