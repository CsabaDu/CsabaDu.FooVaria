using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories
{
    public interface ISpreadFactory : IFactory
    {
        public IMeasureFactory MeasureFactory { get; init; }
    }

    public interface ISpreadFactory<T, U> : ISpreadFactory, IFactory<T> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
    {
        T Create(U spreadMeasure);
        T Create(params IExtent[] shapeExtents);
    }
}