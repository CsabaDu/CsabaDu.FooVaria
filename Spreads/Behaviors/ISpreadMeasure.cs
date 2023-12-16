namespace CsabaDu.FooVaria.Spreads.Behaviors;

public interface ISpreadMeasure<in TSelf, out TEnum> : IMeasureUnit<TEnum>, ISpreadMeasure where TSelf : class, IMeasure, ISpreadMeasure where TEnum : struct, Enum
{
}

