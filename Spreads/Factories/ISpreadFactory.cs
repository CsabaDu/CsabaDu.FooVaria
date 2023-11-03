using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories
{
    public interface ISpreadFactory : IBaseSpreadFactory
    {
        ISpread Create(params IExtent[] shapeExtents);
    }

    public interface ISpreadFactory<T, U> : ISpreadFactory, IFactory<T> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
    {
        public IMeasureFactory MeasureFactory { get; init; }

        T Create(U spreadMeasure);
    }
}
