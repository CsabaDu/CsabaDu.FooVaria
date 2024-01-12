namespace CsabaDu.FooVaria.Quantifiables.Behaviors
{
    public interface IDenominate
    {
        MeasureUnitCode GetDenominatorMeasureUnitCode();
    }

    public interface IDenominate<out TNumerator, in TDenominator> : IDenominate
        where TNumerator : class, IBaseMeasure
        where TDenominator : notnull
    {
        TNumerator Denominate(TDenominator denominator);
    }
}
