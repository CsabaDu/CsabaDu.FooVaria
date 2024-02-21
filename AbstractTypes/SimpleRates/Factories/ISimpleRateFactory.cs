namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Factories
{
    public interface ISimpleRateFactory : IBaseRateFactory/*, IFactory<IBaseRate>*/
    {
        ISimpleRate CreateSimpleRate(MeasureUnitCode numeratorCode, decimal defaultQuantity, MeasureUnitCode denominatorCode);
    }
}