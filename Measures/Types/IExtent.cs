namespace CsabaDu.FooVaria.Measures.Types;

public interface IExtent : IMeasure<IExtent, double, ExtentUnit>, IConvertMeasure<IExtent, IDistance>, ISimpleShapeComponent
{
}
