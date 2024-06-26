﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Spreads;

public sealed class SpreadFactoryObject : ISpreadFactory
{
    public IQuantifiable CreateQuantifiable(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        if (!measureUnitCode.IsSpreadMeasureUnitCode()) throw InvalidMeasureUnitEnumArgumentException(measureUnitCode);

        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        double quantity = (double)defaultQuantity.ToQuantity(TypeCode.Double); 
        ISpreadMeasure spreadMeasure = CreateSpreadMeasure(measureUnit,quantity);

        return CreateSpread(spreadMeasure);
    }

    public ISpread CreateSpread(ISpreadMeasure spreadMeasure)
    {
        return GetSpreadChild(spreadMeasure, this);
    }

    private SpreadChild GetSpreadChild(ISpreadMeasure spreadMeasure, SpreadFactoryObject spreadFactoryObject)
    {
        return SpreadChild.GetSpreadChild(spreadMeasure, this);
    }

    public ISpreadMeasure CreateSpreadMeasure(Enum measureUnit, double quantity)
    {
        return GetSpreadMeasureBaseMeasureObject(measureUnit, quantity);
    }
}
