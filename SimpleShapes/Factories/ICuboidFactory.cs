namespace CsabaDu.FooVaria.SimpleShapes.Factories;

public interface ICuboidFactory : IDryBodyFactory<ICuboid, IRectangle>, IRectangularShapeFactory<ICuboid, ICylinder>/*, IFactory<ICuboid>*/
{
    ICuboid Create(IExtent length, IExtent width, IExtent height);
    IRectangle CreateBaseFace(IExtent length, IExtent width);
    IRectangle CreateVerticalProjection(ICuboid cuboid, ComparisonCode comparisonCode);
}
