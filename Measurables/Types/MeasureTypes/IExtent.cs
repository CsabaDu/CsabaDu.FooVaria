namespace CsabaDu.FooVaria.Measurables.Types.MeasureTypes;

public interface IExtent : IMeasure, IMeasure<IExtent, double, ExtentUnit>, IConvertMeasure<IExtent, IDistance>
{
}
