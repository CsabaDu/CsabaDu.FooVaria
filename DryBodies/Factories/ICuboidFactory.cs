namespace CsabaDu.FooVaria.DryBodies.Factories;

public interface ICuboidFactory : IDryBodyFactory<ICuboid, IRectangle>, IRectangularShapeFactory<ICuboid, ICylinder>
{
    ICylinderFactory CylinderFactory { get; init; }

    ICuboid Create(IExtent length, IExtent width, IExtent height);
    IRectangle CreateBaseFace(IExtent length, IExtent width);
    IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode);
}
