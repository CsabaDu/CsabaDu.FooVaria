namespace CsabaDu.FooVaria.Spreads.Behaviors
{
    public interface ISpreadMeasures
    {
        IMeasure GetSpreadMeasure();

        void ValidateShapeExtents(IEnumerable<IExtent> shapeExtents);
    }

    public interface ISpreadMeasures<T, in U> : IQuantifyable<double>, ISpreadMeasures where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        T GetSpreadMeasure(params IExtent[] shapeExtents);
        T GetSpreadMeasure(U measureUnit);

        void ValidateSpreadMeasure(IMeasure spreadMeasure);
    }
}
