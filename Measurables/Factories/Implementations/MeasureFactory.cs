using CsabaDu.FooVaria.Measurables.Types.Implementations.MeasureTypes;

namespace CsabaDu.FooVaria.Measurables.Factories.Implementations;

public sealed class MeasureFactory : BaseMeasureFactory, IMeasureFactory
{
    #region Constructors
    public MeasureFactory(IMeasurementFactory measurementFactory) : base(measurementFactory)
    {
    }
    #endregion

    #region Properties
    public override RateComponentCode RateComponentCode => RateComponentCode.Numerator;
    #endregion

    #region Public methods
    public IMeasure Create(ValueType quantity, Enum measureUnit)
    {
        return CreateMeasure(quantity, measureUnit);
    }

    public IMeasure Create(ValueType quantity, string name)
    {
        return CreateMeasure(quantity, name);
    }

    public IMeasure Create(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        return CreateCustomMeasure(quantity, measureUnit, exchangeRate, customName);
    }

    public IMeasure Create(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        return CreateCustomMeasure(quantity, customName, measureUnitTypeCode, exchangeRate);
    }

    public IMeasure Create(ValueType quantity, IMeasurement measurement)
    {
        return CreateMeasure(quantity, measurement);
    }

    public IMeasure Create(IBaseMeasure baseMeasure)
    {
        return CreateMeasure(this, baseMeasure);
    }

    public IMeasure Create(IMeasure measure)
    {
        return CreateMeasure(measure);
    }
    #endregion

    #region Private methods
    private IMeasure CreateCustomMeasure(ValueType quantity, Enum measureUnit, decimal exchangeRate, string customName)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit, exchangeRate, customName);

        return CreateMeasure(quantity, measurement);
    }

    private IMeasure CreateMeasure(ValueType quantity, IMeasurement measurement)
    {
        return CreateMeasure(this, quantity, measurement);
    }

    private IMeasure CreateCustomMeasure(ValueType quantity, string customName, MeasureUnitTypeCode measureUnitTypeCode, decimal exchangeRate)
    {
        IMeasurement measurement = MeasurementFactory.Create(customName, measureUnitTypeCode, exchangeRate);

        return CreateMeasure(quantity, measurement);
    }

    private IMeasure CreateMeasure(ValueType quantity, Enum measureUnit)
    {
        IMeasurement measurement = MeasurementFactory.Create(measureUnit);

        return CreateMeasure(quantity, measurement);
    }

    private IMeasure CreateMeasure(ValueType quantity, string name)
    {
        IMeasurement measurement = MeasurementFactory.Create(name);

        return CreateMeasure(quantity, measurement);
    }

    //private static MeasureUnitTypeCode GetMeasureUnitTypeCode(Enum measureUnit)
    //{
    //    if (!IsDefinedMeasureUnit(measureUnit)) throw InvalidMeasureUnitEnumArgumentException(measureUnit);

    //    string measureUnitTypeName = measureUnit.GetType().Name;

    //    return (MeasureUnitTypeCode)Enum.Parse(typeof(MeasureUnitTypeCode), measureUnitTypeName);
    //}
    #endregion
}
