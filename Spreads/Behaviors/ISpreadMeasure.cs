namespace CsabaDu.FooVaria.Spreads.Behaviors;

public interface ISpreadMeasure<in TSelf, TEnum> : IMeasureUnit<TEnum>, ISpreadMeasure
    where TSelf : class, IMeasure, ISpreadMeasure
    where TEnum : struct, Enum
{
}

