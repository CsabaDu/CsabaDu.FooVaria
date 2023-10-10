using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories.Implementations;

public abstract class SpreadFactory<T, U> : ISpreadFactory<T, U> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
{
    public SpreadFactory(IMeasureFactory measureFactory)
    {
        MeasureFactory = NullChecked(measureFactory, nameof(measureFactory));
    }

    public IMeasureFactory MeasureFactory { get; init; }

    public abstract T Create(U spreadMeasure);
    public abstract T Create(T other);
    public abstract T Create(params IExtent[] shapeExtents);
}

