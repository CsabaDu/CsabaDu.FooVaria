namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;

public interface IBaseRateFactory : IBaseQuantifiableFactory/*, ICreateNew<IBaseRate>*/
{
    IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator);
    IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator);
    IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator);
}
