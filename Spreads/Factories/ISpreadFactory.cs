namespace CsabaDu.FooVaria.Spreads.Factories
{
    public interface ISpreadFactory : IBaseSpreadFactory
    {
        public IMeasureFactory MeasureFactory { get; init; }

        ISpread Create(params IExtent[] shapeExtents);
    }

    public interface ISpreadFactory<T, in TSMeasure> : ISpreadFactory, IFactory<T>
        where T : class, ISpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
    {
        T Create(TSMeasure spreadMeasure);

        T? Create(IBaseSpread baseSpread);
    }

    public interface ISpreadFactory<T, in TSMeasure, in TEnum> : ISpreadFactory<T, TSMeasure>
        where T : class, ISpread
        where TSMeasure : class, IMeasure, ISpreadMeasure
        where TEnum : struct, Enum
    {
        T Create(TEnum measureUnit, double quantity);
    }
}
