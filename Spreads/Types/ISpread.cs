using CsabaDu.FooVaria.Spreads.Behaviors;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseSpread, IShapeExtents/*, ISpreadMeasure*//*, IFit<ISpread>*/
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(IBaseSpread baseSpread);
    }

    public interface ISpread<T, U, V> : ISpread, ISpreadMeasure<U, V>/*, IFit<T>*//*, IProportional<T>*/ where T : class, ISpread where U : class, IMeasure, ISpreadMeasure where V : struct, Enum
    {
        U SpreadMeasure { get; init; }

        T GetSpread(U spreadMeasure);
        T GetSpread(V measureUnit);
    }
}
