namespace CsabaDu.FooVaria.Measurables.Behaviors;

public interface IQuantityTypeCode
{
    TypeCode GetQuantityTypeCode();
    TypeCode? GetQuantityTypeCode(object quantity);

    void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName);
}
