namespace CsabaDu.FooVaria.AbstractTypes.SimpleRates.Types
{
    public interface ISimpleRate : IBaseRate/*, ICommonBase<IBaseRate>*/
    {
        //Enum? this[RateComponentCode rateComponentCode] { get; }

        MeasureUnitCode NumeratorCode { get; init; }
        MeasureUnitCode DenominatorCode { get; init; }
        decimal DefaultQuantity { get; init; }

        //TSelf GetBaseRate(IBaseRate baseRate);
    }
}
