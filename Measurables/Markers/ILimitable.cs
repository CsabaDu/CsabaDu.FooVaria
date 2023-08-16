namespace CsabaDu.FooVaria.Measurables.Markers;

public interface ILimitable : IFit<IBaseMeasure>
{
    bool? FitsIn(ILimit? limit);
}
