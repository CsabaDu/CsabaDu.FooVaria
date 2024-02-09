using CsabaDu.FooVaria.Measures.Factories;

namespace CsabaDu.FooVaria.BulkSpreads.Factories
{
    public interface IBulkSpreadFactory : ISpreadFactory
    {
        public IMeasureFactory MeasureFactory { get; init; }

        IBulkSpread Create(params IExtent[] shapeExtents);
    }

    public interface IBulkSpreadFactory<T, in TSMeasure> : IBulkSpreadFactory, IFactory<T>
        where T : class, IBulkSpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
    {
        T Create(TSMeasure spreadMeasure);
        T? Create(ISpread spread);
    }

    public interface IBulkSpreadFactory<T, in TSMeasure, in TEnum> : IBulkSpreadFactory<T, TSMeasure>
        where T : class, IBulkSpread
        where TSMeasure : class, IMeasure, ISpreadMeasure, ILimitable
        where TEnum : struct, Enum
    {
        T Create(TEnum measureUnit, double quantity);
    }
}
