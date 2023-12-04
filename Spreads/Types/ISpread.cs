using CsabaDu.FooVaria.Spreads.Behaviors;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseSpread, IShapeExtents
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(IBaseSpread baseSpread);
    }

    public interface ISpread<out T, U> : ISpread where T : class, ISpread where U : class, IMeasure<U, double>, ISpreadMeasure
    {
        U SpreadMeasure { get; init; }

        T GetSpread(U spreadMeasure);
    }

    public interface ISpread<out T, U, W> : ISpread<T, U>, ISpreadMeasure<U, W> where T : class, ISpread where U : class, IMeasure<U, double, W>, ISpreadMeasure<U, W> where W : struct, Enum
    {
        T GetSpread(W measureUnit);
        T GetSpread(W measureUnit, double quantity);
    }
}
