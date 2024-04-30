namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Spreads;

public sealed class ShapeComponentSpreadMeasureObject : SpreadMeasureObject, IShapeComponent
{
    public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(Enum measureUnit, ValueType quantity)
    {
        return new()
        {
            MeasureUnit = measureUnit,
            Quantity = Convert.ToDouble(quantity) /*(double)quantity.ToQuantity(TypeCode.Double)*/,
        };
    }

    public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(ISpreadMeasure spreadMeasure)
    {
        Enum measureUnit = spreadMeasure.GetBaseMeasureUnit();
        double quantity = spreadMeasure.GetQuantity();

        return GetShapeComponentSpreadMeasureObject(measureUnit, quantity);
    }

    public bool Equals(IShapeComponent x, IShapeComponent y)
    {
        return FakeMethods.Equals(x, y);
    }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        yield return this;
    }

    public decimal GetDefaultQuantity()
    {
       return Convert.ToDecimal(Quantity)
            * GetExchangeRate(MeasureUnit, nameof(MeasureUnit))
            .Round(RoundingMode.DoublePrecision);
    }

    public int GetHashCode([DisallowNull] IShapeComponent shapeComponent)
    {
        return FakeMethods.GetHashCode(shapeComponent);
    }

    public MeasureUnitCode GetMeasureUnitCode()
    {
        return Measurable.GetMeasureUnitCode(MeasureUnit);
    }

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        return measureUnitCode == GetMeasureUnitCode();
    }

    public void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        if (HasMeasureUnitCode(measureUnitCode)) return;

        throw InvalidMeasureUnitCodeEnumArgumentException(measureUnitCode, paramName);
    }
}
