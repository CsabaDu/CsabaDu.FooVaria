namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface ILimitable/* : IFit<IQuantifiable>*/
{
    bool? FitsIn(ILimiter? limiter);
}
