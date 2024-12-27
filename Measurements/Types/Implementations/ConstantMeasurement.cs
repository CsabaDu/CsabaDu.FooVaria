namespace CsabaDu.FooVaria.Measurements.Types.Implementations;

/// <summary>
/// Represents a constant measurement that is derived from the <see cref="Measurement"> class and implements the <see cref="IConstantMeasurement"/> interface.
/// </summary>
/// <param name="factory">The factory used to create <see cref="IMeasurement"/> instances.</param>
/// <param name="measureUnit">The unit of measurement as an enumeration.</param>
internal sealed class ConstantMeasurement(IMeasurementFactory factory, Enum measureUnit) : Measurement(factory, measureUnit), IConstantMeasurement;
