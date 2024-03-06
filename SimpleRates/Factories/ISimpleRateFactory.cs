namespace CsabaDu.FooVaria.SimpleRates.Factories;

public interface ISimpleRateFactory : IBaseRateFactory
{
    IMeasureFactory MeasureFactory { get; init; }

    ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
}
