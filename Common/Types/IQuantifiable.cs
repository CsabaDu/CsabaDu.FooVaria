namespace CsabaDu.FooVaria.Common.Types;

public interface IQuantifiable : IMeasurable, IQuantityType
{
    decimal GetDefaultQuantity();

    void ValidateQuantity(ValueType? quantity, string paramName); // TypeCode
}
