namespace CsabaDu.FooVaria.Spreads.Behaviors;

public interface ISpreadMeasure<out T, in U> : IQuantifyable<double>, ISpreadMeasure where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
{
    T GetSpreadMeasure(U measureUnit);

    void ValidateSpreadMeasure(IMeasure spreadMeasure);
}
