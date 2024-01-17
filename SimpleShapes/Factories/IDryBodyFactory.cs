namespace CsabaDu.FooVaria.Shapes.Factories
{
    public interface IDryBodyFactory : ISimpleShapeFactory, IBodyFactory
    {
        IPlaneShapeFactory BaseFaceFactory { get; init; }

        IDryBody Create(IPlaneShape baseFace, IExtent height);
        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBodyFactory<out T, TBFace> : IDryBodyFactory
        where T : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        T Create(TBFace baseFace, IExtent height);
    }
}
