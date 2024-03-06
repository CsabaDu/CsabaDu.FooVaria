namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Factories;

public interface IBaseRateFactory : IBaseQuantifiableFactory/*, IFactory<IBaseRate>*/
{
    IBaseRate CreateBaseRate(IQuantifiable numerator, Enum denominator);
    IBaseRate CreateBaseRate(IQuantifiable numerator, IMeasurable denominator);
    IBaseRate CreateBaseRate(IQuantifiable numerator, IQuantifiable denominator);
}

    //IBaseRate CreateBaseRate(IQuantifiable numerator, MeasureUnitCode denominatorCode);
    //IBaseRate CreateBaseRate(IBaseRate baseRate);