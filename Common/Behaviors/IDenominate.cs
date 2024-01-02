namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IDenominate
    {
        MeasureUnitTypeCode GetDenominatorMeasureUnitTypeCode();
    }

    public interface IDenominate<out TSelf, in TDenominator> : IDenominate/*, IMultiply<TSelf, TDenominator>*/
        where TSelf : class, IBaseMeasure
        where TDenominator : notnull
    {
        TSelf Denominate(TDenominator denominator);
    }
}
