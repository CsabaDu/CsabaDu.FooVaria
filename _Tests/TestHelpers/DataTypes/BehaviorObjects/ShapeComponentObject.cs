using CsabaDu.FooVaria.BaseTypes.Measurables.Statics;

namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;

public sealed class ShapeComponentObject : IShapeComponent
{
    public MeasureUnitCode MeasureUnitCode { private get; set; }
    public decimal DefaultQuantity { private get; set; }

    public static ShapeComponentObject GetShapeComponentObject(Enum context, decimal defaultQuantity)
    {
        MeasurementElements measurementElements = GetMeasurementElements(context, nameof(context));
        MeasureUnitCode measureUnitCode = measurementElements.MeasureUnitCode;
        defaultQuantity *= measurementElements.ExchangeRate;

        return new()
        {
            MeasureUnitCode = measureUnitCode,
            DefaultQuantity = defaultQuantity,
        };
    }

    public decimal GetDefaultQuantity() => DefaultQuantity;

    public MeasureUnitCode GetMeasureUnitCode() => MeasureUnitCode;

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == MeasureUnitCode;
    }

    public void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw new InvalidEnumArgumentException(paramName);
    }
}
