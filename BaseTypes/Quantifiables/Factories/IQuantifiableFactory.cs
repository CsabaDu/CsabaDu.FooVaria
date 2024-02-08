namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Factories
{
    public interface IQuantifiableFactory : IMeasurableFactory
    {
    }

    public interface IQuantifiableFactory<T> : IQuantifiableFactory
        where T : class, IQuantifiable
    {
        T CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
    }
}
