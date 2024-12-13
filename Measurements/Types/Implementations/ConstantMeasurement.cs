namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

internal sealed class ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), IConstantMeasurement;
