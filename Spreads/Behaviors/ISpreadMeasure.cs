namespace CsabaDu.FooVaria.Spreads.Behaviors;

public interface ISpreadMeasure<in T, out U> : IQuantity<double>, IMeasureUnit<U>, ISpreadMeasure where T : class, IMeasure, ISpreadMeasure where U : struct, Enum
{
}

