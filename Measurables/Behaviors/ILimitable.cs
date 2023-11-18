namespace CsabaDu.FooVaria.RateComponents.Behaviors;

public interface ILimitable : IFit<IRateComponent>
{
    bool? FitsIn(ILimit? limit);
}
