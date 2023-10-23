using CsabaDu.FooVaria.Spreads.Behaviors;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread : IBaseSpread, IShapeExtents/*, ISpreadMeasure*//*, IFit<ISpread>*/
    {
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(IBaseSpread baseSpread);
    }

    public interface ISpread<T, U, W> : ISpread, ISpreadMeasure<U, W>/*, IFit<U>*//*, IProportional<U>*/ where T : class, ISpread where U : class, IMeasure, ISpreadMeasure where W : struct, Enum
    {
        U SpreadMeasure { get; init; }

        T GetSpread(U spreadMeasure);
        T GetSpread(W measureUnit);
    }
}
