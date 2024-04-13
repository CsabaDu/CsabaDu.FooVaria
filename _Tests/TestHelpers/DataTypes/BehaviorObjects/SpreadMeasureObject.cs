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

    public void ValidateSpreadMeasure(ISpreadMeasure spreadMeasure, [DisallowNull] string paramName)
    {
        _ = spreadMeasure ?? throw new ArgumentNullException(paramName);

        if (spreadMeasure.GetMeasureUnitType() == GetMeasureUnitType()) return;

        throw new InvalidEnumArgumentException(paramName);
    }
}
