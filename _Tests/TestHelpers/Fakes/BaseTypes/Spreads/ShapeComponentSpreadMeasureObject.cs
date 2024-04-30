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
        if (x is null && y is null) return true;

        if (x is null || y is null) return false;

        if (x.GetMeasureUnitCode() != y.GetMeasureUnitCode()) return false;

        return x.GetBaseShapeComponents().SequenceEqual(y.GetBaseShapeComponents());
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
        HashCode hashCode = new();
        hashCode.Add(shapeComponent.GetMeasureUnitCode());

        foreach (IBaseShapeComponent item in shapeComponent.GetBaseShapeComponents())
        {
            hashCode.Add(item);
        }

        return hashCode.ToHashCode();
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

    //public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    //{
    //    Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    double quantity = (double)defaultQuantity.ToQuantity(TypeCode.Double);

    //    return GetShapeComponentSpreadMeasureObject(measureUnit, quantity);
    //}
}
