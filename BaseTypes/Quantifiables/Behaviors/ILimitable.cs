namespace CsabaDu.FooVaria.Quantifiables.Behaviors;

public interface ILimitable : IFit<IBaseMeasure>
{
    bool? FitsIn(ILimiter? limiter);
}
