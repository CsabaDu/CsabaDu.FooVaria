namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors
{
    public interface IMeasureUnit
    {
        Enum GetMeasureUnit();
        MeasureUnitCode GetMeasureUnitCode();

    }

    public interface IMeasureUnit<TEnum> : IMeasureUnit
        where TEnum : Enum
    {
        TEnum GetMeasureUnit(IMeasureUnit<TEnum>? other);
    }
}
