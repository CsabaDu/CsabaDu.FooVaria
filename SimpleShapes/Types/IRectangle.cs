namespace CsabaDu.FooVaria.SSimpleShapeshapes.Types
{
    public interface IRectangle : IPlaneShape, IRectangularShape<IRectangle, ICircle>, IHorizontalRotation<IRectangle>, ICommonBase<IRectangle>
    {
        IExtent Length { get; init; }
        IExtent Width { get; init; }

        IRectangle GetRectangle(IExtent length, IExtent width);
    }
}
