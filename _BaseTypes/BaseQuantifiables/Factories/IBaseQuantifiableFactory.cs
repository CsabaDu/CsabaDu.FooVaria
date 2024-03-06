namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Factories
{
    public interface IBaseQuantifiableFactory : IMeasurableFactory
    {
    }

    //public interface IQuantifiableFactory<T> : IBaseQuantifiableFactory
    //    where T : class, IBaseQuantifiable
    //{
    //    T CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity);
    //}
}
