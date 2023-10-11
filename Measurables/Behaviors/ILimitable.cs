namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface ILimitable : IFit<IBaseMeasure>
{
    bool? FitsIn(ILimit? limit);
}
