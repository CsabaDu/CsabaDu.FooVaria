namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IQuantityType
{
    TypeCode GetQuantityTypeCode(MeasureUnitTypeCode? measureUnitTypeCode = null);
}
