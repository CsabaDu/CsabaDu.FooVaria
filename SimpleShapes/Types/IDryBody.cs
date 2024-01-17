namespace CsabaDu.FooVaria.Shapes.Types
{
    public interface IDryBody : IBaseFace, IHeight, IShape, IBody, IProjection
    {
        IVolume Volume { get; init; }
        IExtent Height { get; init; }

        IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBody<out TSelf, TBFace> : IDryBody
        where TSelf : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        TBFace BaseFace { get; init; }

        TSelf GetDryBody(TBFace baseFace, IExtent height);
    }
}
