
namespace CsabaDu.FooVaria.Tests.TestHelpers.Fakes.BaseTypes.Spreads;

public sealed class ShapeComponentSpreadMeasureObject : SpreadMeasureObject, IShapeComponent
{
    public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(Enum measureUnit, ValueType quantity)
    {
        return new()
        {
            MeasureUnit = measureUnit,
            Quantity = (double)quantity.ToQuantity(TypeCode.Double),
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
        throw new NotImplementedException();
    }

    public IEnumerable<IShapeComponent> GetBaseShapeComponents()
    {
        throw new NotImplementedException();
    }

    public decimal GetDefaultQuantity()
    {
        throw new NotImplementedException();
    }

    public int GetHashCode([DisallowNull] IShapeComponent obj)
    {
        throw new NotImplementedException();
    }

    public MeasureUnitCode GetMeasureUnitCode()
    {
        throw new NotImplementedException();
    }

    public bool HasMeasureUnitCode(MeasureUnitCode measureUnitCode)
    {
        throw new NotImplementedException();
    }

    public void ValidateMeasureUnitCode(MeasureUnitCode measureUnitCode, [DisallowNull] string paramName)
    {
        throw new NotImplementedException();
    }

    //public static ShapeComponentSpreadMeasureObject GetShapeComponentSpreadMeasureObject(MeasureUnitCode measureUnitCode, decimal defaultQuantity)
    //{
    //    Enum measureUnit = measureUnitCode.GetDefaultMeasureUnit();
    //    double quantity = (double)defaultQuantity.ToQuantity(TypeCode.Double);

    //    return GetShapeComponentSpreadMeasureObject(measureUnit, quantity);
    //}
}
