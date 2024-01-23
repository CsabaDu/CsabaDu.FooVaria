namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface ILimitable
{
    bool? FitsIn(ILimiter? limiter);
}
