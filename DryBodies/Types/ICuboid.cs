namespace CsabaDu.FooVaria.DryBodies.Types
{
    public interface ICuboid : IDryBody<ICuboid, IRectangle>, IRectangularShape<ICuboid, ICylinder>, IHorizontalRotation<ICuboid>, ISpatialRotation<ICuboid>
    {
        ICuboidFactory Factory { get; init; }

        ICuboid GetCuboid(IExtent length, IExtent width, IExtent height);
        IRectangle GetVerticalProjection(ComparisonCode comparisonCode);
    }
}
