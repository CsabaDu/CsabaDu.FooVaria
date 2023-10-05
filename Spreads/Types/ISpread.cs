using CsabaDu.FooVaria.Common.Behaviors;
using CsabaDu.FooVaria.Measurables.Markers;
using CsabaDu.FooVaria.Measurables.Types;
using CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

namespace CsabaDu.FooVaria.Spreads.Types
{
    public interface ISpread
    {
        ISpread GetSpread(params IExtent[] shapeExtents);
        ISpread GetSpread(ISpreadMeasure spreadMeasure);
        ISpread GetSpread(ISpread spread);
        ISpread GetSpread(Enum measureUnit);
    }

    public interface ISpread<T> : ISpread, ISpreadMeasures<T>, IFit<T>, IProportional<T, Enum> where T : class, IMeasure, ISpreadMeasure
    {
        ISpread<T> GetSpread(T spreadMeasure);
    }
}
