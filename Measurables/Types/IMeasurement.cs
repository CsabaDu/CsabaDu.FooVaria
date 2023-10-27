namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IBaseMeasurement, ICustomMeasurement, IRateComponentType<IMeasurement>, IProportional<IMeasurement>, IExchangeable<Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit);
    IMeasurement GetMeasurement(IMeasurement other);
    IMeasurement GetMeasurement(IBaseMeasure baseMeasure);
    IMeasurement GetMeasurement(string name);
}
