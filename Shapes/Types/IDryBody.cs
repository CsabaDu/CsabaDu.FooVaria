using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IDryBody : IBaseFace, IHeight, IShape, IBody, IProjection
    {
        IVolume Volume { get; init; }
        IExtent Height { get; init; }

        IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
    }

    public interface IDryBody<T, U> : IDryBody where T : class, IShape, ITangentShape where U : IShape, ITangentShape
    {
        U BaseFace { get; init; }

        T GetDryBody(U baseFace, IExtent height);
    }
}
