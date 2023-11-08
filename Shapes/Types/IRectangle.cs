namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IRectangle : IPlaneShape, IRectangularShape<IRectangle, ICircle>, IHorizontalRotation<IRectangle>
    {
        IExtent Length { get; init; }
        IExtent Width { get; init; }

        IRectangle GetRectangle(IExtent length, IExtent width);
    }
}
