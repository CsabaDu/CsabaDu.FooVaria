using CsabaDu.FooVaria.Shapes.Behaviors;

namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IDryBody : IBaseFace, IHeight, IShape, IBody, IProjection
    {
        IVolume Volume { get; init; }
        IExtent Height { get; init; }

        IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBody<out T, U> : IDryBody where T : class, IDryBody, ITangentShape where U : IPlaneShape, ITangentShape
    {
        U BaseFace { get; init; }

        T GetDryBody(U baseFace, IExtent height);
    }
}
