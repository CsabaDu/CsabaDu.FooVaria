namespace CsabaDu.FooVaria.BaseTypes.BaseRates.Behaviors
{
    public interface IDenominate
    {
        MeasureUnitCode GetDenominatorCode();
    }

    public interface IDenominate<out TNumerator, in TDenominator> : IDenominate
        where TNumerator : class, IQuantifiable
        where TDenominator : notnull
    {
        TNumerator Denominate(TDenominator denominator);

        void ValidateDenominator(TDenominator denominator, string paramName);
    }
}
