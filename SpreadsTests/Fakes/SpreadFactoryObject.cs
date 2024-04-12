internal sealed class SpreadFactoryObject : ISpreadFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        ISpreadMeasure spreadMeasure = CreateSpreadMeasure(measureUnit, defaultQuantity);

        return CreateSpread(spreadMeasure);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        throw new NotImplementedException();
    }

    public ISpreadMeasure CreateSpreadMeasure(Enum measureUnit, ValueType quantity)
    {
        return GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
    }
}
