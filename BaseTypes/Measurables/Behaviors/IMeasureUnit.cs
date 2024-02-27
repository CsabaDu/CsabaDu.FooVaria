namespace CsabaDu.FooVaria.BaseTypes.Measurables.Behaviors
{
    public interface IMeasureUnit
    {
        Enum GetBaseMeasureUnit();
        Type GetMeasureUnitType();
    }

    public interface IMeasureUnit<TEnum> : IMeasureUnit
        where TEnum : Enum
    {
        TEnum GetMeasureUnit();
    }
}
