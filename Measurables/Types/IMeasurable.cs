namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurable : IBaseMeasurable, IQuantityType
{
    IMeasurable GetDefault();

    IMeasurable GetMeasurable(IMeasurable other);
}
