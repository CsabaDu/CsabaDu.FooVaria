namespace CsabaDu.FooVaria.Measurables.Types;

public interface IMeasurement : IMeasurable, IRateComponent, IProportional<IMeasurement, Enum>
{
    object MeasureUnit { get; init; }
    decimal ExchangeRate { get; init; }

    IMeasurement GetMeasurement(Enum measureUnit, decimal? exchangeRate = null);
    IMeasurement GetMeasurement(IMeasurement? other = null);
}
