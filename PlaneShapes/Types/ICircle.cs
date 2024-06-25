using CsabaDu.FooVaria.BaseTypes.Common.Behaviors;

namespace CsabaDu.FooVaria.PlaneShapes.Types
{
    public interface ICircle : IPlaneShape, ICircularShape<ICircle, IRectangle>, IGetNew<ICircle>, IGetFactory<ICircleFactory>
    {
        IExtent Radius { get; init; }
        //ICircleFactory Factory { get; init; }

        ICircle GetCircle(IExtent radius);
    }
}
