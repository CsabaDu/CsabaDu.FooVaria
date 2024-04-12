namespace CsabaDu.FooVaria.Tests.UnitTests.BaseTypes.BaseMeasurementsTests.Fakes;

internal sealed class SpreadFactoryObject : ISpreadFactory
{
    //public IBaseMeasurement CreateBaseMeasurement(Enum context)
    //{
    //    Enum measureUnit = GetMeasureUnitElements(context, nameof(context)).MeasureUnit;

    //    return new BaseMeasurementChild(Fields.RootObject, Fields.paramName)
    //    {
    //        Return = new()
    //        {
    //            GetBaseMeasureUnit = measureUnit,
    //            GetFactory = this,
    //        }
    //    };
    //}

    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        BaseMeasureFactoryObject factory = new(RateComponentCode.Numerator);
        ISpreadMeasure spreadMeasure = factory.CreateQuantifiable(measureUnitCode, defaultQuantity) as ISpreadMeasure;

        return CreateSpread(spreadMeasure);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public ISpreadMeasure CreateSpreadMeasure(Enum measureUnit, ValueType quantity)
    {
        throw new NotImplementedException();
    }
}
