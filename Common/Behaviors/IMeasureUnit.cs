namespace CsabaDu.FooVaria.Common.Behaviors
{
    public interface IMeasureUnit
    {
    }

    public interface IMeasureUnit<out T> : IMeasureUnit where T : Enum
    {
        T GetMeasureUnit();
    }
}
