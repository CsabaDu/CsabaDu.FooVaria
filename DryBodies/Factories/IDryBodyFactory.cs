using CsabaDu.FooVaria.DryBodies.Types;
using CsabaDu.FooVaria.PlaneShapes.Factories;

namespace CsabaDu.FooVaria.DryBodies.Factories
{
    public interface IDryBodyFactory : ISimpleShapeFactory, IBodyFactory
    {
        IPlaneShapeFactory BaseFaceFactory { get; init; }

        IDryBody Create(IPlaneShape baseFace, IExtent height);
        IPlaneShape? CreateProjection(IDryBody dryBody, ShapeExtentCode perpendicular);

        IPlaneShapeFactory GetBaseFaceFactory();
    }

    public interface IDryBodyFactory<out T, TBFace> : IDryBodyFactory
        where T : class, IDryBody, ITangentShape
        where TBFace : IPlaneShape, ITangentShape
    {
        T Create(TBFace baseFace, IExtent height);
    }
}
