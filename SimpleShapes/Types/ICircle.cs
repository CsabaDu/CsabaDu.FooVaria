namespace CsabaDu.FooVaria.SimpleShapes.Types
{
    public interface ICircle : IPlaneShape, ICircularShape<ICircle, IRectangle>, ICommonBase<ICircle>
    {
        IExtent Radius { get; init; }

        ICircle GetCircle(IExtent radius);
    }
}
