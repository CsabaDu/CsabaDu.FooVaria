namespace CsabaDu.FooVaria.DryBodies.Types
{
    public interface IDryBody : IBaseFace, IHeight, ISimpleShape, IBody, IProjection
    {
        IVolume Volume { get; init; }
        IExtent Height { get; init; }

        IDryBody GetDryBody(IPlaneShape baseFace, IExtent height);
        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBody<TSelf, TBFace> : IDryBody, IGetNew<TSelf>
        where TSelf : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        TBFace BaseFace { get; init; }

        TSelf GetDryBody(TBFace baseFace, IExtent height);
    }
}
