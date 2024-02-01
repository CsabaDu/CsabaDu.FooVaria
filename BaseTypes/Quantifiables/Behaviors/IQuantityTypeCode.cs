namespace CsabaDu.FooVaria.BaseTypes.Quantifiables.Behaviors;

public interface IQuantityTypeCode
{
    TypeCode? GetQuantityTypeCode(object quantity);

    //void ValidateQuantityTypeCode(TypeCode quantityTypeCode, string paramName);
}
