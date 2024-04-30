﻿namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.BaseMeasures;

public sealed class SpreadMeasureBaseMeasureObject(IRootObject rootObject, string paramName) : BaseMeasureChild(rootObject, paramName), ISpreadMeasure
{
    public static SpreadMeasureBaseMeasureObject GetSpreadMeasureBaseMeasureObject(Enum measureUnit, ValueType quantity, RateComponentCode? rateComponentCode = null)
    {
        if (measureUnit is null || !GetMeasureUnitCode(measureUnit).IsSpreadMeasureUnitCode()) return null;

        return new(Fields.RootObject, Fields.paramName)
        {
            Return = new()
            {
                GetBaseMeasurement = BaseMeasurementFactory.CreateBaseMeasurement(measureUnit),
                GetBaseQuantity = quantity is double ? quantity : Convert.ToDouble(quantity),
                GetFactory = GetBaseMeasureFactoryObject(rateComponentCode ?? RateComponentCode.Numerator),
            }
        };
    }

    public static SpreadMeasureBaseMeasureObject GetSpreadMeasureBaseMeasureObject(DataFields fields, RateComponentCode? rateComponentCode = null)
    {
        return GetSpreadMeasureBaseMeasureObject(fields.measureUnit, fields.quantity, rateComponentCode);
    }

    public double GetQuantity() => (double)Return.GetBaseQuantity.ToQuantity(TypeCode.Double);

    public ISpreadMeasure GetSpreadMeasure() => this;

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        if (spreadMeasure == null) throw new ArgumentNullException(paramName);

        if (spreadMeasure is not IBaseMeasure) throw new ArgumentOutOfRangeException(paramName);
    }
}
