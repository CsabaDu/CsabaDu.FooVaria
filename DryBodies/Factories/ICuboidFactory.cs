namespace CsabaDu.FooVaria.DryBodies.Factories;

public interface ICuboidFactory : IDryBodyFactory<ICuboid, IRectangle>, IRectangularShapeFactory<ICuboid, ICylinder>, IConcreteFactory
{
    ICylinderFactory TangentShapeFactory { get; init; }
    IRectangleFactory BaseFaceFactory { get; init; }

    ICuboid Create(IExtent length, IExtent width, IExtent height);
    IRectangle CreateBaseFace(IExtent length, IExtent width);
    IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode);
}
