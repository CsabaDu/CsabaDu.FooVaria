namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface ICuboid : IDryBody<ICuboid, IRectangle>, IRectangularShape<ICuboid, ICylinder>/*, IProjection<IRectangle>*/, IHorizontalRotation<ICuboid>, ISpatialRotation<ICuboid>
    {
        IRectangle GetVerticalProjection(ComparisonCode comparisonCode);
    }
}
