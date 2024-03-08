namespace CsabaDu.FooVaria.DryBodies.Factories;

public interface ICylinderFactory : IDryBodyFactory<ICylinder, ICircle>, ICircularShapeFactory<ICylinder, ICuboid>
{
    ICuboidFactory TangentShapeFactory { get; init; }
    ICircleFactory BaseFaceFactory { get; init; }

    ICylinder Create(IExtent radius, IExtent height);
    ICircle CreateBaseFace(IExtent radius);
    IRectangle CreateVerticalProjection(ICylinder cylinder);
}
