﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.TestDoubles.BaseTypes.Quantifiables;

public class SpreadMeasureObject : ISpreadMeasure
{
    public Enum MeasureUnit { protected get; set; }
    public double Quantity { protected get; set; }

    public static SpreadMeasureObject GetSpreadMeasureObject(Enum measureUnit, ValueType quantity)
    {
        return new()
        {
            MeasureUnit = measureUnit,
            Quantity = Convert.ToDouble(quantity) /*(double)quantity.ToQuantity(TypeCode.Double)*/,
        };
    }

    public static SpreadMeasureObject GetSpreadMeasureObject(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    {
        Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
        double quantity = (double)defaultQuantity.ToQuantity(TypeCode.Double);

        return GetSpreadMeasureObject(measureUnit, quantity);
    }

    public Enum GetBaseMeasureUnit() => MeasureUnit;

    public ValueType GetBaseQuantity() => GetQuantity();

    public Type GetMeasureUnitType() => MeasureUnit.GetType();

    public double GetQuantity() => (double)GetQuantity(TypeCode.Double);

    public object GetQuantity(TypeCode quantityTypeCode) => Quantity.ToQuantity(quantityTypeCode);

    public ISpreadMeasure GetSpreadMeasure() => this;

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        _ = spreadMeasure ?? throw new ArgumentNullException(paramName);

        if (spreadMeasure.GetMeasureUnitType() != GetMeasureUnitType())
        {
            throw new InvalidEnumArgumentException(paramName);
        }

        if (spreadMeasure.GetSpreadMeasure() is IBaseMeasure && spreadMeasure.GetQuantity() > 0) return;

        throw new ArgumentOutOfRangeException(paramName);
    }
}
