namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface ILimitable
{
    bool? FitsIn(ILimiter? limiter);
}
