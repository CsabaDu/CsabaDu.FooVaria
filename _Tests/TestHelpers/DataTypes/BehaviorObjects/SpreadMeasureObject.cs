namespace CsabaDu.FooVaria.Tests.TestHelpers.DataTypes.BehaviorObjects;

public sealed class SpreadMeasureObject : ISpreadMeasure
{
    public Enum MeasureUnit { private get; set; }
    public double Quantity { private get; set; }

    public Enum GetBaseMeasureUnit() => MeasureUnit;

    public ValueType GetBaseQuantity() => Quantity;

    public Type GetMeasureUnitType() => MeasureUnit.GetType();

    public double GetQuantity() => Quantity;

    public object GetQuantity(TypeCode quantityTypeCode) => Quantity.ToQuantity(quantityTypeCode);

    public ISpreadMeasure GetSpreadMeasure() => this;

    public MeasureUnitCode GetSpreadMeasureUnitCode() => GetMeasureUnitCode(MeasureUnit);

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        _ = spreadMeasure ?? throw new ArgumentNullException(paramName);

        if (spreadMeasure.GetSpreadMeasureUnitCode() == GetSpreadMeasureUnitCode()) return;

        throw new InvalidEnumArgumentException(paramName);
    }
}
