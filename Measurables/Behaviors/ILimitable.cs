namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ILimitable : IFit<IRateComponent>
{
    bool? FitsIn(ILimit? limit);
}
