namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;

public sealed class SpreadMeasureObject : ISpreadMeasure
{
    public MeasureUnitCode MeasureUnitCode { private get; set; }
    public double Quantity { private get; set; }

    public ValueType GetBaseQuantity() => Quantity;

    public double GetQuantity() => Quantity;

    public object GetQuantity(TypeCode quantityTypeCode) => Quantity.ToQuantity(quantityTypeCode);

    public ISpreadMeasure GetSpreadMeasure() => this;

    public MeasureUnitCode GetSpreadMeasureUnitCode() => MeasureUnitCode;

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        _ = spreadMeasure ?? throw new ArgumentNullException(paramName);

        if (spreadMeasure.GetSpreadMeasureUnitCode() == MeasureUnitCode) return;

        throw new InvalidEnumArgumentException(paramName);
    }
}
