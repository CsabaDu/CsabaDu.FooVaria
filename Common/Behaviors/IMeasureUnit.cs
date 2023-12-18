namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IMeasureUnit
    {
    }

    public interface IMeasureUnit<out TEnum> : IMeasureUnit
        where TEnum : Enum
    {
        TEnum GetMeasureUnit();
    }
}
