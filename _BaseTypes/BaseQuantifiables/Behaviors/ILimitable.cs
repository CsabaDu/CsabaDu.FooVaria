namespace CsabaDu.FooVaria.BaseTypes.BaseQuantifiables.Behaviors;

public interface ILimitable
{
    bool? FitsIn(ILimiter? limiter);
}
