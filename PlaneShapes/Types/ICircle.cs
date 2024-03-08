namespace CsabaDu.FooVaria.PlaneShapes.Types
{
    public interface ICircle : IPlaneShape, ICircularShape<ICircle, IRectangle>, ICommonBase<ICircle>
    {
        IExtent Radius { get; init; }
        ICircleFactory Factory { get; init; }

        ICircle GetCircle(IExtent radius);
    }
}
