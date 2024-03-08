namespace CsabaDu.FooVaria.Measures.Types;

public interface IWeight : IMeasure<IWeight, double, WeightUnit>, IConvertMeasure<IWeight, IVolume>;
