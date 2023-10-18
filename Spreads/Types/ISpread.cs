using CsabaDu.FooVaria.Spreads.Behaviors;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseSpread, IShapeExtents, ISpreadMeasure
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(IBaseSpread baseSppread);
    }

    public interface ISpread<T, U> : ISpread, ISpreadMeasure<T, U>, IFit<T>, IProportional<T, U>, IExchange<T, U> where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
    {
        T SpreadMeasure { get; init; }

        ISpread<T, U> GetSpread(T spreadMeasure);
        ISpread<T, U> GetSpread(U measureUnit);
    }
}
