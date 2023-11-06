using CsabaDu.FooVaria.Measurables.Factories;

namespace CsabaDu.FooVaria.Spreads.Factories
{
    public interface ISpreadFactory : IBaseSpreadFactory
    {
        public IMeasureFactory MeasureFactory { get; init; }

        ISpread Create(params IExtent[] shapeExtents);
    }

    public interface ISpreadFactory<T, in U> : ISpreadFactory, IFactory<T> where T : class, ISpread where U : class, IMeasure, ISpreadMeasure
    {
        T Create(U spreadMeasure);
    }
}
