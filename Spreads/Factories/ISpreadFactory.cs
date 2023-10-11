using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories
{
    public interface ISpreadFactory : IFactory
    {
    }

    public interface ISpreadFactory<T, U> : IFactory<T> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
    {
        public IMeasureFactory MeasureFactory { get; init; }

        T Create(U spreadMeasure);
        T Create(params IExtent[] shapeExtents);
    }
}